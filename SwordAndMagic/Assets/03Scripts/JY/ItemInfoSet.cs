using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//아이템 종류 저장 
[System.Serializable]
public class ItemInfo
{   
    public string ItemName;
    public string ItemAbility;
    public Sprite ItemImage;
    public int ItemLevel;   
}

//아이템 저장 리스트.
public class ItemInfoSet : MonoBehaviour
{
    public List<ItemInfo> Items = new List<ItemInfo>();

    public Sprite[] ItemImages;  

    void Awake()
    {
        //아이템 이미지 배열 동적 할당
        //유의점: 밑의 행이 정상적으로 실행되려면 이미지들(sprite)이 Resources파일에 반드시 저장 되어 있어야 합니다.
        //Resources파일 안에 있는 파일에 있으면 상관없습니다. Ex: ItemImages -> Resources안에 있는 파일이름입니다. 
        ItemImages = Resources.LoadAll<Sprite>("ItemImages");       

        //아이템 리스트에 아이템 정보 저장
        ItemInfo ItemA = new ItemInfo();
        ItemA.ItemName = "Ring of Strength";
        ItemA.ItemAbility = "Add 10 attackDamage";
        ItemA.ItemImage = ItemImages[0];
        ItemA.ItemLevel = 0;
        

        ItemInfo ItemB = new ItemInfo();
        ItemB.ItemName = "Protective Ring";
        ItemB.ItemAbility = "Add 1 armorPoint ";
        ItemB.ItemImage = ItemImages[1];
        ItemB.ItemLevel = 0;        

        ItemInfo ItemC = new ItemInfo();
        ItemC.ItemName = "Ring of Wrath";
        ItemC.ItemAbility = "Add 12 AD_Speed";
        ItemC.ItemImage = ItemImages[2];
        ItemC.ItemLevel = 0;       

        ItemInfo ItemD = new ItemInfo();
        ItemD.ItemName = "Old Necklace";
        ItemD.ItemAbility = "Add 10 Max_HP";
        ItemD.ItemImage = ItemImages[3];
        ItemD.ItemLevel = 0;       

        ItemInfo ItemE = new ItemInfo();
        ItemE.ItemName = "Horn Necklace";
        ItemE.ItemAbility = "Add 24 AD_Speed ";
        ItemE.ItemImage = ItemImages[4];
        ItemE.ItemLevel = 0;       

        ItemInfo ItemF = new ItemInfo();
        ItemF.ItemName = "Sunstone Necklace";
        ItemF.ItemAbility = "Add 20 attackDamage";
        ItemF.ItemImage = ItemImages[5];
        ItemF.ItemLevel = 0;       

        ItemInfo ItemG = new ItemInfo();
        ItemG.ItemName = "Old Earring";
        ItemG.ItemAbility = "Add 1 Move_Speed";
        ItemG.ItemImage = ItemImages[6];
        ItemG.ItemLevel = 0;
       
        ItemInfo ItemH = new ItemInfo();
        ItemH.ItemName = "Crystal Earring";
        ItemH.ItemAbility = "Add 2 armorPoint";
        ItemH.ItemImage = ItemImages[7];
        ItemH.ItemLevel = 0;       

        ItemInfo ItemI = new ItemInfo();
        ItemI.ItemName = "Red Embellished Earring";
        ItemI.ItemAbility = "Add 20 Max_HP";
        ItemI.ItemImage = ItemImages[8];
        ItemI.ItemLevel = 0;       

        ItemInfo ItemJ = new ItemInfo();
        ItemJ.ItemName = "Dragon’s Blessing";
        ItemJ.ItemAbility = "Add 10 Max_HP" + "\n" + "Add 1 armorPoint" + "\n" + "Add 1 Move_Speed";
        ItemJ.ItemImage = ItemImages[9];
        ItemJ.ItemLevel = 0;       

        Items.Add(ItemA); 
        Items.Add(ItemB); 
        Items.Add(ItemC); 
        Items.Add(ItemD); 
        Items.Add(ItemE);
        Items.Add(ItemF); 
        Items.Add(ItemG); 
        Items.Add(ItemH); 
        Items.Add(ItemI); 
        Items.Add(ItemJ);
    }
}
