using UnityEditor;
using UnityEditor.U2D;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.U2D;

public class SpritePathExporter
{
    [MenuItem("Tools/导出Sprite路径映射（含图集）")]
    public static void ExportSpritePaths()
    {
        Debug.Log("开始导出Sprite路径（含图集）...");

        Dictionary<string, string> spritePathDict = new Dictionary<string, string>();

        // 1. 处理普通单张Sprite（非图集）
        ProcessRegularSprites(spritePathDict);

        // 2. 处理Sprite Atlas中的精灵
        ProcessSpriteAtlases(spritePathDict);

        // 保存映射文件
        SaveSpritePathMap(spritePathDict);
    }

    // 处理普通单张Sprite
    private static void ProcessRegularSprites(Dictionary<string, string> dict)
    {
        string[] spriteGuids = AssetDatabase.FindAssets("t:Sprite");
        foreach (string guid in spriteGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

            if (sprite != null && !dict.ContainsKey(sprite.GetInstanceID().ToString()))
            {
                // 检查是否为图集精灵（图集精灵的纹理名称通常含哈希值）
                if (!sprite.packed)
                {
                    dict.Add(sprite.GetInstanceID().ToString(), assetPath);
                    Debug.Log($"添加普通Sprite: {assetPath}");
                }
            }
        }
    }

    // 处理Sprite Atlas中的精灵
    private static void ProcessSpriteAtlases(Dictionary<string, string> dict)
    {
        // 查找所有Sprite Atlas资源
        string[] atlasGuids = AssetDatabase.FindAssets("t:SpriteAtlas");
        foreach (string guid in atlasGuids)
        {
            string atlasPath = AssetDatabase.GUIDToAssetPath(guid);
            SpriteAtlas atlas = AssetDatabase.LoadAssetAtPath<SpriteAtlas>(atlasPath);

            if (atlas != null)
            {
                // 获取图集中的所有精灵
                Sprite[] atlasSprites = new Sprite[atlas.spriteCount];
                atlas.GetSprites(atlasSprites);

                foreach (Sprite sprite in atlasSprites)
                {
                    if (sprite != null && !dict.ContainsKey(sprite.GetInstanceID().ToString()))
                    {
                        // 存储格式："图集路径::精灵名称"（方便区分）
                        string pathWithName = $"{atlasPath}::{sprite.name}";
                        dict.Add(sprite.GetInstanceID().ToString(), pathWithName);
                        Debug.Log($"添加图集Sprite: {pathWithName}");
                    }
                }
            }
        }
    }

    // 保存映射文件
    private static void SaveSpritePathMap(Dictionary<string, string> dict)
    {
        string json = JsonUtility.ToJson(new SerializableDict(dict));
        string savePath = "Assets/Resources/SpritePathMap.json";

        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        File.WriteAllText(savePath, json);
        AssetDatabase.Refresh();

        Debug.Log($"导出完成，共 {dict.Count} 个Sprite（含图集），文件路径：{savePath}");
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
