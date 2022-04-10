using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualSkill : MonoBehaviour
{
    public int requiredLevel=0;
    public bool Option_Damaged = false; //�÷��̾��� �⺻ ������ ����� ��ü�ϴ°�


    [Header("Crusaders")]
    public int ShieldCount = 1000;
    public GameObject ironWall;

    public enum SkillName
    {
        NA,
        Crusader_Base,CrusaderA,CrusaderB, CrusaderC, CrusaderD, CrusaderE, CrusaderF
            
    };
    public SkillName skillName = SkillName.NA;

    public bool Damaged()
    {
        //Debug.Log("damagedȣ��");
        switch (skillName)
        {
            case SkillName.Crusader_Base:
                if (ShieldCount > 0)
                {
                    ShieldCount--;
                    Instantiate(ironWall, transform.position, transform.rotation);
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
                
                
            default:
                return false;
        }

        return false;
    }

    // Start is called before the first frame update
    void SetUp(InherenceSkill inherenceSkill)
    {
        switch (skillName)
        {
            case SkillName.CrusaderA:
                inherenceSkill.overlapAble = true;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
