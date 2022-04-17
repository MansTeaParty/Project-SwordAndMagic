using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject   GameOverUI;

    public GameObject   TimeLineController;   // 타임라인 컨트롤러
    public GameObject   SelectingTimeLine;    // 선택된 타임라인

    private bool isSpawnAble;
    private bool isGameStart;
    private bool isGameOver;

    //시간 설정
    public  int     setTimeMinute       = 15;
    public  int     setTimeSecond       = 60;
    public  int     SetSpawnPoolTime    = 30;

    private float   currentTimeSecond;  // 초
    private int     currentTimeMinute;  // 분
    public  Text    TimeText;           

    void Start()
    {
        isGameOver = false;
        isGameStart = true;

        // 15분 1초에서 시작 -> 30초 단위로 몬스터 풀이 생성되기 위해
        // 15분 0초에서 시작하면 바로 59초가 되버림 -> 첫번째 몬스터 풀 안나오고 30초 뒤에나 나옴.
        currentTimeMinute = setTimeMinute;
        currentTimeSecond = 1;

        TimeLineController = GameObject.FindGameObjectWithTag("TimeLineController");
        // 타임라인 컨트롤러에게 타임라인을 선택할 것을 지시
        // 그러면 타임라인 컨트롤러가 SelectTimeLine함수에서 타임라인을 선택하고
        // 선택한 타임라인을 RecieveTimeLine을 호출하여 전달.
        // 받은 타임라인 정보를 SelectingTimeLine에 저장
        TimeLineController.GetComponent<TimeLineController>().SelectTimeLine();

        isSpawnAble = true;

        StartCoroutine((Timer()));
    }

    void Update()
    {}

    #region Timer
    void ShowTimeText()
    {
        TimeText.color = new Color(255, 255, 255);

        if (currentTimeMinute < 10)
        {
            TimeText.text = "0" + currentTimeMinute + " : " + currentTimeSecond;

            if (currentTimeSecond < 10)
            {
                TimeText.text ="0" + currentTimeMinute + " : " + "0" + currentTimeSecond;
            }
        }
        else
        {
            TimeText.text = currentTimeMinute + " : " + currentTimeSecond;

            if (currentTimeSecond < 10)
            {
                TimeText.text = currentTimeMinute + " : " + "0" + currentTimeSecond;
            }
        }
    }

    IEnumerator Timer()
    {
        if (currentTimeSecond == 0f)
        {
            currentTimeMinute -= 1;             // 분 -1
            currentTimeSecond = setTimeSecond;  // 초는 다시 60초로
        }

        currentTimeSecond -= 1f;

        ShowTimeText();

        yield return new WaitForSeconds(1.0f);

        if ((int)currentTimeSecond % SetSpawnPoolTime == 0 && isSpawnAble == true)
        {
            StartCoroutine(SpawnCool());
        }

        StartCoroutine(Timer());

    }
    #endregion

    //타임라인 컨트롤러가 호출하여 정해진 타임라인이 무엇인지 받아옴. 
    public void RecieveTimeLine(GameObject timeline)
    {
        SelectingTimeLine = timeline;  
        
    }

    //알아낸 SelectingTimeLine에게 일정시간마다 다음 몬스터 풀을 생성할 것을 전달
    IEnumerator SpawnCool()
    {
        isSpawnAble = false;

        //TimeLine.cs의 NextPool 함수호출
        SelectingTimeLine.GetComponent<TimeLine>().NextPool();

        //SetSpawnPoolTime(30초) 동안 대기
        yield return new WaitForSeconds(SetSpawnPoolTime);
        isSpawnAble = true;
    }

   
    #region GameOverUI
    //---(GameOverUI)----

    public void gotoMain()
    {
        SceneManager.LoadScene("SY_WorkScene"); //임시로 씬 재로드
    }

    public void playerDie()
    {
        GameOverUI.SetActive(true);
    }
    #endregion
}
