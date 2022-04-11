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

    public int requiredLevel=0;
    public bool Option_Damaged = false; //�÷��̾��� �⺻ ������ ����� ��ü�ϴ°�



    public enum SkillName
    {
        NA,
        CrusaderBase,CrusaderA,CrusaderB, CrusaderC, CrusaderD, CrusaderE, CrusaderF
            
    };
    public SkillName skillName = SkillName.NA;



    private void Awake()
    {
        inherenceSkill = GetComponentInParent<InherenceSkill>();
    }

    public bool Damaged()
    {
        //Debug.Log("damagedȣ��");
        switch (skillName)
        {
            #region Crusader
            case SkillName.CrusaderBase:
                if (inherenceSkill.ShieldCount > 0)
                {
                    inherenceSkill.ShieldCount--;
                    Instantiate(inherenceSkill.ironWallKnockBack, transform.position, transform.rotation,transform);
                    //Instantiate(ironWall, transform, false);
                    return true;
                }
                else
                {
                    return false;
                }

            case SkillName.CrusaderA:
                return false;
            case SkillName.CrusaderB:
                PlayerStatus.instance.addPlayerCurrentHP(10);
                Debug.Log("hpȸ��");
                return false;
            

            case SkillName.CrusaderC:
                //���������ؼ� ���� ���ߵ�
                //PlayerStatus.instance.attackDamage *= 2;
                return false;
            

            case SkillName.CrusaderD:
                //���ĳ���
                return false;
            

            case SkillName.CrusaderE:
                //���������ؼ� ���� ���ߵ�
                //PlayerStatus.instance.movementSpeed +=PlayerStatus.instance.basemovementSpeed*0.5f;
                return false;
            

            case SkillName.CrusaderF:
                //���������ؼ� ���� ���ߵ�, �̰� �ı�Ÿ�̹��� �ǵ����������
                //PlayerStatus.instance.attackSpeed -= PlayerStatus.instance.baseAttackSpeed * 0.3f;
                return false;

            #endregion

            default:
                return false;
        }

        return false;
    }

    // Start is called before the first frame update
    public void SetUp()
    {
        switch (skillName)
        {
            case SkillName.CrusaderA:
                inherenceSkill.overlapAble = true;
                break;

        }
    }

    public void SkillCast()
    {
        switch (skillName)
        {
            case SkillName.CrusaderBase:
                if(inherenceSkill.ShieldCount <1 || inherenceSkill.overlapAble)
                {
                    inherenceSkill.ShieldCount += 1;
                }

                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
