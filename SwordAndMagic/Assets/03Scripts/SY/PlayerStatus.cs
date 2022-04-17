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


    //buffStatus�� lastingStatus�� ���� :
    //buff�� �ð��� ������ �˾Ƽ� ��ȭ�� �� ����(������ ����� �������� ����),
    //lastingStatus : ��ȭ�� ���� ������ ��� ������
    public CustomStatus buffStatus;
    public CustomStatus lastingStatus;//���⼭ �ʱⰪ ����
    public BuffManager buffManager;

    [Header("PlayerStatus")]
    public int currentHP = 100;
    public int currentMP = 0;


    [Header("PlayerSubStatus")]//�ҰŰ����� �̰͵� customstatus�� ����
    public int penetration = 1;        //����ü ���� Ƚ��
    public float projectileSpeed = 1.0f;//����ü �̵��ӵ� ���
    public float projectileScale = 1.0f;//����ü ũ�� ���
    public float duration = 1.0f;//����ȵ�       //�÷��̾��� ����ü�� ���ӵǴ� �ð����
    public float knockBack = 0.0f;     //���ݽ� �˹�Ÿ�
    public float expBonus = 1.0f;      //�÷��̾� ȹ�� ����ġ�� ���
    public float goldBonus = 1.0f;     //�÷��̾� ȹ�� ��差 ���


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

    public void addPlayerSpeed(float newSpeed)  //�÷��̾� ���� ��ȭ�� �߰��۾��� �ʿ��� ��� �Լ�����
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
        //Debug.Log("mpȸ��");
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
