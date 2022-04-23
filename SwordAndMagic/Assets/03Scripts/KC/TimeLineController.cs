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
        temp = Instantiate(CurrentTimeLine, this.transform.position, Quaternion.identity); //������ġ�� Ÿ�Ӷ����� temp��� ���ӿ�����Ʈ�� ����
        temp.transform.SetParent(this.transform, false);                                   // temp�� ��ġ�� �� ��ü�� ������ ������.
        TimeLineSpawn = false;

        //���ӸŴ������� ������ Ÿ�Ӷ����� �������� �˷���.
        GameManager.GetComponent<GameManager>().RecieveTimeLine(temp);
    }

    //���ӸŴ����� ȣ�����ָ� 
    //�Ӽ��� �ٲ�� TimeLine�� ������.
    public void SelectTimeLine()
    { 
        selectNum = Random.Range(0, 2); //0 or 1

        if (selectNum == 0)
        {CurrentTimeLine = TimeLine_A;}
        else //selectNum=1
        {CurrentTimeLine = TimeLine_B;}

        TimeLineSpawn = true;
    }
}
