using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;


    public GameObject TimeLineController;//Ÿ�Ӷ��� ��Ʈ�ѷ�
    public GameObject SelectingTimeLine; //���õ� Ÿ�Ӷ���

    private bool isSpawnAble;

    private int StageNum;

    private bool isGameStart;
    private bool isGameOver;

    //�ð� ����
    public int setTime=180;
    public int SetSpawnPoolTime=30;
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



    #region TimeLine

    void TimeCal()
    {
        //currentTime�� �ð��� ���� + ��Ŵ
        currentTime += Time.deltaTime;
        //Debug.Log("���� �ð� : " + (int)currentTime);

        //�� ����� �� -> SetSpawnPoolTime�� ����
        //������ : 1�ʵ��� ����Ǵ� �����̶�� ��
        //update�� ��û���� ���� ȣ�� �ϱ� ������ 
        //TimeLine.cs�� ����Ʈ �ε��� ���� ���޾� �ö�.
        if ((int)currentTime % SetSpawnPoolTime == 0 && isSpawnAble == true)
        {
            StartCoroutine(SpawnCool());
        }
    }


    //Ÿ�Ӷ��� ��Ʈ�ѷ��� ȣ���Ͽ� ������ Ÿ�Ӷ����� �������� �޾ƿ�. 
    void RecieveTimeLine(GameObject timeline)
    {
        SelectingTimeLine = timeline;
        //Debug.Log("SelectingTimeLine: " + SelectingTimeLine);
    }

    IEnumerator SpawnCool()
    {
        isSpawnAble = false;

        //Debug.Log(SetSpawnPoolTime + "�� ����");
        //Debug.Log("���� ���� Ǯ ����");
        //TimeLine.cs�� NextPool �Լ�ȣ��
        SelectingTimeLine.SendMessage("NextPool");

        //Debug.Log("nextpool ȣ��");
        yield return new WaitForSeconds(30.0f);
        isSpawnAble = true;
    }

    #endregion



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
