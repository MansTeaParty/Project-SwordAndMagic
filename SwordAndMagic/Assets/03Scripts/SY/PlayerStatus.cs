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
   
    public int maxHP = 100;     //HP�� ���� ��ȭ�ϴ� �����̹Ƿ� �ϴ��� �÷��̾ �־���, ���� ���࿡ ���� ��ȭ�� ��
    public int currentHP = 100;
    public int maxMP = 3;       //~23
    public int currentMP = 3;
    public int MPregen = 3;     //~23
    public int armorPoint = 1;         //����
    public int attackDamage = 10;      //�⺻���ݷ�
    public float baseAttackSpeed = 3;       //�⺻���ݼӵ�attackSpeed - (baseAttackSpeed * 0.3) (3-23�߰�)
    public float attackSpeed = 0.5f;   //���ݼӵ�
    public float basemovementSpeed = 3.0f;
    public float movementSpeed = 1.0f;  //�̵��ӵ� ���


    [Header("PlayerSubStatus")]
    public int penetration = 1;        //����ü ���� Ƚ��
    public float projectileSpeed = 1.0f;//����ü �̵��ӵ� ���
    public float projectileScale = 1.0f;//����ü ũ�� ���
    public float knockBack = 0.0f;     //���ݽ� �˹�Ÿ�
    public float expBonus = 1.0f;      //�÷��̾� ȹ�� ����ġ�� ���
    public float goldBonus = 1.0f;     //�÷��̾� ȹ�� ��差 ���

    void Awake()
    {
        Player=GetComponentInParent<PlayerCtrl>();
        instance = this;
    }

    public void addPlayerSpeed(float newSpeed)  //�÷��̾� ���� ��ȭ�� �߰��۾��� �ʿ��� ��� �Լ�����
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
