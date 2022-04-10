using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualSkill : MonoBehaviour
{
    public int requiredLevel=0;
    public bool Option_Damaged = false; //플레이어의 기본 데미지 기믹을 대체하는가


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
        //Debug.Log("damaged호출");
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
                Debug.Log("hp회복");
                return false;
            

            case SkillName.CrusaderC:
                //버프구현해서 따로 빼야됨
                //PlayerStatus.instance.attackDamage *= 2;
                return false;
            

            case SkillName.CrusaderD:
                //밀쳐내기
                return false;
            

            case SkillName.CrusaderE:
                //버프구현해서 따로 빼야됨
                //PlayerStatus.instance.movementSpeed +=PlayerStatus.instance.basemovementSpeed*0.5f;
                return false;
            

            case SkillName.CrusaderF:
                //버프구현해서 따로 빼야됨, 이건 파괴타이밍이 실드없어질때임
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
