using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//damaged�� castSkill�� �и��Ǿ��ִµ� �������Ǽ��� ���ؼ��� key�� � ������ �۾��� ��������(damaged,castskill)
//�� �ְ� �ϳ��� �Լ����� switch���� ������ �͵� ��������� ���ǹ���ø�� �ʹ� ���ϰ� ���߿� �ٸ� �������� ��ĥ�Ŷ�
//�ϴ� �̷��� ������� ����

//����� �ȹް� �̷��� �� ������ ��ų�� �������� �ʹ� ������ ���� �� �־. ������ �ʹ� �������Ͱ����� ���ٰ� �Բ� �ڽ�Ŭ������ �и��� �� ���� ��.
//��ų�� ������ �ƴ϶� �ϳ��� ��ų�� ������ ������ �ϸ� �� ���� �� ������.... �׷��� inherence�� �ִ� �����鵵 �������� ������ �� ����
//

public class IndividualSkill : MonoBehaviour
{

    public InherenceSkill inherenceSkill;

    public int ClassLevel = 1;
    public int[] requiredClassLevel = new int[]{1,2,3,4,5,6};
    
    public bool Option_Damaged = false; //�÷��̾��� �⺻ ������ ����� ��ü�ϴ°�


    [Header("Crusaders")]
    public bool overlapAble = false;//���� ��ø���ɿ���
    public int ShieldCount = 1000;
    public GameObject ironWall;
    public GameObject ironWallKnockBack;
    public bool IronWallDamage = false;


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
    }

    public bool Damaged()
    {
        bool changeOption = false;

        if (true)
        {
            if (ShieldCount > 0)
            {
                
                if (IronWall.instance == null)
                {
                    ShieldCount--;
                    Instantiate(ironWallKnockBack, transform.position, transform.rotation, transform);
                }
                if(ShieldCount<=0)
                {
                    ironWall.SetActive(false);
                }
                //Instantiate(ironWall, transform, false);
                changeOption = true;
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
            //���������ؼ� ���� ���ߵ�
            //PlayerStatus.instance.attackDamage *= 2;
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
            //PlayerStatus.instance.movementSpeed +=PlayerStatus.instance.basemovementSpeed*0.5f;
        }
        else return changeOption;

        if (ClassLevel >= requiredClassLevel[5])
        {

            //���������ؼ� ���� ���ߵ�, �̰� �ı�Ÿ�̹��� �ǵ����������
            //PlayerStatus.instance.attackSpeed -= PlayerStatus.instance.baseAttackSpeed * 0.3f;
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
            ironWall.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
