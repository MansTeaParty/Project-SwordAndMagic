using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject   GameOverUI;

    public GameObject   TimeLineController;   // Ÿ�Ӷ��� ��Ʈ�ѷ�
    public GameObject   SelectingTimeLine;    // ���õ� Ÿ�Ӷ���

    private bool isSpawnAble;
    private bool isGameStart;
    private bool isGameOver;

    //�ð� ����
    public  int     setTimeMinute       = 15;
    public  int     setTimeSecond       = 60;
    public  int     SetSpawnPoolTime    = 30;

    private float   currentTimeSecond;  // ��
    private int     currentTimeMinute;  // ��
    public  Text    TimeText;           

    void Start()
    {
        isGameOver = false;
        isGameStart = true;

        // 15�� 1�ʿ��� ���� -> 30�� ������ ���� Ǯ�� �����Ǳ� ����
        // 15�� 0�ʿ��� �����ϸ� �ٷ� 59�ʰ� �ǹ��� -> ù��° ���� Ǯ �ȳ����� 30�� �ڿ��� ����.
        currentTimeMinute = setTimeMinute;
        currentTimeSecond = 1;

        TimeLineController = GameObject.FindGameObjectWithTag("TimeLineController");
        // Ÿ�Ӷ��� ��Ʈ�ѷ����� Ÿ�Ӷ����� ������ ���� ����
        // �׷��� Ÿ�Ӷ��� ��Ʈ�ѷ��� SelectTimeLine�Լ����� Ÿ�Ӷ����� �����ϰ�
        // ������ Ÿ�Ӷ����� RecieveTimeLine�� ȣ���Ͽ� ����.
        // ���� Ÿ�Ӷ��� ������ SelectingTimeLine�� ����
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
            currentTimeMinute -= 1;             // �� -1
            currentTimeSecond = setTimeSecond;  // �ʴ� �ٽ� 60�ʷ�
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

    //Ÿ�Ӷ��� ��Ʈ�ѷ��� ȣ���Ͽ� ������ Ÿ�Ӷ����� �������� �޾ƿ�. 
    public void RecieveTimeLine(GameObject timeline)
    {
        SelectingTimeLine = timeline;  
        
    }

    //�˾Ƴ� SelectingTimeLine���� �����ð����� ���� ���� Ǯ�� ������ ���� ����
    IEnumerator SpawnCool()
    {
        isSpawnAble = false;

        //TimeLine.cs�� NextPool �Լ�ȣ��
        SelectingTimeLine.GetComponent<TimeLine>().NextPool();

        //SetSpawnPoolTime(30��) ���� ���
        yield return new WaitForSeconds(SetSpawnPoolTime);
        isSpawnAble = true;
    }

   
    #region GameOverUI
    //---(GameOverUI)----

    public void gotoMain()
    {
        SceneManager.LoadScene("SY_WorkScene"); //�ӽ÷� �� ��ε�
    }

    public void playerDie()
    {
        GameOverUI.SetActive(true);
    }
    #endregion
}
