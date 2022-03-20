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
                Instantiate(CurrentTimeLine); //������ ���� Ÿ�Ӷ����� �����ϰ�
                CurrentTimeLine.transform.SetParent(this.gameObject.transform, false); //�� Ÿ�Ӷ����� ��ġ�� �� ��ü�� ��ġ�� ��.
                TimeLineSpawn = false;
                break;

            case 1:
                Instantiate(CurrentTimeLine);
                TimeLineSpawn = false;
                break;*/

            default:
                GameObject temp = Instantiate(CurrentTimeLine,this.transform.position,Quaternion.identity); //������ġ�� Ÿ�Ӷ����� temp��� ���ӿ�����Ʈ�� ����
                temp.transform.SetParent(this.transform, false); // temp�� ��ġ�� �� ��ü�� ������ ������.
                TimeLineSpawn = false;
                break;

        }
    }

    //���ӸŴ����� ȣ�����ָ� 
    //�Ӽ��� �ٲ�� TimeLine�� ������.
    public void SelectTimeLine()
    {
        selectNum = Random.Range(0, 2); // 0���� 1����
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
