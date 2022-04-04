using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//������ ���� ���� 
[System.Serializable]
public class ItemInfo
{   
    public string ItemName;
    public string ItemAbility;
    public Sprite ItemImage;
    public int ItemLevel;   
}

//������ ���� ����Ʈ.
public class ItemInfoSet : MonoBehaviour
{
    public List<ItemInfo> Items = new List<ItemInfo>();

    public Sprite[] ItemImages;  

    void Awake()
    {
        //������ �̹��� �迭 ���� �Ҵ�
        //������: ���� ���� ���������� ����Ƿ��� �̹�����(sprite)�� Resources���Ͽ� �ݵ�� ���� �Ǿ� �־�� �մϴ�.
        //Resources���� �ȿ� �ִ� ���Ͽ� ������ ��������ϴ�. Ex: ItemImages -> Resources�ȿ� �ִ� �����̸��Դϴ�. 
        ItemImages = Resources.LoadAll<Sprite>("ItemImages");       

        //������ ����Ʈ�� ������ ���� ����
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
        ItemJ.ItemName = "Dragon��s Blessing";
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
