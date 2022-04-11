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

    public int ClassLevel = 1;
    public int[] requiredClassLevel = new int[]{1,2,3,4,5,6};
    
    public bool Option_Damaged = false; //플레이어의 기본 데미지 기믹을 대체하는가


    [Header("Crusaders")]
    public bool overlapAble = false;//방어수 중첩가능여부
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
            //Debug.Log("hp회복");
        }
        else return changeOption;

        if (ClassLevel >= requiredClassLevel[2])
        {
            //버프구현해서 따로 빼야됨
            //PlayerStatus.instance.attackDamage *= 2;
        }
        else return changeOption;

        if (ClassLevel >= requiredClassLevel[3])
        {
            //밀쳐내기 데미지
        }
        else return changeOption;

        if (ClassLevel >= requiredClassLevel[4])
        {
            //버프구현해서 따로 빼야됨
            //PlayerStatus.instance.movementSpeed +=PlayerStatus.instance.basemovementSpeed*0.5f;
        }
        else return changeOption;

        if (ClassLevel >= requiredClassLevel[5])
        {

            //버프구현해서 따로 빼야됨, 이건 파괴타이밍이 실드없어질때임
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
            //밀쳐내기 데미지
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
