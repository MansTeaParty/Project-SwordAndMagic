using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineController : MonoBehaviour
{
    private GameObject GameManager;

    public GameObject TimeLine_A;
    public GameObject TimeLine_B;
    private int selectNum;

    public GameObject CurrentTimeLine;
    private GameObject temp;

    private bool TimeLineSpawn;

    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
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
            default:
                temp = Instantiate(CurrentTimeLine,this.transform.position,Quaternion.identity); //현재위치에 타임라인을 temp라는 게임오브젝트로 만들어서
                temp.transform.SetParent(this.transform, false); // temp의 위치를 이 객체의 하위로 생성함.
                TimeLineSpawn = false;

                //게임매니저에게 결정된 타임라인이 무엇인지 알려줌.
                GameManager.SendMessage("RecieveTimeLine", temp);
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
        {CurrentTimeLine = TimeLine_A;}
        else //selectNum=1
        {CurrentTimeLine = TimeLine_B;}

        TimeLineSpawn = true;
    }
}
