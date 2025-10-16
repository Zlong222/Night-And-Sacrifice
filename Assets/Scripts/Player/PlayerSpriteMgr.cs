using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerSpriteMgr : MonoBehaviour 
{
    private SpriteRenderer armorBody,armorLeft,armorRight,back,body
        ,bodyArmL,bodyArmR,bodyFootL,bodyFootR,bodyHead,clothBody,clothLeft
        ,clothRight,faceHair,footLeft,footRight,hair,helmet;
    private JObject jsonData_customPlayer;
    
    private void Awake()
    {
        GetSpriteRender();
        InitSpriteData();
    }
    public void InitSpriteData()
    {
        jsonData_customPlayer = GlobalGameData.LoadPlayerCustomCharactorDataFromJson(@"/SaveData/charactorData/customCharactor.json");
        if (jsonData_customPlayer != null)
        {
            ShowSpriteFromJsonFile(jsonData_customPlayer);
        }
        else
        {
            ShowDefaultSprite();
        }

    }
    public void MakeSureSelect(GameObject obj)
    {
        CustomCharactor customCharactor = new CustomCharactor();
        customCharactor.armorBodySpriteName = armorBody.sprite.texture.name;
        customCharactor.armorLeftSpriteName = armorLeft.sprite.texture.name;
        customCharactor.armorRightSpriteName = armorRight.sprite.texture.name;
        customCharactor.backSpriteName = back.sprite.name;
        customCharactor.bodySpriteName = body.sprite.texture.name;
        customCharactor.bodyArmLSpriteName = bodyArmL.sprite.texture.name;
        customCharactor.bodyArmRSpriteName = bodyArmR.sprite.texture.name;
        customCharactor.bodyFootLSpriteName = bodyFootL.sprite.texture.name;
        customCharactor.bodyFootRSpriteName = bodyFootR.sprite.texture.name;
        customCharactor.bodyHeadSpriteName = bodyHead.sprite.texture.name;
        customCharactor.clothBodySpriteName = clothBody.sprite.texture.name;
        customCharactor.clothLeftSpriteName = clothLeft.sprite.texture.name;
        customCharactor.clothRightSpriteName = clothRight.sprite.texture.name;
        customCharactor.faceHairSpriteName = faceHair.sprite.texture.name;
        customCharactor.footLeftSpriteName = footLeft.sprite.texture.name;
        customCharactor.footRightSpriteName = footRight.sprite.texture.name;
        customCharactor.hairSpriteName = hair.sprite.name;
        customCharactor.helmetSpriteName = helmet.sprite.name;

        GlobalGameData.SavePlayerCustomCharactorDataToJson(customCharactor); ;
    }
    private void ShowSpriteFromJsonFile(JObject jsData)
    {
        //加载图集
        Sprite[] bodySprites = Resources.LoadAll<Sprite>("Charactor/Body/" + jsData[4]);
        Sprite[] armorSprites = Resources.LoadAll<Sprite>("Charactor/Armor/" + jsData[0]);
        Sprite[] clothSprites = Resources.LoadAll<Sprite>("Charactor/Cloth/" + jsData[10]);
        Sprite[] footSprites = Resources.LoadAll<Sprite>("Charactor/Foot/" + jsData[14]);
        //bodyAtlas
        bodyArmL.sprite = Array.Find(bodySprites, s => s.name == "Arm_L");
        bodyArmR.sprite = Array.Find(bodySprites, s => s.name == "Arm_R");
        bodyFootL.sprite = Array.Find(bodySprites, s => s.name == "Foot_L");
        bodyFootR.sprite = Array.Find(bodySprites, s => s.name == "Foot_R");
        bodyHead.sprite = Array.Find(bodySprites, s => s.name == "Head");
        body.sprite = Array.Find(bodySprites, s => s.name == "Body");

        //bodyArmorAtlas
        armorBody.sprite = Array.Find(armorSprites, s => s.name == "Right");
        armorLeft.sprite = Array.Find(armorSprites, s => s.name == "Left");
        armorRight.sprite = Array.Find(armorSprites, s => s.name == "Body");
        //clothAtlas
        clothBody.sprite = Array.Find(clothSprites, s => s.name == "Body");
        clothLeft.sprite = Array.Find(clothSprites, s => s.name == "Left");
        clothRight.sprite = Array.Find(clothSprites, s => s.name == "Right");

        //footClothAtlas
        footRight.sprite = Array.Find(footSprites, s => s.name == "Right");
        footLeft.sprite = Array.Find(footSprites, s => s.name == "Left");

        //misc
        back.sprite = Resources.Load<Sprite>("Charactor/Back/" + jsData[3]);
        faceHair.sprite = Resources.Load<Sprite>("Charactor/FaceHair/" + jsData[13]);
        hair.sprite = Resources.Load<Sprite>("Charactor/Hair/" + jsData[16]);
        helmet.sprite = Resources.Load<Sprite>("Charactor/Helmet/" + jsData[17]);
        
    }
    private void ShowDefaultSprite()
    {

        //加载图集
        Sprite[] bodySprites = Resources.LoadAll<Sprite>("Charactor/Body/" + "Body_1");
        Sprite[] armorSprites = Resources.LoadAll<Sprite>("Charactor/Armor/"+"Armor_1" );
        Sprite[] clothSprites = Resources.LoadAll<Sprite>("Charactor/Cloth/"+"Cloth_1" );
        Sprite[] footSprites = Resources.LoadAll<Sprite>("Charactor/Foot/" +"Foot_1");
        //bodyAtlas
        bodyArmL.sprite = Array.Find(bodySprites, s => s.name == "Arm_L");
        bodyArmR.sprite = Array.Find(bodySprites, s => s.name == "Arm_R");
        bodyFootL.sprite = Array.Find(bodySprites, s => s.name == "Foot_L");
        bodyFootR.sprite = Array.Find(bodySprites, s => s.name == "Foot_R");
        bodyHead.sprite = Array.Find(bodySprites, s => s.name == "Head");
        body.sprite = Array.Find(bodySprites, s => s.name == "Body");

        //bodyArmorAtlas
        armorBody.sprite = Array.Find(armorSprites, s => s.name == "Right");
        armorLeft.sprite = Array.Find(armorSprites, s => s.name == "Left");
        armorRight.sprite = Array.Find(armorSprites, s => s.name == "Body");
        //clothAtlas
        clothBody.sprite = Array.Find(clothSprites, s => s.name == "Body");
        clothLeft.sprite = Array.Find(clothSprites, s => s.name == "Left");
        clothRight.sprite = Array.Find(clothSprites, s => s.name == "Right");

        //footClothAtlas
        footRight.sprite = Array.Find(footSprites, s => s.name == "Right");
        footLeft.sprite = Array.Find(footSprites, s => s.name == "Left");
        
        //misc
        back.sprite = Resources.Load<Sprite>("Charactor/Back/"+"Back_1" );
        faceHair.sprite = Resources.Load<Sprite>("Charactor/FaceHair/" +"FaceHair_1");
        hair.sprite = Resources.Load<Sprite>("Charactor/Hair/"+"Hair_1");
        helmet.sprite = Resources.Load<Sprite>("Charactor/Helmet/"+"Helmet_1");
    }
    public void GetSpriteRender()
    {
        Transform root = transform.GetChild(0).GetChild(0);
        Transform bodySet = root.GetChild(0);
        Transform p_body = bodySet.GetChild(0);
        Transform p_back = p_body.GetChild(0);
        Transform body_ = p_body.GetChild(1);
        Transform p_clothBody = body_.GetChild(0);
        Transform p_armorBody = body_.GetChild(1);
        Transform headSet = p_body.GetChild(2);
        #region headSet
        Transform p_head = headSet.GetChild(0);
        Transform p_hair = p_head.GetChild(0);
        Transform p_head_ = p_head.GetChild(1);
        Transform p_mustache = p_head.GetChild(2);
        //Transform p_Eye = p_head.GetChild(3);
        Transform p_helmet = p_head.GetChild(4);
        #endregion
        Transform armSet = p_body.GetChild(3);
        #region armSet
        Transform armL = armSet.GetChild(0);
        Transform lArm = armL.GetChild(0).GetChild(0).GetChild(0);
        Transform armR = armSet.GetChild(1);
        Transform rArm = armR.GetChild(0).GetChild(0).GetChild(0);
        #endregion
        Transform p_rFoot = root.GetChild(1);
        Transform p_lFoot = root.GetChild(2);


        back = p_back.GetChild(0).GetComponent<SpriteRenderer>();
        clothBody = p_clothBody.GetChild(0).GetComponent<SpriteRenderer>();
        armorBody = p_armorBody.GetChild(0).GetComponent<SpriteRenderer>();
        hair = p_hair.GetChild(0).GetComponent<SpriteRenderer>();
        bodyHead = p_head_.GetChild(0).GetComponent<SpriteRenderer>();
        faceHair = p_mustache.GetChild(0).GetComponent<SpriteRenderer>();
        helmet = p_helmet.GetChild(0).GetComponent<SpriteRenderer>();
        bodyArmL = lArm.GetComponent<SpriteRenderer>();
        clothLeft = lArm.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        armorLeft = lArm.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        bodyArmR = rArm.GetComponent<SpriteRenderer>();
        clothRight = rArm.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        armorRight = rArm.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        bodyFootR = p_rFoot.GetChild(0).GetComponent<SpriteRenderer>();
        footRight = p_rFoot.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        bodyFootL = p_lFoot.GetChild(0).GetComponent<SpriteRenderer>();
        footLeft = p_lFoot.GetChild(1).GetChild(0).GetComponent<SpriteRenderer>();
        body = body_.GetComponent<SpriteRenderer>();

        Debug.Log("获取成功");



    }
    

}

