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
            Debug.LogError("未找到SpritePathMap.json，请先执行导出操作！");
            return;
        }

        SerializableDict serializableDict = JsonUtility.FromJson<SerializableDict>(jsonAsset.text);
        _spritePathDict = serializableDict.ToDict();
    }

    // 获取Sprite路径（返回格式：普通Sprite为资源路径，图集Sprite为"图集路径::精灵名称"）
    public string GetSpritePath(Sprite sprite)
    {
        if (sprite == null || _spritePathDict == null)
            return null;

        string key = sprite.GetInstanceID().ToString();
        _spritePathDict.TryGetValue(key, out string path);
        return path;
    }

    // 解析图集路径（从"图集路径::精灵名称"中提取图集路径）
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
