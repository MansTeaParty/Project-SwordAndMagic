using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    public PlayerCtrl Player;

    [Header("PlayerStatus")]//PlayerStatus
   
    public int maxHP = 100;     //HP�� ���� ��ȭ�ϴ� �����̹Ƿ� �ϴ��� �÷��̾ �־���, ���� ���࿡ ���� ��ȭ�� ��


    public int armorPoint = 1;         //����
    public int attackDamage = 10;      //�⺻���ݷ�
    public float attackSpeed = 0.5f;   //���ݼӵ�
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
}
