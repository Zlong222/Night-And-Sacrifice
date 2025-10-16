using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
public class CustomCharactor
{
    public string armorBodySpriteName { get; set;}
    public string armorLeftSpriteName { get; set; }
    public string armorRightSpriteName { get; set; }
    public string backSpriteName { get; set; }
    public string bodySpriteName { get; set; }
    public string bodyArmLSpriteName { get; set; }
    public string bodyArmRSpriteName { get; set; }
    public string bodyFootLSpriteName { get; set; }
    public string bodyFootRSpriteName { get; set; }
    public string bodyHeadSpriteName { get; set; }
    public string clothBodySpriteName { get; set; }
    public string clothLeftSpriteName { get; set; }
    public string clothRightSpriteName { get; set; }
    public string faceHairSpriteName { get; set; }
    public string footLeftSpriteName { get; set; }
    public string footRightSpriteName { get; set; }
    public string hairSpriteName { get; set; }
    public string helmetSpriteName { get; set; }

}   
public class GlobalGameData : MonoBehaviour
{
    public static void SavePlayerCustomCharactorDataToJson(CustomCharactor customCharactor)
    {
        FileInfo file = new FileInfo(Application.dataPath + @"/SaveData/charactorData/customCharactor.json");
        StreamWriter sw = file.CreateText();
        string json = JsonConvert.ToString(customCharactor);
        sw.Write(json);
        sw.Close();
        sw.Dispose();
    }
    public static JObject LoadPlayerCustomCharactorDataFromJson(string jsonFileNamePath)
    {
        FileInfo file = new FileInfo(Application.dataPath + jsonFileNamePath);
        if (file.Exists)
        {
            StreamReader sr = file.OpenText();
            string strData = sr.ReadToEnd();
            JObject jsdata3 = JObject.Parse(strData);
            JsonConvert.ToString(jsdata3);
            return jsdata3;
        }
        else
        {
            Debug.LogError("");
            return null;
        }
    }
}
