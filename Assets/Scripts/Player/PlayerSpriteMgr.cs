using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
public class PlayerSpriteMgr : MonoBehaviour 
{
    private SpriteRenderer armorBody,armorLeft,armorRight,back,body,bodyArmL,bodyArmR,bodyFootL,bodyFootR,bodyHead,clothBody,clothLeft,clothRight,faceHair,footLeft,footRight,hair,helmet;
    private CustomCharactor jsonData_customPlayer;

    public List<string> armorList, backList, bodyList, clothList, footList, hairList, helmetList,faceHairList;
    public Button btHair,btHelmet,btBack,btFaceHair,btArmor,btBody,btCloth,btFoot;
    public int iHair=0,iHelmet=0,iBack=0,iFaceHair=0,iArmor=0,iBody=0,iCloth=0,iFoot=0;
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
    private void ShowSpriteFromJsonFile(CustomCharactor jsData)
    {
        //加载图集
        Sprite[] bodySprites = Resources.LoadAll<Sprite>("Charactor/Body/" + jsData.bodyArmLSpriteName);
        Sprite[] armorSprites = Resources.LoadAll<Sprite>("Charactor/Armor/" + jsData.armorBodySpriteName);
        Sprite[] clothSprites = Resources.LoadAll<Sprite>("Charactor/Cloth/" + jsData.clothLeftSpriteName);
        Sprite[] footSprites = Resources.LoadAll<Sprite>("Charactor/Foot/" + jsData.footLeftSpriteName);
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
        back.sprite = Resources.Load<Sprite>("Charactor/Back/" + jsData.backSpriteName);
        faceHair.sprite = Resources.Load<Sprite>("Charactor/FaceHair/" + jsData.faceHairSpriteName);
        hair.sprite = Resources.Load<Sprite>("Charactor/Hair/" + jsData.hairSpriteName);
        helmet.sprite = Resources.Load<Sprite>("Charactor/Helmet/" + jsData.helmetSpriteName);
        
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
        armorBody.sprite = Array.Find(armorSprites, s => s.name == "Body");
        armorLeft.sprite = Array.Find(armorSprites, s => s.name == "Left");
        armorRight.sprite = Array.Find(armorSprites, s => s.name == "Right");
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
        helmet.sprite = Resources.Load<Sprite>("Charactor/Helmet/"+"Helmet_11");
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
    
    public int Change(List<string> list,int index)
    {
        if (index == list.Count - 1)
        {
            index = 0;
        }
        else
        {
            index++;

        }
        return index;

    }
    private void Start()
    {
        btHair.onClick.AddListener(() =>
        {
            iHair = Change(hairList, iHair);
            hair.sprite = Resources.Load<Sprite>("Charactor/Hair/" + hairList[iHair]);
        });
        btHelmet.onClick.AddListener(() =>
        {
            iHelmet = Change(helmetList, iHelmet);
            helmet.sprite = Resources.Load<Sprite>("Charactor/Helmet/" + helmetList[iHelmet]);
        });
        btFaceHair.onClick.AddListener(() =>
        {
            iFaceHair = Change(faceHairList, iFaceHair);
            faceHair.sprite = Resources.Load<Sprite>("Charactor/FaceHair/" + faceHairList[iFaceHair]);
        });
        btBack.onClick.AddListener(() =>
        {
            iBack = Change(backList, iBack);
            back.sprite = Resources.Load<Sprite>("Charactor/Back/" + backList[iBack]);
        });
        btArmor.onClick.AddListener(() =>
        {
            iArmor = Change(armorList, iArmor);
            Sprite[] armorSprites = Resources.LoadAll<Sprite>("Charactor/Armor/" + armorList[iArmor]);
            armorBody.sprite = Array.Find(armorSprites, s => s.name == "Body");
            armorLeft.sprite = Array.Find(armorSprites, s => s.name == "Left");
            armorRight.sprite = Array.Find(armorSprites, s => s.name == "Right");
        });
        btBody.onClick.AddListener(() =>
        {
            iBody = Change(bodyList, iBody);
            Sprite[] bodySprites = Resources.LoadAll<Sprite>("Charactor/Body/" + bodyList[iBody]);
            bodyArmL.sprite = Array.Find(bodySprites, s => s.name == "Arm_L");
            bodyArmR.sprite = Array.Find(bodySprites, s => s.name == "Arm_R");
            bodyFootL.sprite = Array.Find(bodySprites, s => s.name == "Foot_L");
            bodyFootR.sprite = Array.Find(bodySprites, s => s.name == "Foot_R");
            bodyHead.sprite = Array.Find(bodySprites, s => s.name == "Head");
            body.sprite = Array.Find(bodySprites, s => s.name == "Body");
        });
        btCloth.onClick.AddListener(() =>
        {
            iCloth = Change(clothList, iCloth);
            Sprite[] clothSprites = Resources.LoadAll<Sprite>("Charactor/Cloth/" + clothList[iCloth]);
            clothBody.sprite = Array.Find(clothSprites, s => s.name == "Body");
            clothLeft.sprite = Array.Find(clothSprites, s => s.name == "Left");
            clothRight.sprite = Array.Find(clothSprites, s => s.name == "Right");
        });
        btFoot.onClick.AddListener(() =>
        {
            iFoot = Change(footList, iFoot);
            Sprite[] footSprites = Resources.LoadAll<Sprite>("Charactor/Foot/" + footList[iFoot]);
            footRight.sprite = Array.Find(footSprites, s => s.name == "Right");
            footLeft.sprite = Array.Find(footSprites, s => s.name == "Left");
        });
    }
    private void Update()
    {
        
    }

}

