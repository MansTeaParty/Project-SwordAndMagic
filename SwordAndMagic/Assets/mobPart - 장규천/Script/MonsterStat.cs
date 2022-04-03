using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public int MonsterId;
    public enum MonsterState {MonsterIdle, MonsterRun, MonsterAttack, MonsterDie, MonsterHit };
    public MonsterState Monster_State;

    private GameObject TraceTarget;
    private SpriteRenderer ThisSpriteRenderer; 
    private Animator thisAnim;

    public string MonsterName;
    public int Attack_Type; // 근접 :0, 원거리 : 1

    public float Attack_Range; //근접은 0
    public float Attack_Cooltime;
    public int Monster_HP;
    public int Monster_Damage;
    public float Monster_MoveSpeed;
    public float Monster_DropRate; // 10% = 0.1
    public int Monster_Exp;
    public float KnockBackPower;    //넉백 정도를 저장할 변수

    public GameObject DamageFont;
    public int attackDamageForText; //DamageFontManage.cs에 전달


    //이동 가능한지?
    private bool isRunAble;
    //사망 했는지?
    private bool isMonsterDie;
    //공격 가능한지?
    private bool isAttackAble;

    [SerializeField]
    private GameObject HitEft;

    void Start()
    {
        ThisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisAnim = GetComponent<Animator>();
        TraceTarget = GameObject.FindGameObjectWithTag("Player");

        //처음 생성될 때 idle 상태로 하고자 함.
        Monster_State = MonsterState.MonsterIdle;
        isRunAble = true;
        isMonsterDie = false;
        isAttackAble = false;

        if (MonsterId == 2)
        {
            //StartCoroutine(Attack_B_Cool());
        }
        if (MonsterId == 3)
        { 
            //StartCoroutine(Attack_C_Cool());
        }

    }

    void Update()
    {
        NowMonsterState();
        AttackDistanceCal();

        Debug.Log(Monster_State);

    }

    void NowMonsterState()
    {
        switch (Monster_State)
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
        }
    }
    void Move()
    {
        //이 객체 포지션 = moveToward써서 지정 방향으로 이동시킬 것
        //지정 방향 : TraceTarget 방향
        //new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0)
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0),
            Monster_MoveSpeed * Time.deltaTime);


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
        float a = KnockBackPower;

        attackDamageForText = m_attackDamage;
        Instantiate(HitEft, transform);
        StartCoroutine(MakeDamageFont(m_attackDamage));

        Monster_HP -= m_attackDamage;

        thisAnim.SetTrigger("Hit");

        for (int i = 0; i < 10; i++)
        {
            transform.position += (Vector3)m_knockBackVec.normalized *a ;
            a -= KnockBackPower / 10.0f;
            yield return new WaitForSeconds(0.01f);
        }

        //체력 검사
        isMonsterHPzero();

        //넉백후 0.5초 뒤 Idle 상태가 됨.
        yield return new WaitForSeconds(0.5f);
        if (!isMonsterDie)
        {
            isRunAble = true;
            Monster_State = MonsterState.MonsterIdle;
        }
    }

    IEnumerator MakeDamageFont(int m_damage)
    {
        Instantiate(DamageFont, transform);
        yield return new WaitForSeconds(0.0f);
    }


    void isMonsterHPzero()
    {
        if (Monster_HP <= 0)
        {
            Monster_State = MonsterState.MonsterDie;

            isMonsterDie = true;
            thisAnim.SetTrigger("Death");

            //사망처리를 했는데도 간혹 플레이어 공격에 맞는 경우가 있음.
            //collier 꺼주기
            GetComponents<BoxCollider2D>()[0].enabled = false;
            GetComponents<BoxCollider2D>()[1].enabled = false;


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

        Destroy(gameObject);
    }



    void GiveExpToPlayer()
    { }

    void AttackDistanceCal()
    {
        /*float dis = (transform.position - TraceTarget.transform.position).magnitude;
        //Debug.Log("dis: " + dis);

        if (dis <= Attack_Range && !isAttackAble)
        {
            Debug.Log("공격 범위에 플레이어 있습니다.");

            Monster_State = MonsterState.MonsterAttack;

            thisAnim.SetBool("Attack", true);
        }
        else if(dis > Attack_Range && !isAttackAble)
        {
            Debug.Log("공격 범위에 플레이어 없습니다.");

            Monster_State = MonsterState.MonsterIdle;

            thisAnim.SetBool("Attack", false);
        }*/

    }
}
