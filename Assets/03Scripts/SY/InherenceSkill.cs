using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class InherenceSkill : MonoBehaviour
{
    //public List<IndividualSkill> SkillList = new List<IndividualSkill>();
    //public IndividualSkill[] SkillArray = new IndividualSkill[7];
    public IndividualSkill indiSkillBase;

    //[Header("Crusaders")]
    //public bool overlapAble = false;//���� ��ø���ɿ���
    //public int ShieldCount = 1000;
    //public GameObject ironWall;
    //public GameObject ironWallKnockBack;
    //public bool IronWallDamage = false;


    void Awake()
    {
        indiSkillBase.SetUp();
        //for(int index=1; index<SkillArray.Length; index++)
        //{   //�⺻������ ��ų ����Ʈ�� �Ҵ�� ��ų���� classLevel�� ���� ��Ȱ��ȭ
        //    //if (PlayerStatus.instance.classLevel<SkillArray[index].requiredLevel)
        //    //{
        //    //    SkillArray[index].gameObject.SetActive(false);
        //    //}
        //    //else
        //    //{
        //        SkillArray[index].SetUp();
        //    //}
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Damaged()
    {
        

        bool optionExist=false; //������������ ���ְ� �ϴ� �ɼ��� ����(T/F), 
        //for(int index=0; index< SkillArray.Length; index++)
        //{
        //    //SkillArray[index].SendMessage("Damaged");
        //    if (SkillArray[index].Damaged())
        //    {
        //        optionExist = true;
        //    }
        //}

        if (indiSkillBase.Damaged())
        {
            optionExist = true;
        }
        return optionExist;
    }

    public void CastSkill()
    {
        indiSkillBase.SkillCast();

        //for (int index = 0; index < SkillArray.Length; index++)
        //{
        //    //SkillArray[index].SendMessage("Damaged");
        //    //if (SkillArray[index].Damaged())
        //    //{
        //    //    //optionExist = true;
        //    //}
        //}
    }
}
