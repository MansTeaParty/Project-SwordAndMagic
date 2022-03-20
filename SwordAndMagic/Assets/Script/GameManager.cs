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

    //시간 설정
    public int setTime;
    public int SetSpawnPoolTime;
    private float currentTime; // wave 카운트를 세기 위한 시간변수
    private float remainTime; // UI에 남은 시간 보여주는 변수
    private int waveCount;

    void Start()
    {
        isGameOver = false;
        isGameStart = true;

        currentTime = 0;
        //remainTime = setTime; //180초
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
        //currentTime을 시간에 따라 + 시킴
        currentTime += Time.deltaTime;
        Debug.Log("currentTime : " + (int)currentTime);

        if ((int)currentTime % SetSpawnPoolTime == 0) //몇 배수일 때 -> SetSpawnPoolTime초 마다
        { 
            for (int i = 0; i < 1; i++)
            {
                Debug.Log(SetSpawnPoolTime + ":초 지남");

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


    //TimeLineA 또는 TimeLineB에게 다음 몬스터 풀로 변경해라고 메시지 전달하는 역할
    /*
    IEnumerator SendNextPool(waveCount)
    {
        print("다음 몬스터 풀로 변경!");
        //TimeLineController.SendMessage("MonsterPool");
        //MonsterPoolController.SendMessage("SpawnCheck");

        isSpawnAble = false;

        yield return new WaitForSeconds(3.0f);
        isSpawnAble = true;
    }*/
}
