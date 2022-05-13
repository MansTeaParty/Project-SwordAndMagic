using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerCtrl PC;
    public GameObject GameOverUI;

    public GameObject TimeLineController;   // Ÿ�Ӷ��� ��Ʈ�ѷ�
    public GameObject SelectingTimeLine;    // ���õ� Ÿ�Ӷ���

    private bool isSpawnAble;
    private bool isGameStart;
    private bool isGameOver;

    //�ð� ����
    public int setTimeMinute = 15;
    public int setTimeSecond = 60;
    public int SetSpawnPoolTime = 30;

    private float currentTimeSecond;  // ��
    private int currentTimeMinute;  // ��
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

        // 15�� 1�ʿ��� ���� -> 30�� ������ ���� Ǯ�� �����Ǳ� ����
        // 15�� 0�ʿ��� �����ϸ� �ٷ� 59�ʰ� �ǹ��� -> ù��° ���� Ǯ �ȳ����� 30�� �ڿ��� ����.
        currentTimeMinute = setTimeMinute;
        currentTimeSecond = 1;

        StartCoroutine((Timer()));

        TimeLineController = GameObject.FindGameObjectWithTag("TimeLineController");
        // Ÿ�Ӷ��� ��Ʈ�ѷ����� Ÿ�Ӷ����� ������ ���� ����
        // �׷��� Ÿ�Ӷ��� ��Ʈ�ѷ��� SelectTimeLine�Լ����� Ÿ�Ӷ����� �����ϰ�
        // ������ Ÿ�Ӷ����� RecieveTimeLine�� ȣ���Ͽ� ����.
        // ���� Ÿ�Ӷ��� ������ SelectingTimeLine�� ����
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

        PoolText.enabled = true;
        yield return new WaitForSeconds(2f);
        PoolText.enabled = false;

        //SetSpawnPoolTime(30��) ���� ���
        yield return new WaitForSeconds(SetSpawnPoolTime - 2f);
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

    #region BossCutScene
    IEnumerator BossStart()
    {
        //  �÷��̾� �ӵ� �����ϰ� ���� �����ϸ� �ӵ� 0����, ���� ������ �����ֱ�
        //float pcOriginSpeed = PC.moveSpeed;
        //PC.moveSpeed = 0f;
        //PC.stop = true;

        /*Spawner.SetActive(false);
        All_UI.SetActive(false);
        Boss_UI.SetActive(true);
        skillManager.SetActive(false);
        cameraTracer.enabled = false;*/

        //  ������ ��ü�� ã��
        StartCoroutine(FoundObjects());
        yield return new WaitForSeconds(1.0f);

        Vector3 bossSpawnPos = PC.transform.position + new Vector3(0, 60, 0);
        GameObject b_Boss = Instantiate(Boss, bossSpawnPos, Quaternion.identity);

        //  ī�޶� ����
        /*for (int i = 0; i < 75; i++)
        {
            //sound.volume -= 0.0015f;
            //CutScenePanelDown.transform.Translate(Vector2.up * 3f);
            //CutScenePanelUp.transform.Translate(Vector2.down * 3f);
            //Camera_Main.transform.Translate(Vector2.up);
            yield return new WaitForSeconds(0.02f);
        }*/

        //  ���� �ɾ�� ����
        b_Boss.GetComponent<MonsterCtrl>().MonsterMoveSpeed = 10f;
        b_Boss.GetComponent<Animator>().SetTrigger("Move");

        yield return new WaitForSeconds(3f);
        b_Boss.GetComponent<MonsterCtrl>().MonsterMoveSpeed = 0f;
        b_Boss.GetComponent<Animator>().SetTrigger("Idle");
        yield return new WaitForSeconds(0.5f);



        //  ���� ��ǳ��
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

        //  �� ����(= �̵� ����)
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

        //  MonsterStat.cs�� ������ ��ġ�� ����
        //  �׷��� MonsterStat.cs���� ��ġ�� �����ϰ� �װ��� �������� ������ �ڷ���Ʈ ������ ����ϴµ� ���


        //  �÷��̾� �̵��ӵ� �����ְ�
        //PC.moveSpeed = pcOriginSpeed;
        yield return new WaitForSeconds(1.0f);

        //���� ���� ����
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

        //  ���� �����, ����ġ ���� ���� ����.
        //  ���� ����Ʈ�� ���� ��ü ���� ã�Ƽ� 9999������ �༭ ����ϰ� ��
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
