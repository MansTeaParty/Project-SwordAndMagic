using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public string MonsterName;
    public int Attack_Type; // ���� :0, ���Ÿ� : 1

    public float Attack_Range; //������ 0
    public float Attack_Cooltime;

    public int Monster_HP;
    public int Monster_Damage;
    public float Monster_MoveSpeed;
    public float Monster_DropRate; // 10% = 0.1
    public int Monster_Exp;

    public Sprite Monster_Idle;
    public Sprite Monster_Move;
    public Sprite Monster_Attack;

    public Animator thisAnim;


    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
