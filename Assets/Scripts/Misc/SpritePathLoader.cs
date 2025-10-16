using UnityEngine;
using System.Collections.Generic;

public class SpritePathLoader : MonoBehaviour
{
    private Dictionary<string, string> _spritePathDict;

    private void Awake()
    {
        TextAsset jsonAsset = Resources.Load<TextAsset>("SpritePathMap");
        if (jsonAsset == null)
        {
            Debug.LogError("δ�ҵ�SpritePathMap.json������ִ�е���������");
            return;
        }

        SerializableDict serializableDict = JsonUtility.FromJson<SerializableDict>(jsonAsset.text);
        _spritePathDict = serializableDict.ToDict();
    }

    // ��ȡSprite·�������ظ�ʽ����ͨSpriteΪ��Դ·����ͼ��SpriteΪ"ͼ��·��::��������"��
    public string GetSpritePath(Sprite sprite)
    {
        if (sprite == null || _spritePathDict == null)
            return null;

        string key = sprite.GetInstanceID().ToString();
        _spritePathDict.TryGetValue(key, out string path);
        return path;
    }

    // ����ͼ��·������"ͼ��·��::��������"����ȡͼ��·����
    public string GetAtlasPathFromSprite(Sprite sprite)
    {
        string fullPath = GetSpritePath(sprite);
        if (fullPath == null || !fullPath.Contains("::"))
            return null;

        return fullPath.Split(new[] { "::" }, System.StringSplitOptions.None)[0];
    }

    [System.Serializable]
    private class SerializableDict
    {
        public List<string> keys = new List<string>();
        public List<string> values = new List<string>();

        public Dictionary<string, string> ToDict()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < keys.Count; i++)
            {
                dict[keys[i]] = values[i];
            }
            return dict;
        }
    }
}
