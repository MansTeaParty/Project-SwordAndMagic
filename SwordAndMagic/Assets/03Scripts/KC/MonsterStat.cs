using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public int MonsterId;

    public  GameObject      DamageFont;
    private GameObject      TraceTarget;
    private SpriteRenderer  ThisSpriteRenderer; 
    private Animator        thisAnim;

    public  float   MonsterMoveSpeed;
    public  float   BaseMonsterMoveSpeed;
    public  int     attackDamageForText;         //DamageFontManage.cs에 전달될 텍스트
    private float   AttackCooltime;

    public  bool isDead;
    private bool isAttack = false;
    private bool isSuperArmor = false;

    public int      MonsterHP;
    public float    AttackRange;
    public int      MonsterPrivateCoolTime;
    public int      MonsterDamage;

    public float    MonsterDropRate;
    public int      MonsterExp;
    public float    KnockBackPower;    //넉백 정도를 저장할 변수, 일단 1

    //현재 객체와 플레이어 캐릭터 사이의 거리
    private Vector3 toPcVec;

    [SerializeField]
    private GameObject HitEft;

    [SerializeField]
    private GameObject attackProjectile;

    public GameObject RewardBox;

    void Start()
    {
        AttackCooltime = 5;
        thisAnim = GetComponent<Animator>();
        ThisSpriteRenderer = GetComponent<SpriteRenderer>();
        TraceTarget = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(Ability_Cool());
    }

    void Update()
    {
        Move();
        AttackMove();
    }

    void Move()
    {
        //이 객체 포지션 = moveToward써서 지정 방향으로 이동시킬 것
        //지정 방향 : TraceTarget 방향
        //new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0)
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0),
            MonsterMoveSpeed * Time.deltaTime);

        //몬스터가 바라볼 방향 결정
        float objPosXCal = transform.position.x - TraceTarget.transform.position.x;
        if (!isDead)
        {
            if (objPosXCal < 0f)//이 객체가 플레이어보다 왼쪽에 있음
            {
                ThisSpriteRenderer.flipX = false;
            }
            else//이 객체가 플레이어보다 오른쪽에 있음
            {
                ThisSpriteRenderer.flipX = true;
            }
        }
    }

    private void AttackMove()
    {
        if (isAttack && !isDead)
        {
            //골렘의 경우에 한하여 돌진 이동 처리
            //애니메이션 처리는 아래의 Attack_B 코루틴에서 처리
            //isAttack 변수가 false 였다가 공격 쿨타임에 맞춰 true로 전환되면
            //업데이트에 의해 돌진하는 이동처리가 진행
            if (MonsterId == 2)
                transform.position += toPcVec.normalized * 120.0f * Time.deltaTime;
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

            //이동 속도 0으로 변경
            MonsterMoveSpeed = 0;

            //사망처리를 했는데도 간혹 플레이어 공격에 맞는 경우가 있음.
            //collier 꺼주기
            GetComponents<BoxCollider2D>()[0].enabled = false;
            GetComponents<BoxCollider2D>()[1].enabled = false;

            
            thisAnim.SetBool("isDuringAnim", true);
            thisAnim.SetBool("isDead", true);
            thisAnim.SetTrigger("Die");

            StartCoroutine(ObjFlash());
        }
    }

    //오브젝트 점멸
    //일단 스프라이트 랜더러 껐다켜는 방식으로
    //시원찮으면 스프라이트 색을 흰색으로 변경하는 식으로 해볼듯
    IEnumerator ObjFlash()
    {
        yield return new WaitForSeconds(2.0f);

        for (int i = 0; i < 5; i++)
        {
            ThisSpriteRenderer.enabled = false;

            yield return new WaitForSeconds(0.3f);

            ThisSpriteRenderer.enabled = true;

            yield return new WaitForSeconds(0.3f);
        }

        for (int i = 0; i < 5; i++)
        {
            ThisSpriteRenderer.enabled = false;

            yield return new WaitForSeconds(0.15f);

            ThisSpriteRenderer.enabled = true;

            yield return new WaitForSeconds(0.15f);
        }

        CheckDropRate();
        Destroy(gameObject);
    }
    void CheckDropRate()
    {
        if (Random.Range(0, 1000) < MonsterDropRate)
        {
            Instantiate(RewardBox, transform.position, transform.rotation);
        }
    }


    /// <summary>
    /// 몬스터 종류별 공격
    /// </summary>
    /// Attack_B~~ -> 골렘
    /// Attack_C~~ -> Spiked슬라임
    /// Attack_D~~ -> Tentacle슬라임
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
                }
                AttackCooltime = MonsterPrivateCoolTime;
            }
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Ability_Cool());

    }

    //골렘 공격 절차
    IEnumerator Attack_B()
    {
        isSuperArmor = true;
        thisAnim.SetTrigger("Ready");
        float temp = MonsterMoveSpeed;
        MonsterMoveSpeed = 0.0f;

        yield return new WaitForSeconds(0.7f);

        toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x, 
             TraceTarget.transform.position.y - transform.position.y, 
             0);
        
        yield return new WaitForSeconds(0.3f);
        
        isAttack = true;
        thisAnim.SetTrigger("Attack");
        thisAnim.SetBool("isDuringAnim", true);

        yield return new WaitForSeconds(0.4f);

        isSuperArmor = false;
        isAttack = false;
        thisAnim.SetBool("isDuringAnim", false);
        MonsterMoveSpeed = temp;

        yield return new WaitForSeconds(0.5f);
    }


    //Spiked슬라임 공격 절차
    IEnumerator Attack_C() 
    {
        float temp = MonsterMoveSpeed;
        MonsterMoveSpeed = 0.0f;

        isSuperArmor = true;
        
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
        MonsterMoveSpeed = temp;

        yield return new WaitForSeconds(0.5f);
    }

    //Tentacle 슬라임 공격 절차
    IEnumerator Attack_D()
    {
        float temp = MonsterMoveSpeed;
        MonsterMoveSpeed = 0.0f;

        isSuperArmor = true;

        yield return new WaitForSeconds(0.2f);
        thisAnim.SetTrigger("Attack");
        thisAnim.SetBool("isDuringAnim", true);

        yield return new WaitForSeconds(0.9f);
        toPcVec = new Vector3
            (TraceTarget.transform.position.x - transform.position.x,
            TraceTarget.transform.position.y - transform.position.y, 0);

        for (int i = -1; i < 2; i++)
        {
            float plusAngle = i * 15f;

            Quaternion angleAxis = Quaternion.Euler(
                0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) 
                * Mathf.Rad2Deg + plusAngle);

            Instantiate(attackProjectile, transform.position, angleAxis);
        }
        yield return new WaitForSeconds(0.1f);
        
        isSuperArmor = false;
        thisAnim.SetBool("isDuringAnim", false);
        MonsterMoveSpeed = temp;

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

}
