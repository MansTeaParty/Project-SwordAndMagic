using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
