using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCtrl : MonoBehaviour
{
    public int MonsterId;

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
    public  float   KnockBackPower;    //넉백 정도를 저장할 변수, 일단 1

    //현재 객체와 플레이어 캐릭터 사이의 거리
    private Vector3 toPcVec;

    [SerializeField]
    private GameObject HitEft;

    [SerializeField]
    private GameObject attackProjectile;

    public GameObject RewardBox;
    public GameObject Mimic;

    void Start()
    {
        //  미믹에 한해서 변신한 상태가 아니면
        //  플레이어 공격에 맞지 않도록 박스콜라이더 꺼주기
        if (MonsterId == 5 && isTrans == false)
        {
            BoxColliderOnOff(false);
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

        //  다른 몬스터의 경우 스프라이트가 오른쪽을 바라보고 있음
        //  그러나 미믹의 경우 스프라이트가 왼쪽을 바라보고 있음
        //  그래서 미믹만 따로 바라보는 방향을 반대로 해줘야 함.
        //  트랜스폼에서 y rotation 값을 180으로 하면 따로 코드로 방향 전환해줄 필요 없지만
        //  데미지 폰트 또한 반대로 출력되서 그렇게 되면 데미지 폰트 코드를 또 건들여야 됨.
        if (MonsterId == 5)
        {
            if (isTrans)
            { objPosXCal *= -1; }
            //  미믹이 변신하 않았을 때 플레이어를 바라볼 필요가 없으므로
            //  아예 0으로 고정시켜서 스프라이트 좌우반전 하지 않도록
            else
            { objPosXCal *= 0; }
        }
        if (!isDead)
        {
            if (objPosXCal < 0f)//이 객체가 플레이어보다 왼쪽에 있음, 0.00001이라도 0보다만 작으면
            {
                ThisSpriteRenderer.flipX = false;
            }
            else if (objPosXCal > 0f)//이 객체가 플레이어보다 오른쪽에 있음, 0.00001이라도 0보다만 크면
            {
                ThisSpriteRenderer.flipX = true;
            }
            else // 무조건 0이면
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

    //플레이어가 호출
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
            Instantiate(attackProjectile, transform.position, angleAxis1);
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
                Instantiate(attackProjectile, transform.position, angleAxis);
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

    IEnumerator Attack_F()
    {
        isSuperArmor = true;
        float temp = MonsterMoveSpeed;
        MonsterMoveSpeed = 0.0f;

        yield return new WaitForSeconds(0.2f);
        thisAnim.SetTrigger("Attack");
        thisAnim.SetBool("isDuringAnim", true);

        yield return new WaitForSeconds(0.9f);
        toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
             TraceTarget.transform.position.y - transform.position.y,
             0);

        //3갈래로 나가는 투사체를 4방향으로 발사 
        for (int j = 0; j < 4; j++)
        {
            float other = j * 90f;
            for (int i = -1; i < 2; i++)
            {
                float plusAngle = i * 15f + other;

                Quaternion angleAxis = Quaternion.Euler(
                    0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                    * Mathf.Rad2Deg + plusAngle);

                if (!isDead)
                {
                    Instantiate(attackProjectile, transform.position, angleAxis);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }

        //한번에 전 방향으로 발사
        for (int j = 0; j < 5; j++)
        {
            float plusRotate = j * 3f;

            for (int i = 0; i < 18; i++)
            {
                float plusAngle = i * 20f;
                float sum = plusAngle + plusRotate;

                Quaternion angleAxis = Quaternion.Euler(
                    0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                    * Mathf.Rad2Deg + sum);

                if (!isDead)
                {
                    Instantiate(attackProjectile, transform.position, angleAxis);
                }
            }

            yield return new WaitForSeconds(0.5f);
        }

        ////////////////////////////////
      
        
        // 4방향으로 조금씩 각도를 틀면서 원형으로 투사체를 발사
        for (int i = 0; i < 40; i++)
        {
            float plusAngle1 = i * 10f;
            float plusAngle2 = i * 10f + 90;
            float plusAngle3 = i * 10f + 180f;
            float plusAngle4 = i * 10f + 270f;

            Quaternion angleAxis1 = Quaternion.Euler(
                0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                * Mathf.Rad2Deg + plusAngle1);

            Quaternion angleAxis2 = Quaternion.Euler(
               0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
               * Mathf.Rad2Deg + plusAngle2);

            Quaternion angleAxis3 = Quaternion.Euler(
                0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
                * Mathf.Rad2Deg + plusAngle3);

            Quaternion angleAxis4 = Quaternion.Euler(
               0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x)
               * Mathf.Rad2Deg + plusAngle4);

            if (!isDead)
            {
                Instantiate(attackProjectile, transform.position, angleAxis1);
                Instantiate(attackProjectile, transform.position, angleAxis2);
                Instantiate(attackProjectile, transform.position, angleAxis3);
                Instantiate(attackProjectile, transform.position, angleAxis4);
            }

            yield return new WaitForSeconds(0.15f);
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
