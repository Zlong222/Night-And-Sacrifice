using UnityEditor;
using UnityEditor.U2D;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

public class SpritePathExporter
{
    [MenuItem("Tools/����Sprite·��ӳ�䣨��ͼ����")]
    public static void ExportSpritePaths()
    {
        Debug.Log("��ʼ����Sprite·������ͼ����...");

        Dictionary<string, string> spritePathDict = new Dictionary<string, string>();

        // 1. ������ͨ����Sprite����ͼ����
        ProcessRegularSprites(spritePathDict);

        // 2. ����Sprite Atlas�еľ���
        ProcessSpriteAtlases(spritePathDict);

        // ����ӳ���ļ�
        SaveSpritePathMap(spritePathDict);
    }

    // ������ͨ����Sprite
    private static void ProcessRegularSprites(Dictionary<string, string> dict)
    {
        string[] spriteGuids = AssetDatabase.FindAssets("t:Sprite");
        foreach (string guid in spriteGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

            if (sprite != null && !dict.ContainsKey(sprite.GetInstanceID().ToString()))
            {
                // ����Ƿ�Ϊͼ�����飨ͼ���������������ͨ������ϣֵ��
                if (!sprite.packed)
                {
                    dict.Add(sprite.GetInstanceID().ToString(), assetPath);
                    Debug.Log($"�����ͨSprite: {assetPath}");
                }
            }
        }
    }

    // ����Sprite Atlas�еľ���
    private static void ProcessSpriteAtlases(Dictionary<string, string> dict)
    {
        // ��������Sprite Atlas��Դ
        string[] atlasGuids = AssetDatabase.FindAssets("t:SpriteAtlas");
        foreach (string guid in atlasGuids)
        {
            string atlasPath = AssetDatabase.GUIDToAssetPath(guid);
            SpriteAtlas atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(atlasPath);

            if (atlas != null)
            {
                // ��ȡͼ���е����о���
                Sprite[] atlasSprites = new Sprite[atlas.spriteCount];
                atlas.GetSprites(atlasSprites);

                foreach (Sprite sprite in atlasSprites)
                {
                    if (sprite != null && !dict.ContainsKey(sprite.GetInstanceID().ToString()))
                    {
                        // �洢��ʽ��"ͼ��·��::��������"���������֣�
                        string pathWithName = $"{atlasPath}::{sprite.name}";
                        dict.Add(sprite.GetInstanceID().ToString(), pathWithName);
                        Debug.Log($"���ͼ��Sprite: {pathWithName}");
                    }
                }
            }
        }
    }

    // ����ӳ���ļ�
    private static void SaveSpritePathMap(Dictionary<string, string> dict)
    {
        string json = JsonUtility.ToJson(new SerializableDict(dict));
        string savePath = "Assets/Resources/SpritePathMap.json";

        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        File.WriteAllText(savePath, json);
        AssetDatabase.Refresh();

        Debug.Log($"������ɣ��� {dict.Count} ��Sprite����ͼ�������ļ�·����{savePath}");
    }

    [System.Serializable]
    private class SerializableDict
    {
        public List<string> keys = new List<string>();
        public List<string> values = new List<string>();

        public SerializableDict(Dictionary<string, string> data)
        {
            foreach (var kvp in data)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }
    }
}
