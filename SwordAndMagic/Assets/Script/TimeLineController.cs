using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineController : MonoBehaviour
{
    public GameObject TimeLine_A;
    public GameObject TimeLine_B;
    private int selectNum;

    public GameObject CurrentTimeLine;

    private bool TimeLineSpawn;
    void Start()
    {
        selectNum = -1;
        TimeLineSpawn = false;
    }


    void Update()
    {
        if (TimeLineSpawn == true)
        {
            WhatisCurrentTimeLine();
        }
    }

    void WhatisCurrentTimeLine()
    {
        switch (selectNum)
        {
            /*case 0:
                Instantiate(CurrentTimeLine); //지정된 현재 타임라인을 생성하고
                CurrentTimeLine.transform.SetParent(this.gameObject.transform, false); //그 타임라인의 위치를 이 객체의 위치로 함.
                TimeLineSpawn = false;
                break;

            case 1:
                Instantiate(CurrentTimeLine);
                TimeLineSpawn = false;
                break;*/

            default:
                GameObject temp = Instantiate(CurrentTimeLine,this.transform.position,Quaternion.identity); //현재위치에 타임라인을 temp라는 게임오브젝트로 만들어서
                temp.transform.SetParent(this.transform, false); // temp의 위치를 이 객체의 하위로 생성함.
                TimeLineSpawn = false;
                break;

        }
    }

    //게임매니저가 호출해주면 
    //속성값 바뀌고 TimeLine이 결정됨.
    public void SelectTimeLine()
    {
        selectNum = Random.Range(0, 2); // 0부터 1까지
        Debug.Log("selectNum:" + selectNum);
        if (selectNum == 0)
        {
            CurrentTimeLine = TimeLine_A;
            TimeLineSpawn = true;
        }
        else //selectNum=1
        {
            CurrentTimeLine = TimeLine_B;
            TimeLineSpawn = true;
        }
    }
}
