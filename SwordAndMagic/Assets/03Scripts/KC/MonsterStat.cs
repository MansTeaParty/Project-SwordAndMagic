using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public int MonsterId;
    //public enum MonsterState {MonsterIdle, MonsterRun, MonsterAttack, MonsterDie, MonsterHit };
    //public MonsterState Monster_State;

    private GameObject TraceTarget;
    private SpriteRenderer ThisSpriteRenderer; 
    private Animator thisAnim;

    public string MonsterName;
    public string Attack_Type; // 근접 :Meele, 원거리 : Range

    public float Attack_Range; //근접은 0
    public float AttackCooltime;
    public int MonsterHP;
    public int MonsterDamage;
    public float MonsterMoveSpeed;
    public float MonsterDropRate;
    public int MonsterExp;
    public float KnockBackPower;    //넉백 정도를 저장할 변수

    public GameObject DamageFont;
    public int attackDamageForText; //DamageFontManage.cs에 전달

    //현재 객체와 플레이어 캐릭터 사이의 거리
    private Vector3 toPcVec;

    //public bool isHit;
    public bool isDead;
    private bool isAttack = false;
    private bool isSuperArmor = false;

    [SerializeField]
    private GameObject HitEft;

    [SerializeField]
    private GameObject attackProjectile;

    public GameObject RewardBox;

    void Start()
    {
        AttackCooltime = 5;
        ThisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisAnim = GetComponent<Animator>();
        TraceTarget = GameObject.FindGameObjectWithTag("Player");

        if (MonsterId == 2) // 골렘
        {
            if(!isDead)
                StartCoroutine(Attack_B_Cool());
        }
        if (MonsterId == 3) // 슬라임
        {
            if (!isDead)
                StartCoroutine(Attack_C_Cool());
        }

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

        //이 객체가 플레이어보다 왼쪽에 있음
        if (objPosXCal < 0f)
        {
            ThisSpriteRenderer.flipX = false;
        }
        //이 객체가 플레이어보다 오른쪽에 있음
        else //if (objPosXCal > 3.0f)
        {
            ThisSpriteRenderer.flipX = true;
        }
    }

    private void AttackMove()
    {
        if (isAttack && !isDead)
        {
            //골렘의 경우에 한하여 
            //돌진 이동 처리
            //애니메이션 처리는 따로 진행
            //isAttack 변수가 false 였다가 공격 쿨타임에 맞춰 true로 전환되면
            //업데이트에 의해 돌진하는 이동처리가 진행
            if (MonsterId == 2)
                transform.position += toPcVec.normalized * 120.0f * Time.deltaTime;
        }
    }


    void NowMonsterState()
    {
        /*switch (Monster_State)
        {
            case MonsterState.MonsterIdle:
                if (isRunAble == true)
                {
                    Monster_State = MonsterState.MonsterRun;
                }
                break;

            case MonsterState.MonsterRun:
                thisAnim.SetBool("Run", true);
                Move();
                break;

            case MonsterState.MonsterAttack:
                isRunAble = false;
                thisAnim.SetBool("Run", false);

                break;

            case MonsterState.MonsterDie:
                isRunAble = false;
                thisAnim.SetBool("Run", false);


                break;

            case MonsterState.MonsterHit:
                break;


            default:
                break;
        }*/
    }
   
    //플레이어가 호출
    public void Hit(int attackDamage)
    {
        Vector2 knockBackVec = new Vector2(transform.position.x - TraceTarget.transform.position.x, transform.position.y - TraceTarget.transform.position.y);

        StartCoroutine(KnockBack(knockBackVec, attackDamage));
    }
    
    void KnockBack(Vector2 pos)
    {
        /*
        //어떤 방향에서 공격에 맞을지 모르기 때문에 밀려날 방향을 계산해야함.
        //이 객체의 포지션 x - 맞은 객체의 x 값 recX가 0보다 크면 1 , 작으면 -1
        //recX가 1이면 오른쪽으로 밀려나고 recX가 -1이면 왼쪽으로 밀려남
        //rexY가 1이면 위로 밀려나고 recY가 -1이면 아래로 밀려남.
        int recX = this.transform.position.x - pos.x > 0 ? 1 : -1;
        int recY = this.transform.position.y - pos.y > 0 ? 1 : -1;
        //Debug.Log("recX: " + recX);
        //Debug.Log("recY: " + recY);
        rigid.AddForce(new Vector2(recX, recY) * KnockBackPower, ForceMode2D.Impulse);
        */

        //rigid 쓰게 되면 발생하는 문제가
        //밀려나는 애들이 다른애들에게 부딪히면 부딛힌 애들이 물리적으로 밀려나서 멀리 날아가
        //게 되는 경우가 생김
        //그래서 RigidBody2D 컴포넌트에서 linear Drag에 값을 줘서 저항을 좀 주자.

    }

    IEnumerator KnockBack(Vector2 m_knockBackVec, int m_attackDamage)
    {
        float _knockBackPower = KnockBackPower;
        float temp = MonsterMoveSpeed;
        MonsterMoveSpeed = 0f;
        attackDamageForText = m_attackDamage; // damagefontmanager에게 전달할 텍스트는 m_attackDamage
        Instantiate(HitEft, transform);       // 히트 이펙트 출력
        StartCoroutine(MakeDamageFont());     // 데미지 폰트 생성하는 코루틴

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
        Vector2 pos = new Vector2(0f, 0.2f);

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
    /// Attack_C~~ -> 슬라임
    /// <returns></returns>

    IEnumerator Attack_B_Cool()
    {
        AttackCooltime -= 1;
        if (AttackCooltime <= 0)
        {
            if (Vector3.Distance(transform.position, TraceTarget.transform.position) <= 40)
            {
                isSuperArmor = true;
                thisAnim.SetTrigger("Ready");
                float temp = MonsterMoveSpeed;
                MonsterMoveSpeed = 0.0f;

                yield return new WaitForSeconds(0.7f);
                toPcVec = new Vector3(TraceTarget.transform.position.x - transform.position.x, TraceTarget.transform.position.y - transform.position.y, 0);
                yield return new WaitForSeconds(0.3f);
                attack_B();
                isAttack = true;


                yield return new WaitForSeconds(0.4f);
                isSuperArmor = false;
                isAttack = false;
                thisAnim.SetBool("isDuringAnim", false);
                MonsterMoveSpeed = temp;
            }
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Attack_B_Cool());
    }
    void attack_B()
    {
        AttackCooltime = 5;
        thisAnim.SetTrigger("Attack");
        thisAnim.SetBool("isDuringAnim", true);
    }

    IEnumerator Attack_C_Cool()
    {
        AttackCooltime -= 1;
        if (AttackCooltime <= 0)
        {
            if (Vector3.Distance(transform.position, TraceTarget.transform.position) <= 45)
            {
                isSuperArmor = true;
                thisAnim.SetTrigger("Attack");
                float temp = MonsterMoveSpeed;
                MonsterMoveSpeed = 0.0f;
                yield return new WaitForSeconds(0.2f);
                attack_C();

                yield return new WaitForSeconds(0.9f);
                Instantiate(attackProjectile, transform.position, transform.rotation);
                yield return new WaitForSeconds(0.5f);
                isSuperArmor = false;
                thisAnim.SetBool("isDuringAnim", false);
                MonsterMoveSpeed = temp;
            }
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(Attack_C_Cool());
    }
    void attack_C()
    {
        AttackCooltime = 5;
        thisAnim.SetTrigger("Attack");
        thisAnim.SetBool("isDuringAnim", true);
    }

    void GiveExpToPlayer()
    { }

}
