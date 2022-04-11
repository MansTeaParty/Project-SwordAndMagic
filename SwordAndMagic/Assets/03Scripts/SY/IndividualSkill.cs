using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//damaged와 castSkill이 분리되어있는데 개발편의성을 위해서는 key로 어떤 유형의 작업을 수행할지(damaged,castskill)
//를 주고 하나의 함수에서 switch문을 돌리는 것도 방법이지만 조건문중첩이 너무 심하고 나중에 다른 패턴으로 고칠거라
//일단 이러한 방식으로 진행

//상속을 안받고 이렇게 한 이유는 스킬이 가짓수가 너무 많으면 힘들 수 있어서. 성능이 너무 떨어질것같으면 밑줄과 함께 자식클래스로 분리할 수 있을 것.
//스킬을 레벨이 아니라 하나의 스킬이 레벨을 가지게 하면 더 좋을 것 같은데.... 그러면 inherence에 있는 변수들도 이쪽으로 가져올 수 있음
//

public class IndividualSkill : MonoBehaviour
{
    public InherenceSkill inherenceSkill;

    public int requiredLevel=0;
    public bool Option_Damaged = false; //플레이어의 기본 데미지 기믹을 대체하는가



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
        //Debug.Log("damaged호출");
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
