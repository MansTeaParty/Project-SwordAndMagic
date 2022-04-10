using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    public PlayerCtrl Player;

    //public string playerClass = "Crusader"; //Temp
    public int classLevel = 0;

    [Header("PlayerStatus")]//PlayerStatus
   
    public int maxHP = 100;     //HP는 자주 변화하는 영역이므로 일단은 플레이어에 넣었음, 개발 진행에 따라 변화할 듯
    public int currentHP = 100;
    public int maxMP = 3;       //~23
    public int currentMP = 3;
    public int MPregen = 3;     //~23
    public int armorPoint = 1;         //방어력
    public int attackDamage = 10;      //기본공격력
    public float baseAttackSpeed = 3;       //기본공격속도attackSpeed - (baseAttackSpeed * 0.3) (3-23추가)
    public float attackSpeed = 0.5f;   //공격속도
    public float basemovementSpeed = 3.0f;
    public float movementSpeed = 1.0f;  //이동속도 계수


    [Header("PlayerSubStatus")]
    public int penetration = 1;        //투사체 관통 횟수
    public float projectileSpeed = 1.0f;//투사체 이동속도 계수
    public float projectileScale = 1.0f;//투사체 크기 계수
    public float knockBack = 0.0f;     //공격시 넉백거리
    public float expBonus = 1.0f;      //플레이어 획득 경험치량 계수
    public float goldBonus = 1.0f;     //플레이어 획득 골드량 계수

    void Awake()
    {
        Player=GetComponentInParent<PlayerCtrl>();
        instance = this;
    }

    public void addPlayerSpeed(float newSpeed)  //플레이어 스탯 변화에 추가작업이 필요한 경우 함수생성
    {
        movementSpeed += newSpeed;
        Player.moveSpeed = movementSpeed;
    }

    public void addPlayerCurrentHP(int HP_Value)
    {
        currentHP += HP_Value;
        if(currentHP>maxHP)
        {
            currentHP = maxHP;
        }
    }
}
