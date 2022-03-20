using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject[] TargetSpawners;
    public GameObject TimeLineController;

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
        TimeLineController.SendMessage("SelectTimeLine");

        isSpawnAble = true;
        StageNum = 1;
    }

    void Update()
    {
        TimeCal();

        /*if (isSpawnAble == true)
        {
            //MonsterPoolController = GameObject.FindGameObjectWithTag("MonsterPool");
            for (int i = 0; i < 1; i++)
            {
                StartCoroutine(SendNextPool());
            }
        }*/

    }

    void TimeCal()
    {
        //currentTime�� �ð��� ���� + ��Ŵ
        currentTime += Time.deltaTime;
        Debug.Log("currentTime : " + (int)currentTime);

        if ((int)currentTime % SetSpawnPoolTime == 0) //�� ����� �� -> SetSpawnPoolTime�� ����
        { 
            for (int i = 0; i < 1; i++)
            {
                Debug.Log(SetSpawnPoolTime + ":�� ����");

                TimeLineController.SendMessage("NextPool");
            }



            /*if (isSpawnAble)
            {
                for (int i = 0; i < 1; i++)
                {
                    StartCoroutine(SendNextPool());
                }
            }*/
        }

    }

    void SendStageNum()
    { 
    
    }


    //TimeLineA �Ǵ� TimeLineB���� ���� ���� Ǯ�� �����ض�� �޽��� �����ϴ� ����
    /*
    IEnumerator SendNextPool(waveCount)
    {
        print("���� ���� Ǯ�� ����!");
        //TimeLineController.SendMessage("MonsterPool");
        //MonsterPoolController.SendMessage("SpawnCheck");

        isSpawnAble = false;

        yield return new WaitForSeconds(3.0f);
        isSpawnAble = true;
    }*/
}
