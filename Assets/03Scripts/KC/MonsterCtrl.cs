using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    public int MonsterId;
    [SerializeField]
    private GameObject PC;

    public  GameObject      DamageFont;
    private GameObject      TraceTarget;
    private SpriteRenderer  ThisSpriteRenderer; 
    private Animator        thisAnim;

    public  float   MonsterMoveSpeed;           // 현재 몬스터 이동속도
    public  float   BaseMonsterMoveSpeed;       // 기본 이동속도 속성 값
    public  int     attackDamageForText;        // DamageFontManage.cs에 전달될 텍스트
    private float   AttackCooltime;
    private float   objPosXCal;

    public  bool    isDead;
    private bool    isAttack        = false;
    private bool    isSuperArmor    = false;
    public  bool    isTrans;

    public  int     MonsterHP;
    public  float   AttackRange;
    public  int     MonsterPrivateCoolTime;
    public  int     MonsterDamage;

    public  int     MonsterExp;
    public  float   MonsterDropRate;
    public  float   KnockBackPower;    //넉백 정도를 저장할 변수

    //현재 객체와 플레이어 캐릭터 사이의 거리
    private Vector3 toPcVec;

    [SerializeField]
    private GameObject HitEft;

    public GameObject RewardBox;
    public GameObject Mimic;

    [SerializeField]
    private GameObject attackProjectile_0; // 투사제
    [SerializeField]
    private GameObject attackProjectile_1; // 보스 장판 공격

    [SerializeField]
    private GameObject TelePortEffect;  //  텔레포트 이펙트
    private Vector3 TelePortPoint;      //  텔레포트를 위해 기준점을 잡을 필요가 있음
    public int SelectAttackNum;
    public List<GameObject> TileList;

    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player");

        //  미믹에 한해서 변신한 상태가 아니면
        //  플레이어 공격에 맞지 않도록 박스콜라이더 꺼주기
        if (MonsterId == 5 && isTrans == false)
        {
            BoxColliderOnOff(false);
        }

        //  보스는 항상 넉백당하지 않음
        if (MonsterId == 6)
        {
            isSuperArmor = true;
        }

        AttackCooltime = 5;
        thisAnim = GetComponent<Animator>();
        ThisSpriteRenderer = GetComponent<SpriteRenderer>();
        TraceTarget = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(Ability_Cool());
    }

    void Update()
    {
        Move();
        MonsterAbility();
    }

    void Move()
    {
        if(MonsterId == 5 && isTrans == false)
        {
            MonsterMoveSpeed = 0f;
        }

        //  이 객체 포지션을 moveToward 써서 new Vector3 방향으로 이동시킬 것
        //  new Vector3 = TraceTarget의 포지션
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0),
            MonsterMoveSpeed * Time.deltaTime);

        //  몬스터가 바라볼 방향 결정
        
        objPosXCal = transform.position.x - TraceTarget.transform.position.x;

        if (MonsterId == 5 || MonsterId == 6)
        {
            if (isTrans)
            { objPosXCal *= -1; }
            else
            { objPosXCal *= 0; }
        }

        if (!isDead)
        {
            if (objPosXCal < 0f)
            {
                ThisSpriteRenderer.flipX = false;
            }
            else if (objPosXCal > 0f)
            {
                ThisSpriteRenderer.flipX = true;
            }
            else
            { }
        }
    }

    private void MonsterAbility()
    {
        if (isAttack && !isDead)
        {
            //  골렘의 경우에 한하여 돌진 이동 처리
            //  애니메이션 처리는 아래의 Attack_B 코루틴에서 처리
            //  isAttack 변수가 false 였다가 공격 쿨타임에 맞춰 true로 전환되면
            //  업데이트에 의해 돌진하는 이동처리가 진행
            if (MonsterId == 2)
            {
                transform.position += toPcVec.normalized * 120.0f * Time.deltaTime;
            }
        }

        if (MonsterId == 5 && isTrans == false)
        {
            //  미믹은 골렘과 다르게 플레이어의 실시간 위치를 추적해서 일정거리 내에 들어오면 변신함.
            toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
            TraceTarget.transform.position.y - transform.position.y,
            0);

            if (toPcVec.magnitude < 12f)
            {
                StartCoroutine(MimicTrans());
                isTrans = true;
            }
        }
    }

    //플레이어 공격이 호출
    public void Hit(int attackDamage)
    {
        Vector2 knockBackVec = new Vector2(transform.position.x - TraceTarget.transform.position.x, transform.position.y - TraceTarget.transform.position.y);
        StartCoroutine(KnockBack(knockBackVec, attackDamage));
    }
    
    IEnumerator KnockBack(Vector2 m_knockBackVec, int m_attackDamage)
    {
        float _knockBackPower = KnockBackPower;
        float temp = MonsterMoveSpeed;          // 넉백을 위해 정지하고자 속도를 저장
        MonsterMoveSpeed = 0f;
        attackDamageForText = m_attackDamage;   // damagefontmanager에게 전달할 텍스트는 m_attackDamage
        Instantiate(HitEft, transform);         // 히트 이펙트 출력
        StartCoroutine(MakeDamageFont());       // 데미지 폰트 생성하는 코루틴

        //체력 계산
        MonsterHP -= m_attackDamage;

        if (!isAttack && !isSuperArmor)
        {
            thisAnim.SetTrigger("Hit");
            thisAnim.SetBool("isDuringAnim", true);

            for (int i = 0; i < 10; i++)
            {
                transform.position += (Vector3)m_knockBackVec.normalized * _knockBackPower;
                _knockBackPower -= KnockBackPower / 10.0f;
                yield return new WaitForSeconds(0.01f);
            }
        }
        thisAnim.SetBool("isDuringAnim", false);

        MonsterMoveSpeed = temp;

        isMonsterHPzero();
    }

    IEnumerator MakeDamageFont()
    {
        Vector2 pos = new Vector2(0f, 0.1f);

        GameObject objDamageFont = Instantiate(DamageFont, pos, Quaternion.identity);
        objDamageFont.transform.SetParent(this.gameObject.transform, false);

        yield return new WaitForSeconds(0.0f);
    }

    void isMonsterHPzero()
    {
        if (MonsterHP <= 0)
        {
            isAttack = false;
            isDead = true;
            
            MonsterMoveSpeed = 0;

            //사망처리를 했는데도 간혹 플레이어 공격에 맞는 경우가 있음.
            //collier 꺼주기
            BoxColliderOnOff(false);
            
            thisAnim.SetBool("isDuringAnim", true);
            thisAnim.SetBool("isDead", true);
            thisAnim.SetTrigger("Die");

            StartCoroutine(ObjFlash());
        }
    }

    //  오브젝트 점멸
    //  스프라이트 랜더러 껐다켜는 방식
    IEnumerator ObjFlash()
    {
        yield return new WaitForSeconds(1.0f);

        //  미믹 이외의 몬스터만 사망시 점멸하도록
        if (!(MonsterId == 5))
        {
            float waitTime = 0.3f;
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    ThisSpriteRenderer.enabled = false;
                    yield return new WaitForSeconds(waitTime);
                    ThisSpriteRenderer.enabled = true;
                    yield return new WaitForSeconds(waitTime);
                }
                waitTime = 0.15f;
            }
        }

        CheckDropRate();
        Destroy(gameObject);
    }
    void CheckDropRate()
    {
        if (!(MonsterId == 5))
        {
            int rand = Random.Range(0, 1000);       // 0~999
            int objselect = Random.Range(0, 10);    // 0~9

            GameObject RewardORMimic;

            if (objselect < 6)
            { RewardORMimic = RewardBox; }
            else
            { RewardORMimic = Mimic; }

            //  랜덤값이 몬스터 각각의 드롭율 값보다 작을 경우 미믹 or 보상 상자 드랍
            if (rand < MonsterDropRate)
            {
                Instantiate(RewardORMimic, transform.position, transform.rotation);
            }
        }
        else if (MonsterId == 5)
        {
            Instantiate(RewardBox, transform.position, transform.rotation);
        }
        else { }
    }


    /// <summary>
    /// 몬스터 종류별 공격
    /// </summary>
    /// Attack_B~~ -> 골렘
    /// Attack_C~~ -> Spiked슬라임
    /// Attack_D~~ -> Tentacle슬라임
    /// MimicTrans -> 미믹 변신
    /// <returns></returns>
    IEnumerator Ability_Cool() 
    {
        if (Vector3.Distance(transform.position, TraceTarget.transform.position) <= AttackRange)
        {
            AttackCooltime -= 1;
            if (AttackCooltime <= 0 && !isDead)
            {
                switch (MonsterId)
                {
                    case 1:
                        break;

                    case 2:
                        StartCoroutine(Attack_B());
                        break;

                    case 3:
                        StartCoroutine(Attack_C());
                        break;

                    case 4:
                        StartCoroutine(Attack_D());
                        break;

                    case 5:
                        break;
                }
                AttackCooltime = MonsterPrivateCoolTime;
            }
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Ability_Cool());
    }

    //  골렘 공격 절차
    IEnumerator Attack_B()
    {
        isSuperArmor = true;
        thisAnim.SetTrigger("Ready");
        float temp = BaseMonsterMoveSpeed;
        MonsterMoveSpeed = 0.0f;

        yield return new WaitForSeconds(0.7f);

        //  toPcVec값을 여기서 초기화 해주는 이유
        //  골렘이 돌진 준비를 하는 자세에서 0.7초 동안 플레이어의 위치를 추적
        //  isAttack이 true가 되는 순간 업데이트에 의해 돌진이 수행될 것인데
        //  0.3초 뒤 플레이어는 골렘이 0.7초 동안 추적한 마지막 위치에서 벗어나 있을 것임.
        //  즉, 여기서 toPcVec값을 초기화하지 않고 update에 썼을 경우
        //  골렘이 플레이어의 실시간 위치로 돌진하기 때문에 플레이어 입장에서는 피하기 너무 어려움.
        if (!isDead)
        {
            toPcVec = new Vector3
                (TraceTarget.transform.position.x - transform.position.x,
                 TraceTarget.transform.position.y - transform.position.y,
                 0);
        }
        yield return new WaitForSeconds(0.3f);
        
        isAttack = true;
        thisAnim.SetTrigger("Attack");
        thisAnim.SetBool("isDuringAnim", true);

        yield return new WaitForSeconds(0.4f);

        isSuperArmor = false;
        isAttack = false;
        thisAnim.SetBool("isDuringAnim", false);

        if(!isDead)
        { MonsterMoveSpeed = temp; }
        else
        { MonsterMoveSpeed = 0.0f; }

        yield return new WaitForSeconds(0.5f);
    }

    //  Spiked슬라임 공격 절차
    IEnumerator Attack_C() 
    {
        isSuperArmor = true;
        float temp = BaseMonsterMoveSpeed;
        MonsterMoveSpeed = 0.0f;

        yield return new WaitForSeconds(0.2f);

        thisAnim.SetTrigger("Attack");
        thisAnim.SetBool("isDuringAnim", true);

        yield return new WaitForSeconds(0.9f);
        if (!isDead)
        {
            toPcVec = new Vector3
               (TraceTarget.transform.position.x - transform.position.x,
                TraceTarget.transform.position.y - transform.position.y,
                0);

            Quaternion angleAxis1 = Quaternion.Euler(0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) * Mathf.Rad2Deg);
            Instantiate(attackProjectile_0, transform.position, angleAxis1);
        }
        yield return new WaitForSeconds(0.1f);

        isSuperArmor = false;
        thisAnim.SetBool("isDuringAnim", false);

        if (!isDead)
        { MonsterMoveSpeed = temp; }
        else
        { MonsterMoveSpeed = 0.0f; }

        yield return new WaitForSeconds(0.5f);
    }

    //  Tentacle 슬라임 공격 절차
    IEnumerator Attack_D()
    {
        isSuperArmor = true;
        float temp = BaseMonsterMoveSpeed;
        MonsterMoveSpeed = 0.0f;

        yield return new WaitForSeconds(0.2f);
        thisAnim.SetTrigger("Attack");
        thisAnim.SetBool("isDuringAnim", true);

        yield return new WaitForSeconds(0.9f);
        toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
             TraceTarget.transform.position.y - transform.position.y,
             0);

        //  for문 3번 돌림 -1, 0, 1
        for (int i = -1; i < 2; i++)
        {
            //  i에 15를 곱함으로서
            //  -15도, 0도, 15도 의 각도를 설정하고 
            float plusAngle = i * 15f;

            //  방향을 정한 뒤
            Quaternion angleAxis = Quaternion.Euler(
                0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) 
                * Mathf.Rad2Deg + plusAngle);

            //  투사체 생성
            if (!isDead)
            {
                Instantiate(attackProjectile_0, transform.position, angleAxis);
            }
        }
        yield return new WaitForSeconds(0.1f);
        
        isSuperArmor = false;
        thisAnim.SetBool("isDuringAnim", false);

        if (!isDead)
        { MonsterMoveSpeed = temp; }
        //특수 공격중 몬스터가 사망했을 경우
        else
        { MonsterMoveSpeed = 0.0f; }

        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator MimicTrans()
    {
        thisAnim.SetBool("Trans", true);
        yield return new WaitForSeconds(0.2f);
        thisAnim.SetBool("Trans", false);

        yield return new WaitForSeconds(0.2f);
        //  변신 했으니깐 꺼져 있던 박스 콜라이더 켜주기
        BoxColliderOnOff(true);

        //  변신했으니깐 이동하기 위해 이동속도 변수 초기화
        MonsterMoveSpeed = BaseMonsterMoveSpeed;
    }

    /// <summary>
    /// 보스 공격 패턴
    /// </summary>

    //  보스 행동 시작 
    //  GM에서 타이머가 0되면 호출할 것
    public void BossPattern(Vector3 BossPos)
    {
        // 보스의 현재 위치를 받아옴.
        // 보스 생성이후 플레이어 방향으로 3초정도 걸어옴.
        // 그 시점 이후의 보스 위치를 말하는 것.
        // 이유 : 컷신 이후 보스 위치 기준으로 벽이 생성되는데 벽 내부에서만 
        // 텔레포트하면서 공격하기 위해
        TelePortPoint = BossPos;

        //  보스 공격 코루틴 시작
        StartCoroutine(BaseAttack_Boss());          // 보스 기본 공격 - 플레이어 위치에 장판 공격 한개 생성
        StartCoroutine(SpecialAttack_Boss());       // 보스 특수 공격 - 텔레포트 이후 공격 선택
    }

    IEnumerator SpecialAttack_Boss()
    {
        StartCoroutine(TelePort());
        yield return new WaitForSeconds(1.5f);

        SelectAttack();
        yield return new WaitForSeconds(1.5f);
    }

    // 텔레포트
    IEnumerator TelePort()
    {
        TelePortEffect.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        TelePortEffect.SetActive(false);

        float randx = Random.Range(-1, 2);  //  -1  or  0 or 1
        float randy = Random.Range(-1, 2);
        float posx = randx * 100;           //  -100 or 0 or 100
        float posy = randy * 60;            //  -60  or 0 or 60

        //  텔레포트 위치 구하기
        Vector3 teleportpos = TelePortPoint + new Vector3(posx, posy, 0);
        Vector3 isnewpos = teleportpos;

        //  만약 현재 있는 위치랑 새로운 텔레포트 위치가 같으면 
        if (transform.position == isnewpos)
        { StartCoroutine(TelePort()); } // 새로운 위치 구하기
        // 같지 않으면 계산한 위치로 이동
        else
        { transform.position = teleportpos; }
        yield return new WaitForSeconds(1.0f);
    }

    void SelectAttack()
    {
        //SelectAttackNu = Random.Range(0, 6);
        StartCoroutine(Boss_AttackKinds(SelectAttackNum));
    }

    IEnumerator BaseAttack_Boss()
    { 
        // 플레이어 현재 위치에 장판 공격 한개 생성하는 보스 기본 공격
        Instantiate(attackProjectile_1, TraceTarget.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(2.5f, 3.5f));
        StartCoroutine(BaseAttack_Boss());
    }

    IEnumerator Boss_AttackKinds(int attacknum)
    {
        thisAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.8f);

        switch (attacknum)
        {
            case 0: // 투사체를 전방향으로 발사
                for (int j = 0; j < 15; j++)
                {
                    float plusRotate = j * 3f;

                    for (int i = 0; i < 20; i++)
                    {
                        float plusAngle = i * 18f;
                        float sum = plusAngle + plusRotate + Random.Range(-15, 16);

                        Quaternion angleAxis = Quaternion.Euler(
                            0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                            * Mathf.Rad2Deg + sum);

                        if (!isDead)
                        {
                            Instantiate(attackProjectile_0, transform.position, angleAxis);
                        }
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                break;

            case 1: //  상하좌우 4방향으로 3갈래로 나가는 투사체를 각도를 더해주면서 발사
                for (int k = 0; k < 20; k++) //총 20줄을 발사할건데
                {
                    float plusAngle2 = k * 3; // 1줄 쏘면 다음 줄은 3도 만큼 더해진 방향으로 쏠거다.

                    for (int j = 0; j < 4; j++) // 그 20번을 4번(4방향으로) 쏠것이다.
                    {
                        float other = j * 90f; // j에 90도를 곱해주면 상하좌우 방향이 나오게 됨.
                        for (int i = -1; i < 2; i++) // i가 -1,0,1에 대하여
                        {
                            float plusAngle = i * 15f + other; //15도씩 곱해줌으로서 -15도, 0도, +15도 -> 3갈래로 나가게 할것이다.

                            Quaternion angleAxis = Quaternion.Euler(
                                0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                                * Mathf.Rad2Deg + plusAngle + plusAngle2);

                            if (!isDead)
                            {
                                Instantiate(attackProjectile_0, transform.position, angleAxis);
                            }
                        }
                        yield return new WaitForSeconds(0.01f);
                    }
                    yield return new WaitForSeconds(0.2f);
                }
                break;

            case 2: // 상하좌우 4방향으로 한줄기로 나가는 투사체를 각도를 더해주면서 발사
                for (int i = 0; i < 60; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        float plusAngle = i * 10f + (j * 90); // i*10 ->다음 줄이 나갈 방향의 추가 각도, j*90 -> 방향

                        Quaternion angleAxis = Quaternion.Euler(
                        0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                        * Mathf.Rad2Deg + plusAngle);

                        if (!isDead)
                        {
                            Instantiate(attackProjectile_0, transform.position, angleAxis);
                        }
                    }
                    yield return new WaitForSeconds(0.15f);
                }
                break;

            case 3: // 대규모 장판 공격
                for (int j = 0; j < 5; j++) // 5번
                {
                    for (int i = 0; i < 20; i++) //25번
                    {
                        // 플레이어 현재 위치 기준
                        // x 범위 얼마, y범위 얼마의 값을 랜덤으로 구해서
                        // 해당 위치에 뿌림
                        Vector3 Pos = TraceTarget.transform.position + new Vector3(Random.Range(-60, 60), Random.Range(-60, 60), 0);
                        
                        if (!isDead)
                        {
                            Instantiate(attackProjectile_1, Pos, Quaternion.identity);
                        }

                        yield return new WaitForSeconds(Random.Range(0.05f, 0.1f));
                    }
                    yield return new WaitForSeconds(0.05f);
                }
                break;


            case 4:
                for (int i = 1; i < 60; i++)
                {
                    if (i % 15 == 0)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            float plusRotate = j * 5f;
                            for (int k = 0; k < 36; k++)
                            {
                                float plusAngle = k * 10f;
                                float sum = plusAngle + plusRotate;

                                Quaternion angleAxis = Quaternion.Euler(
                                    0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                                    * Mathf.Rad2Deg + sum);

                                if (!isDead)
                                {
                                    Instantiate(attackProjectile_0, transform.position, angleAxis);
                                }
                            }
                            yield return new WaitForSeconds(0.15f);
                        }
                        yield return new WaitForSeconds(0.15f);
                    }

                    else
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            float plusAngle = i * 10f + (j * 90); // i*10 ->다음 줄이 나갈 방향의 추가 각도, j*90 -> 방향

                            Quaternion angleAxis = Quaternion.Euler(
                            0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                            * Mathf.Rad2Deg + plusAngle);

                            if (!isDead)
                            {
                                Instantiate(attackProjectile_0, transform.position, angleAxis);
                            }
                        }
                        yield return new WaitForSeconds(0.15f);
                    }
                }
                break;

            case 5:
                toPcVec = new Vector3
                    (TraceTarget.transform.position.x - transform.position.x,
                     TraceTarget.transform.position.y - transform.position.y,
                     0);

                int tilecount = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        float plusAngle = j * 20f;

                        Quaternion angleAxis = Quaternion.Euler(
                        0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                        * Mathf.Rad2Deg + plusAngle);

                        if (!isDead)
                        {
                            GameObject tile = Instantiate(attackProjectile_0, transform.position, angleAxis);
                            TileList.Add(tile);
                        }
                        tilecount++; // -> 15가 될 것
                    }
                    yield return new WaitForSeconds(0.4f);
                }
                yield return new WaitForSeconds(1.0f);
                
                // 리스트에 담긴 객체들의 속도를 0으로
                for (int j = 0; j < tilecount; j++)
                {
                    TileList[j].GetComponent<EnemyProjectile>().TileSpeed = 0;
                }
                yield return new WaitForSeconds(1.0f);
                
                //  리스트에 담긴 객체들에게 쫓을 타겟의 위치와 속도 전달
                for (int k = 0; k < tilecount; k++)
                {
                    Debug.Log(tilecount);
                    float tilespeed = TileList[k].GetComponent<EnemyProjectile>().BaseTileSpeed *= 1.8f;
                    TileList[k].GetComponent<EnemyProjectile>().SetTarget(true, tilespeed);
                    yield return new WaitForSeconds(0.1f);
                }

                //리스트에 있는 객체 정보를 삭제
                TileList.Clear();
                break;
                
            default:
                break;
        }
        thisAnim.SetTrigger("Idle");
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpecialAttack_Boss());
    }

    void GiveExpToPlayer()
    {
        //그냥 숫자만 전달 -> 플레이어 내부적으로 갱신하는 함수를 불러야할 수도 있음.
        TraceTarget.GetComponent<PlayerCtrl>().playerExp += MonsterExp;

        //or
        //TraceTarget.GetComponent<PlayerCtrl>().AcceptExp(MonsterExp);
        //처럼 AcceptExp(MonsterExp) 함수를 호출

        //경험치를 전달하면 UI 상으로 경험치 바가 변하는 것을 보여줄 수 있도록 갱신해줘야 함.

        // 1. 단순히 숫자만 전달할 경우
        // 숫자 전달 + PlayerCtrl.cs가 가진 갱신하는 함수를 호출
        // -> 번거롭다 그냥 전달과 갱신을 동시에 하면 될 듯함.
        
        // 2. 그냥 함수 호출
        // 위처럼 함수 호출해서 경험치도 전달하고 그 함수 내에 갱신하는 로직도 작성

    }

    void BoxColliderOnOff(bool val)
    {
        GetComponents<BoxCollider2D>()[0].enabled = val;
        GetComponents<BoxCollider2D>()[1].enabled = val;
    }
}
