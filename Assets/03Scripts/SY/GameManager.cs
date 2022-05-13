using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerCtrl PC;
    public GameObject GameOverUI;

    public GameObject TimeLineController;   // 타임라인 컨트롤러
    public GameObject SelectingTimeLine;    // 선택된 타임라인

    private bool isSpawnAble;
    private bool isGameStart;
    private bool isGameOver;

    //시간 설정
    public int setTimeMinute = 15;
    public int setTimeSecond = 60;
    public int SetSpawnPoolTime = 30;

    private float currentTimeSecond;  // 초
    private int currentTimeMinute;  // 분
    public Text TimeText;
    public Text PoolText;

    [SerializeField]
    private GameObject Boss;

    [SerializeField]
    private List<GameObject> Founds;


    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl>();

        isGameOver = false;
        isGameStart = true;

        PoolText.enabled = false;

        // 15분 1초에서 시작 -> 30초 단위로 몬스터 풀이 생성되기 위해
        // 15분 0초에서 시작하면 바로 59초가 되버림 -> 첫번째 몬스터 풀 안나오고 30초 뒤에나 나옴.
        currentTimeMinute = setTimeMinute;
        currentTimeSecond = 1;

        StartCoroutine((Timer()));

        TimeLineController = GameObject.FindGameObjectWithTag("TimeLineController");
        // 타임라인 컨트롤러에게 타임라인을 선택할 것을 지시
        // 그러면 타임라인 컨트롤러가 SelectTimeLine함수에서 타임라인을 선택하고
        // 선택한 타임라인을 RecieveTimeLine을 호출하여 전달.
        // 받은 타임라인 정보를 SelectingTimeLine에 저장
        TimeLineController.GetComponent<TimeLineController>().SelectTimeLine();

        isSpawnAble = true;
    }

    void Update()
    { }

    #region Timer
    void ShowTimeText()
    {
        TimeText.color = new Color(255, 255, 255);

        if (currentTimeMinute < 10)
        {
            TimeText.text = "0" + currentTimeMinute + " : " + currentTimeSecond;

            if (currentTimeSecond < 10)
            {
                TimeText.text = "0" + currentTimeMinute + " : " + "0" + currentTimeSecond;
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
        if (currentTimeSecond == 0f && currentTimeMinute == 0f)
        {
            StartCoroutine(BossStart());
            yield break;
        }

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

        PoolText.enabled = true;
        yield return new WaitForSeconds(2f);
        PoolText.enabled = false;

        //SetSpawnPoolTime(30초) 동안 대기
        yield return new WaitForSeconds(SetSpawnPoolTime - 2f);
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

    #region BossCutScene
    IEnumerator BossStart()
    {
        //  플레이어 속도 저장하고 연출 시작하면 속도 0으로, 연출 끝나면 돌려주기
        //float pcOriginSpeed = PC.moveSpeed;
        //PC.moveSpeed = 0f;
        //PC.stop = true;

        /*Spawner.SetActive(false);
        All_UI.SetActive(false);
        Boss_UI.SetActive(true);
        skillManager.SetActive(false);
        cameraTracer.enabled = false;*/

        //  삭제할 객체를 찾기
        StartCoroutine(FoundObjects());
        yield return new WaitForSeconds(1.0f);

        Vector3 bossSpawnPos = PC.transform.position + new Vector3(0, 60, 0);
        GameObject b_Boss = Instantiate(Boss, bossSpawnPos, Quaternion.identity);

        //  카메라 연출
        /*for (int i = 0; i < 75; i++)
        {
            //sound.volume -= 0.0015f;
            //CutScenePanelDown.transform.Translate(Vector2.up * 3f);
            //CutScenePanelUp.transform.Translate(Vector2.down * 3f);
            //Camera_Main.transform.Translate(Vector2.up);
            yield return new WaitForSeconds(0.02f);
        }*/

        //  보스 걸어가는 연출
        b_Boss.GetComponent<MonsterCtrl>().MonsterMoveSpeed = 10f;
        b_Boss.GetComponent<Animator>().SetTrigger("Move");

        yield return new WaitForSeconds(3f);
        b_Boss.GetComponent<MonsterCtrl>().MonsterMoveSpeed = 0f;
        b_Boss.GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(0.5f);



        //  보스 말풍선
        /*yield return new WaitForSeconds(0.5f);
        BossSpeechBox.SetActive(true);

        BossSpeechText_1 = "Another foolish mortal has returned.";
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            BossSpeech.text = BossSpeechText_1.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.0f);

        BossSpeechText_1 = "I wonder what kind of scream you will die with.";
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            BossSpeech.text = BossSpeechText_1.Substring(0, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1.0f);

        BossSpeechText_1 = "You'll find out soon...";
        for (int i = 0; i <= BossSpeechText_1.Length; i++)
        {
            BossSpeech.text = BossSpeechText_1.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        */

        //  벽 생성(= 이동 제한)
        /*Instantiate(BossWall_RL, b_Boss.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1.0f);*/


        /*BossSpeechBox.SetActive(false);
        sound.clip = BossBGM;
        sound.Play();
        for (int i = 0; i < 75; i++)
        {
            sound.volume += 0.0015f;
            CutScenePanelDown.transform.Translate(Vector2.down * 3f);
            CutScenePanelUp.transform.Translate(Vector2.up * 3f);
            Camera_Main.transform.Translate(Vector2.down);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.5f);
        All_UI.SetActive(true);
        Boss_HpBar_UISEt.SetActive(true);
        skillManager.SetActive(true);
        StartCoroutine(skillManager.GetComponent<SkillManagement>().CooltimeProcess());
        cameraTracer.enabled = true;*/

        //b_Boss.GetComponent<MonsterStat>().BossHpBar = Boss_HpBar;

        //  MonsterStat.cs에 보스의 위치를 전달
        //  그러면 MonsterStat.cs에서 위치를 저장하고 그것을 기준으로 보스의 텔레포트 범위를 계산하는데 사용


        //  플레이어 이동속도 돌려주고
        //PC.moveSpeed = pcOriginSpeed;
        yield return new WaitForSeconds(1.0f);

        //보스 패턴 시작
        b_Boss.GetComponent<MonsterCtrl>().BossPattern(b_Boss.transform.position);

        //PC.stop = false;
    }
    IEnumerator FoundObjects()
    {
        yield return new WaitForSeconds(0.01f);
        Founds = new List<GameObject>(GameObject.FindGameObjectsWithTag("MonsterPool"));
        foreach (GameObject found in Founds)
        {
            Destroy(found.gameObject);
        }

        Founds = new List<GameObject>(GameObject.FindGameObjectsWithTag("MonsterProjectile"));
        foreach (GameObject found in Founds)
        {
            Destroy(found.gameObject);
        }

        Founds = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerAttack"));
        foreach (GameObject found in Founds)
        {
            Destroy(found.gameObject);
        }

        //  몬스터 드롭율, 경험치 제공 하지 않음.
        //  몬스터 리스트에 몬스터 객체 전부 찾아서 9999데미지 줘서 사망하게 함
        Founds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        foreach (GameObject found in Founds)
        {
            found.GetComponent<MonsterCtrl>().MonsterDropRate = 0;
            found.GetComponent<MonsterCtrl>().MonsterExp = 0;
            if (!found.GetComponent<MonsterCtrl>().isDead)
            {
                found.GetComponent<MonsterCtrl>().Hit(9999);
            }
        }
    }

    #endregion
}
