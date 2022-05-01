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
    public int Damage;   
    public int Penetration;
    public float CoolTime1;
    public float CoolTime2;
    public Vector3 Size;
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
        Debug.Log(Resources.FindObjectsOfTypeAll<Sprite>().Length  + "zz"+ItemImages.Length);

        //아이템 리스트에 아이템 정보 저장
        ItemInfo ItemA = new ItemInfo();
        ItemA.ItemName = "Ring of Strength";
        ItemA.ItemAbility = "Add 10 AD_Power";
        ItemA.ItemImage = ItemImages[0];
        ItemA.ItemLevel = 0;        

        ItemInfo ItemB = new ItemInfo();
        ItemB.ItemName = "Protective Ring";
        ItemB.ItemAbility = "Add 1 Armor_Point ";
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
        ItemF.ItemAbility = "Add 20 AD_Power";
        ItemF.ItemImage = ItemImages[5];
        ItemF.ItemLevel = 0;       

        ItemInfo ItemG = new ItemInfo();
        ItemG.ItemName = "Old Earring";
        ItemG.ItemAbility = "Add 1 Move_Speed";
        ItemG.ItemImage = ItemImages[6];
        ItemG.ItemLevel = 0;
       
        ItemInfo ItemH = new ItemInfo();
        ItemH.ItemName = "Crystal Earring";
        ItemH.ItemAbility = "Add 2 Armor_Point";
        ItemH.ItemImage = ItemImages[7];
        ItemH.ItemLevel = 0;       

        ItemInfo ItemI = new ItemInfo();
        ItemI.ItemName = "Red Embellished Earring";
        ItemI.ItemAbility = "Add 20 Max_HP";
        ItemI.ItemImage = ItemImages[8];
        ItemI.ItemLevel = 0;       

        ItemInfo ItemJ = new ItemInfo();
        ItemJ.ItemName = "Dragon’s Blessing";
        ItemJ.ItemAbility = "Add 10 Max_HP" + "\n" + "Add 1 Armor_Point" + "\n" + "Add 1 Move_Speed";
        ItemJ.ItemImage = ItemImages[9];
        ItemJ.ItemLevel = 0;

        ItemInfo Arrow = new ItemInfo();
        Arrow.ItemName = "Arrow";
        Arrow.ItemLevel = 1;
        Arrow.ItemImage = ItemImages[10];
        Arrow.Damage = 5;
        Arrow.Penetration = 1;
        Arrow.Size = new Vector3(0.5f, 0.5f, 0.5f);
        Arrow.ItemAbility = "none";

        ItemInfo Fireball = new ItemInfo();
        Fireball.ItemName = "Fireball";
        Fireball.ItemLevel = 1;
        Fireball.ItemImage = ItemImages[11];
        Fireball.Damage = 15;
        Fireball.Penetration = 1;
        Fireball.Size = new Vector3(1.0f, 1.0f, 1.0f);
        Fireball.ItemAbility = "none";

        ItemInfo ShadowBolt = new ItemInfo();
        ShadowBolt.ItemName = "ShadowBolt";
        ShadowBolt.ItemLevel = 1;
        ShadowBolt.ItemImage = ItemImages[12];
        ShadowBolt.Damage = 10;
        ShadowBolt.Penetration = 1;
        ShadowBolt.Size = new Vector3(1.0f, 1.0f, 1.0f);
        ShadowBolt.ItemAbility = "none";              

        ItemInfo Thunderstorm = new ItemInfo();
        Thunderstorm.ItemName = "Thunderstorm";
        Thunderstorm.ItemLevel = 1;
        Thunderstorm.ItemImage = ItemImages[13];
        Thunderstorm.ItemAbility = "none";

        ItemInfo ThunderVeil = new ItemInfo();
        ThunderVeil.ItemName = "ThunderVeil";
        ThunderVeil.ItemLevel = 1;
        ThunderVeil.ItemImage = ItemImages[14];
        ThunderVeil.Damage = 10;
        ThunderVeil.Size = new Vector3(0.4f, 0.4f, 0.5f);
        ThunderVeil.CoolTime1 = 3.0f;
        ThunderVeil.CoolTime2 = 6.0f;
        ThunderVeil.ItemAbility = "none";

        ItemInfo IceShot = new ItemInfo();
        IceShot.ItemName = "IceShot";
        IceShot.ItemLevel = 1;
        IceShot.ItemImage = ItemImages[15];
        IceShot.Damage = 15;
        IceShot.Penetration = 1;
        IceShot.Size = new Vector3(8.0f, 6.0f, 6.0f);
        IceShot.CoolTime1 = 3.0f;
        IceShot.ItemAbility = "none";

        ItemInfo Lavas = new ItemInfo();
        Lavas.ItemName = "Lavas";
        Lavas.ItemLevel = 1;
        Lavas.ItemImage = ItemImages[16];
        Lavas.Damage = 3;
        Lavas.Penetration = 1;
        Lavas.Size = new Vector3(1.5f, 1.25f, 1.0f);
        Lavas.CoolTime1 = 5.0f;
        Lavas.CoolTime2 = 10.0f;
        Lavas.ItemAbility = "none";

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
        Items.Add(Arrow);
        Items.Add(Fireball);
        Items.Add(ShadowBolt);
        Items.Add(Thunderstorm);
        Items.Add(ThunderVeil);
        Items.Add(IceShot);
        Items.Add(Lavas);
        //ItemAbilitySet();
    }   

    void ItemAbilitySet()
    {
        Debug.Log("ItemAbilitySet");
        for(int i=10; i< 15; i++)
        {
            switch(i)
            {
                //Arrow
                case 10:
                    if (Items[10].ItemLevel < 9)
                    {
                        switch (Items[10].ItemLevel)
                        {
                            case 1:
                                Items[10].ItemAbility = "Fires at the nearst enemy";
                                break;
                            case 2:
                                Items[10].ItemAbility = "Base damage up 5";
                                break;
                            case 3:
                                Items[10].ItemAbility = "Increases the base size of your projectiles by 25%";
                                break;
                            case 4:
                                Items[10].ItemAbility = "Fires 1 more projectile";
                                break;
                            case 5:
                                Items[10].ItemAbility = "Base damage up 5";
                                break;
                            case 6:
                                Items[10].ItemAbility = "Passes through 1 more enemies";
                                break;
                            case 7:
                                Items[10].ItemAbility = "Increases the base size of your projectiles by 25%";
                                break;
                            case 8:
                                Items[10].ItemAbility = "Increases the number of projectile hits by 1";
                                break;
                        }
                    }
                    else
                    {
                        Items[10].ItemAbility = "Base damage up 2";
                    }
                    break;
                //Fireball
                case 11:
                    if (Items[11].ItemLevel < 9)
                    {
                        switch (Items[11].ItemLevel)
                        {
                            case 1:
                                Items[11].ItemAbility = "Fires 1 projectile in the direction of the mouse cursor. Every 5 normal attacks.";
                                break;
                            case 2:
                                Items[11].ItemAbility = "Base damage up 10";
                                break;
                            case 3:
                                Items[11].ItemAbility = "Increases the base size of your projectiles by 25%";
                                break;
                            case 4:
                                Items[11].ItemAbility = "Fires 1 more projectile";
                                break;
                            case 5:
                                Items[11].ItemAbility = "Base damage up 10";
                                break;
                            case 6:
                                Items[11].ItemAbility = "Increases the base size of your projectiles by 25%";
                                break;
                            case 7:
                                Items[11].ItemAbility = "Fires 1 more projectile";
                                break;
                            case 8:
                                Items[11].ItemAbility = "Passes through 10 more enemies";
                                break;
                        }
                    }
                    else
                    {
                        Items[11].ItemAbility = "Base damage up 5";
                    }
                    break;
                //ShadowBolt
                case 12:
                    if (Items[12].ItemLevel < 9)
                    {
                        switch (Items[12].ItemLevel)
                        {
                            case 1:
                                Items[12].ItemAbility = "Fires a projectile in the direction of the mouse cursor. Every time you roll";
                                break;
                            case 2:
                                Items[12].ItemAbility = "Base damage up 10";
                                break;
                            case 3:
                                Items[12].ItemAbility = "Increases the base size of your projectiles by 25%";
                                break;
                            case 4:
                                Items[12].ItemAbility = "Fires 1 more projectile";
                                break;
                            case 5:
                                Items[12].ItemAbility = "Base damage up 10";
                                break;
                            case 6:
                                Items[12].ItemAbility = "Passes through 1 more enemies";
                                break;
                            case 7:
                                Items[12].ItemAbility = "Increases the base size of your projectiles by 25%";
                                break;
                            case 8:
                                Items[12].ItemAbility = "Fires projectiles in the opposite direction of the mouse cursor. Every time you roll";
                                break;
                        }
                    }
                    else
                    {
                        Items[12].ItemAbility = "Base damage up 5";
                    }
                    break;
                //ThunderStorm
                case 13:
                    if (Items[13].ItemLevel < 9)
                    {
                        switch (Items[13].ItemLevel)
                        {
                            case 1:
                                Items[13].ItemAbility = "When a basic attack hits, the enemy has a chance to receive additional lightning strikes. Activation rate 5%";
                                break;
                            case 2:
                                Items[13].ItemAbility = "Base damage up 10";
                                break;
                            case 3:
                                Items[13].ItemAbility = "Activation rate increases by 5%";
                                break;
                            case 4:
                                Items[13].ItemAbility = "Activation rate increases by 5%";
                                break;
                            case 5:
                                Items[13].ItemAbility = "Base damage up 10";
                                break;
                            case 6:
                                Items[13].ItemAbility = "Activation rate increases by 5%";
                                break;
                            case 7:
                                Items[13].ItemAbility = "Activation rate increases by 5%";
                                break;
                            case 8:
                                Items[13].ItemAbility = "Lightning explodes, dealing half damage to nearby enemies";
                                break;
                        }
                    }
                    else
                    {
                        Items[13].ItemAbility = "Base damage up 3";
                    }
                    break;                    
                //ThunderVeil         
                case 14:
                    if (Items[14].ItemLevel < 9)
                    {
                        switch (Items[14].ItemLevel)
                        {
                            case 1:
                                Items[14].ItemAbility = "Activates a curtain that damages enemies around the character when hit. 10 damage per second, duration 3 seconds";
                                break;
                            case 2:
                                Items[14].ItemAbility = "Increases the damage per second of the veil by 5";
                                break;
                            case 3:
                                Items[14].ItemAbility = "Increases the duration of the veil by 2 seconds";
                                break;
                            case 4:
                                Items[14].ItemAbility = "Increases the damage per second of the veil by 5";
                                break;
                            case 5:
                                Items[14].ItemAbility = "Increases armor by 1 while the veil is active";
                                break;
                            case 6:
                                Items[14].ItemAbility = "Increases the duration of the veil by 2 seconds";
                                break;
                            case 7:
                                Items[14].ItemAbility = "The base size of the veil is increased by 50%";
                                break;
                            case 8:
                                Items[14].ItemAbility = "When an enemy is hit by the veil, the enemy is stunned for 2 seconds";
                                break;
                        }
                    }
                    else
                    {
                        Items[14].ItemAbility = "Increases the damage per second of the veil by 3";
                    }
                    break;
                case 15:
                    if (Items[15].ItemLevel < 9)
                    {
                        switch (Items[15].ItemLevel)
                        {
                            case 1:
                                Items[15].ItemAbility = "Fires 1 projectile in the direction of the mouse cursor. Base damage 10, Cooldown: 5 seconds";
                                break;
                            case 2:
                                Items[15].ItemAbility = "Enemies hit are frozen for 3 seconds";
                                break;
                            case 3:
                                Items[15].ItemAbility = "Base damage up 5";
                                break;
                            case 4:
                                Items[15].ItemAbility = "Fires 1 more projectile";
                                break;
                            case 5:
                                Items[15].ItemAbility = "Fires 1 more projectile";
                                break;
                            case 6:
                                Items[15].ItemAbility = "Enemies hit are frozen for an additional 3 seconds";
                                break;
                            case 7:
                                Items[15].ItemAbility = "Base damage up 5";
                                break;
                            case 8:
                                Items[15].ItemAbility = "It explodes when hit and freezes all nearby enemies";
                                break;
                        }
                    }
                    else
                    {
                        Items[15].ItemAbility = "Enemies hit are frozen for an additional 1 seconds";
                    }
                    break;
                case 16:
                    if (Items[16].ItemLevel < 9)
                    {
                        switch (Items[16].ItemLevel)
                        {
                            case 1:
                                Items[16].ItemAbility = "Creates a lava area under the character. 3 damage per second, Duration 5 seconds, Cooldown: 10 seconds";
                                break;
                            case 2:
                                Items[16].ItemAbility = "Increases damage by 2 per second";
                                break;
                            case 3:
                                Items[16].ItemAbility = "Increases the base size of your projectiles by 25%";
                                break;
                            case 4:
                                Items[16].ItemAbility = "Duration increased by 2 seconds";
                                break;
                            case 5:
                                Items[16].ItemAbility = "Increases damage by 2 per second";
                                break;
                            case 6:
                                Items[16].ItemAbility = "Increases the base size of your projectiles by 25%";
                                break;
                            case 7:
                                Items[16].ItemAbility = "Duration increased by 2 seconds";
                                break;
                            case 8:
                                Items[16].ItemAbility = "Enemies touching the lava have their movement speed reduced by 30% for 5 seconds";
                                break;
                        }
                    }
                    else
                    {
                        Items[16].ItemAbility = "Duration increased by 1 seconds";
                    }
                    break;
            }
        }
    }
}
