using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject[] TargetSpawners;
    public GameObject TimeLineController;//Ÿ�Ӷ��� ��Ʈ�ѷ�
    public GameObject SelectingTimeLine; //���õ� Ÿ�Ӷ���

    private bool isSpawnAble;

    private int StageNum;

    private bool isGameStart;
    private bool isGameOver;

    //�ð� ����
    public int setTime;
    public int SetSpawnPoolTime;
    private float currentTime; // wave ī��Ʈ�� ���� ���� �ð�����
    private float remainTime; // UI�� ���� �ð� �����ִ� ����
    private int waveCount;

    void Start()
    {
        isGameOver = false;
        isGameStart = true;

        currentTime = 0;
        //remainTime = setTime; //180��
        waveCount = 1;

        TimeLineController = GameObject.FindGameObjectWithTag("TimeLineController");
        //Ÿ�Ӷ��� ��Ʈ�ѷ����� Ÿ�Ӷ����� ������ ���� ����
        //�׷��� Ÿ�Ӷ��� ��Ʈ�ѷ��� SelectTimeLine�Լ����� Ÿ�Ӷ����� �����ϰ�
        // �ٽ� sendMessage�� ���õ� Ÿ�Ӷ����� �˷���.
        // ���� Ÿ�Ӷ��� ������ SelectingTimeLine�� ����
        TimeLineController.SendMessage("SelectTimeLine");

        isSpawnAble = true;
        StageNum = 1;
    }

    void Update()
    {
        TimeCal();
    }

    void TimeCal()
    {
        //currentTime�� �ð��� ���� + ��Ŵ
        currentTime += Time.deltaTime;
        Debug.Log("���� �ð� : " + (int)currentTime);

        //�� ����� �� -> SetSpawnPoolTime�� ����
        if ((int)currentTime % SetSpawnPoolTime == 0)
        {
            for (int i = 0; i < 1; i++)
            {
                Debug.Log(SetSpawnPoolTime + "�� ����");
                Debug.Log("���� ���� Ǯ ����");
                //TimeLine.cs�� NextPool �Լ�
                SelectingTimeLine.SendMessage("NextPool");
            }
        }
    }


    //Ÿ�Ӷ��� ��Ʈ�ѷ��� ȣ���Ͽ� ������ Ÿ�Ӷ����� �������� �޾ƿ�. 
    void RecieveTimeLine(GameObject timeline)
    {
        SelectingTimeLine = timeline;
        Debug.Log("SelectingTimeLine: " + SelectingTimeLine);
    }
}
