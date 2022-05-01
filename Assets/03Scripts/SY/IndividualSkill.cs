using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//damaged�� castSkill�� �и��Ǿ��ִµ� �������Ǽ��� ���ؼ��� key�� � ������ �۾��� ��������(damaged,castskill)
//�� �ְ� �ϳ��� �Լ����� switch���� ������ �͵� ��������� ���ǹ���ø�� �ʹ� ���ϰ� ���߿� �ٸ� �������� ��ĥ�Ŷ�
//�ϴ� �̷��� ������� ����

//����� �ȹް� �̷��� �� ������ ��ų�� �������� �ʹ� ������ ���� �� �־. ������ �ʹ� �������Ͱ����� ���ٰ� �Բ� �ڽ�Ŭ������ �и��� �� ���� ��.
//��ų�� ������ �ƴ϶� �ϳ��� ��ų�� ������ ������ �ϸ� �� ���� �� ������.... �׷��� inherence�� �ִ� �����鵵 �������� ������ �� ����
//���߿� ũ�缼�̴� ������ ����Ŭ������ �����ҵ�

public class IndividualSkill : MonoBehaviour
{

    public InherenceSkill inherenceSkill;

    public int ClassLevel = 1;
    public int[] requiredClassLevel = new int[]{1,2,3,4,5,6};//��ų�䱸����
    
    public bool Option_Damaged = false; //�÷��̾��� �⺻ ������ ����� ��ü�ϴ°�

    public BuffManager buffManager;

    [Header("Crusaders")]
    public bool overlapAble = false;//���� ��ø���ɿ���
    public int ShieldCount = 1000;
    public GameObject ironWall;
    public GameObject ironWallKnockBack;
    public bool IronWallDamage = false;



    [Header("Buffs")]//������ ����°� �ƴ϶� �׳� �� ��ų�������� �迭�� �� ����� ==null�� ���̰� �Ⱥ��̰�� ���ϸ� ���� �� ����
    public GameObject ironWallMS_Buff = null;
    public GameObject ironWallAS_Buff=null;//ironwall�� Ȱ��ȭ�� ���¿��� ��� �ο��Ǵ� ����
    

    public enum SkillName
    {
        NA, Crusader
        //CrusaderBase,CrusaderA,CrusaderB, CrusaderC, CrusaderD, CrusaderE, CrusaderF
            
    };
    public SkillName skillName = SkillName.NA;



    private void Awake()
    {
        inherenceSkill = GetComponentInParent<InherenceSkill>();
        ClassLevel = PlayerStatus.instance.classLevel;
        if (ShieldCount >= 1)
        {
            ironWallAS_Buff = buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, (PlayerStatus.instance.lastingStatus.attackSpeed * -0.3f), 0), float.MaxValue);
        }
    }

    public bool Damaged()
    {
        bool changeOption = false;

        if (true)
        {
            if (ShieldCount <= 0)
            {
                ironWall.SetActive(false);
            }
            else if (ShieldCount > 0)
            {

                changeOption = true;
                if (IronWall.instance == null)
                {
                    ShieldCount--;
                    Instantiate(ironWallKnockBack, transform.position, transform.rotation, transform);
                    
                }
                else return true;//�ǵ�ߵ����̸� �Ʒ��� �������� ������ ����

                //Instantiate(ironWall, transform, false);

            }
        }
        
        if (ClassLevel >= requiredClassLevel[0])
        {
        }

        if (ClassLevel >= requiredClassLevel[1])
        {
            PlayerStatus.instance.addPlayerCurrentHP(10);

            //Debug.Log("hpȸ��");
        }
        else return changeOption;

        if (ClassLevel >= requiredClassLevel[2])
        {
            Debug.Log("���ݷ� ���");
            //PlayerStatus.instance.attackDamage *= 2;
            buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 10, 0, 0), 3.0f * PlayerStatus.instance.duration);

        }
        else return changeOption;

        if (ClassLevel >= requiredClassLevel[3])
        {
            //���ĳ��� ������
        }
        else return changeOption;

        if (ClassLevel >= requiredClassLevel[4])
        {
            //���������ؼ� ���� ���ߵ�
            if (ironWallMS_Buff == null)
            {
                ironWallMS_Buff=buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, 0, PlayerStatus.instance.lastingStatus.movementSpeed * 0.5f), 2.0f * PlayerStatus.instance.duration);
            }
            //PlayerStatus.instance.movementSpeed +=PlayerStatus.instance.basemovementSpeed*0.5f;
        }
        else return changeOption;

        if (ClassLevel >= requiredClassLevel[5])
        {
            if (ironWallAS_Buff != null && ShieldCount == 0)
            {
                Destroy(ironWallAS_Buff.GetComponent<GameObject>());
                ironWallAS_Buff = null;
            }
        }


        return changeOption;
    }

    // Start is called before the first frame update
    public void SetUp()
    {
        if (ClassLevel >= requiredClassLevel[0])
        {
            overlapAble = true;
        }
        if (ClassLevel >= requiredClassLevel[3])
        {
            Debug.Log("damageon");
            //���ĳ��� ������
            IronWallDamage = true;
        }

    }

    public void SkillCast()
    {
                
        if(ShieldCount <1 || overlapAble)
        {     
            ShieldCount += 1;
            if(ironWallAS_Buff==null)
            {
                Debug.Log("���Ӿ�");
                ironWallAS_Buff = buffManager.BuffToPlayer(new CustomStatus(0, 0, 0, 0, 0, PlayerStatus.instance.lastingStatus.attackSpeed * -0.3f, 0), float.MaxValue);
            }
            ironWall.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
