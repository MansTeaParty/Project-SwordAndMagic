using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject[] TargetSpawners;
    public GameObject TimeLineController;//타임라인 컨트롤러
    public GameObject SelectingTimeLine; //선택된 타임라인

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
        //타임라인 컨트롤러에게 타임라인을 선택할 것을 지시
        //그러면 타임라인 컨트롤러가 SelectTimeLine함수에서 타임라인을 선택하고
        // 다시 sendMessage로 선택된 타임라인을 알려줌.
        // 받은 타임라인 정보를 SelectingTimeLine에 저장
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
        //currentTime을 시간에 따라 + 시킴
        currentTime += Time.deltaTime;
        Debug.Log("현재 시간 : " + (int)currentTime);

        //몇 배수일 때 -> SetSpawnPoolTime초 마다
        //문제점 : 1초동안 실행되는 문장이라는 것
        //update로 엄청나게 많이 호출 하기 때문에 
        //TimeLine.cs의 리스트 인덱스 값이 덩달아 올라감.
        if ((int)currentTime % SetSpawnPoolTime == 0 && isSpawnAble == true)
        {
            StartCoroutine(SpawnCool());
        }
    }


    //타임라인 컨트롤러가 호출하여 정해진 타임라인이 무엇인지 받아옴. 
    void RecieveTimeLine(GameObject timeline)
    {
        SelectingTimeLine = timeline;
        //Debug.Log("SelectingTimeLine: " + SelectingTimeLine);
    }

    IEnumerator SpawnCool()
    {
        isSpawnAble = false;

        //Debug.Log(SetSpawnPoolTime + "초 지남");
        //Debug.Log("다음 몬스터 풀 생성");
        //TimeLine.cs의 NextPool 함수호출
        SelectingTimeLine.SendMessage("NextPool");

        //Debug.Log("nextpool 호출");
        yield return new WaitForSeconds(30.0f);
        isSpawnAble = true;
    }
}
