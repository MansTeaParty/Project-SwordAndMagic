using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//public class CustomStatus
//{
//    public int maxHP = 0;
//    public int maxMP = 0;
//    public int MP_Regen = 0;
//    public int armorPoint = 0;
//    public int attackDamage = 0;
//    public float attackSpeed = 0;
//    public float movementSpeed = 0;

//    public CustomStatus(int maxHP = 0, int maxMP = 0, int MP_Regen = 0, int armorPoint = 0, int attackDamage = 0, float attackSpeed = 0, float movementSpeed = 0)
//    {
//        this.maxHP = maxHP;
//        this.maxMP = maxMP;
//        this.MP_Regen = MP_Regen;
//        this.armorPoint = armorPoint;
//        this.attackDamage = attackDamage;
//        this.attackSpeed = attackSpeed;
//        this.movementSpeed = movementSpeed;
//    }
//}

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    public PlayerCtrl Player;

    //public string playerClass = "Crusader"; //Temp
    public int classLevel = 0;


    //buffStatus와 lastingStatus의 구분 :
    //buff는 시간이 지나면 알아서 변화할 수 있음(버프의 생기고 없어짐에 의해),
    //lastingStatus : 변화를 주지 않으면 계속 유지됨
    public CustomStatus buffStatus;
    public CustomStatus lastingStatus;//여기서 초기값 지정
    public BuffManager buffManager;

    [Header("PlayerStatus")]
    public int currentHP = 100;
    public int currentMP = 0;


    [Header("PlayerSubStatus")]//할거같으면 이것도 customstatus로 편입
    public int penetration = 1;        //투사체 관통 횟수
    public float projectileSpeed = 1.0f;//투사체 이동속도 계수
    public float projectileScale = 1.0f;//투사체 크기 계수
    public float duration = 1.0f;//연결안됨       //플레이어의 투사체가 지속되는 시간계수
    public float knockBack = 0.0f;     //공격시 넉백거리
    public float expBonus = 1.0f;      //플레이어 획득 경험치량 계수
    public float goldBonus = 1.0f;     //플레이어 획득 골드량 계수


    public int playerEXP;

    void Awake()
    {
        Player=GetComponentInParent<PlayerCtrl>();
        currentHP = lastingStatus.maxHP;
        currentMP = lastingStatus.maxMP;
        //buffStatus = new CustomStatus(0, 0, 0, 0, 0, 0, 0);// + buffStatus;
        //lastingStatus = new CustomStatus(100, 10, 3, 1, 10, 3.0f, 0.5f);// + lastingStatus;
        buffManager = GetComponentInChildren<BuffManager>();
        //attackSpeed = lastingStatus.attackSpeed;
        //movementSpeed = lastingStatus.movementSpeed;
        instance = this;


    }

    public void addPlayerSpeed(float newSpeed)  //플레이어 스탯 변화에 추가작업이 필요한 경우 함수생성
    {
        Player.moveSpeed = lastingStatus.movementSpeed += newSpeed;
        //Player.moveSpeed = movementSpeed;
    }

    public void addPlayerCurrentHP(int HP_Value)
    {
        currentHP += HP_Value;
        if(currentHP>getMaxHP())
        {
            currentHP = getMaxHP();
        }
    }

    public bool addPlayerCurrentMP(int MP_Value)
    {
        currentMP += MP_Value;
        //Debug.Log("mp회복");
        if (currentMP >= MP_Value)
        {
            currentMP = 0;//= maxMP;
            return true;
        }
        return false;
    }

    public void addPlayerEXP(int EXP_Value)
    {
        playerEXP += Mathf.FloorToInt(EXP_Value * expBonus);
    }

    public float getPlayerSpeed()
    {
        return lastingStatus.movementSpeed + buffStatus.movementSpeed;
    }

    public int getAttackDamage()
    {
        return lastingStatus.attackDamage + buffStatus.attackDamage;
    }

    public int getMaxHP()
    {
        return lastingStatus.maxHP + buffStatus.maxHP;
    }
    public int getMP_Regen()
    {
        return lastingStatus.MP_Regen + buffStatus.MP_Regen;
    }
    public float getAttackSpeed()
    {
        return lastingStatus.attackSpeed + buffStatus.attackSpeed;
    }

}
