using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class InherenceSkill : MonoBehaviour
{
    //public List<IndividualSkill> SkillList = new List<IndividualSkill>();
    public IndividualSkill[] SkillArray = new IndividualSkill[7];

    public bool overlapAble = false;

    void Start()
    {
        for(int index=1; index<SkillArray.Length; index++)
        {   //기본적으로 스킬 리스트에 할당된 스킬들은 classLevel에 따라서 비활성화
            if (PlayerStatus.instance.classLevel<SkillArray[index].requiredLevel)
            {
                SkillArray[index].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Damaged()
    {
        bool optionExist=false; //데미지판정을 없애게 하는 옵션의 유무(T/F), 
        for(int index=0; index< SkillArray.Length; index++)
        {
            //SkillArray[index].SendMessage("Damaged");
            if (SkillArray[index].Damaged())
            {
                optionExist = true;
            }
        }

        return optionExist;
    }

    public void CastSkill()
    {
        for (int index = 0; index < SkillArray.Length; index++)
        {
            //SkillArray[index].SendMessage("Damaged");
            //if (SkillArray[index].Damaged())
            //{
            //    //optionExist = true;
            //}
        }
    }
}
