using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//플레이어 능력치 저장 및 아이템 보상으로 인한 효과 적용 및 저장 static으로 싱글톤 적용 
public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;

    public int AD_Power = 10;
    public float AD_Speed = 1.0f;
    public int Max_HP = 100;
    public int Armor_Point = 5;
    public float Move_Speed = 6.0f;
   
    void Awake()
    {       
        instance = this;
    }    
}
