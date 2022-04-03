using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public enum MonsterState {MonsterIdle, MonsterRun, MonsterAttack, MonsterDie, MonsterHit };
    public MonsterState Monster_State;

    //추적할 타깃
    private GameObject TraceTarget;
    private SpriteRenderer ThisSpriteRenderer;
    private Rigidbody2D rigid;
    private Animator thisAnim;

    public BoxCollider2D Boxcoll;
    public CapsuleCollider2D Capcoll;

    public string MonsterName;
    public int Attack_Type; // 근접 :0, 원거리 : 1

    public float Attack_Range; //근접은 0
    public float Attack_Cooltime;

    public int Monster_HP;
    public int Monster_Damage;
    public float Monster_MoveSpeed;
    public float Monster_DropRate; // 10% = 0.1
    public int Monster_Exp;

    public float KnockBackPower;


    //이동 가능한지?
    private bool isRunAble;
    //사망 했는지?
    private bool isMonsterDie;
    //공격 가능한지?
    private bool isAttackAble;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ThisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisAnim = GetComponent<Animator>();
        TraceTarget = GameObject.FindGameObjectWithTag("Player");

        //처음 생성될 때 idle 상태로 하고자 함.
        Monster_State = MonsterState.MonsterIdle;
        isRunAble = true;
        isMonsterDie = false;
        isAttackAble = false;

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
                LookingDirection();
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


    //플레이어 바라보는 방향 결정
    void LookingDirection()
    {
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

    void Move()
    {
        //이 객체 포지션 = moveToward써서 지정 방향으로 이동시킬 것
        //지정 방향 : TraceTarget 방향
        //new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0)
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0),
            Monster_MoveSpeed * Time.deltaTime);

    }

    //플레이어와의 충돌
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

        //플레이어 공격에 맞았는지 체크
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //플레이어 공격에 맞았으면
        if (coll.gameObject.tag =="PlayerAttack")
        {

            //상태 변경
            //안 해주면 아래에서 run 파라미터를 false 처리해도 
            //몬스터 상태가 run 상태이기 때문에 run가 다시 true로 되서 계속 움직임.
            //그래서 상태를 바꾸어줘야 효과 있음.
            Monster_State = MonsterState.MonsterHit;

            //이동 정지
            isRunAble = false;
            thisAnim.SetBool("Run", false);

            //몬스터 체력 확인
            isMonsterHPzero();

            //hit 애니메이션으로 전환
            thisAnim.SetTrigger("Hit");

            //넉백
            KnockBack(coll.transform.position);

        }
    }

    void KnockBack(Vector2 pos)
    {
        //어떤 방향에서 공격에 맞을지 모르기 때문에 밀려날 방향을 계산해야함.
        //이 객체의 포지션 x - 맞은 객체의 x 값 recX가 0보다 크면 1 , 작으면 -1
        //recX가 1이면 오른쪽으로 밀려나고 recX가 -1이면 왼쪽으로 밀려남
        //rexY가 1이면 위로 밀려나고 recY가 -1이면 아래로 밀려남.
        int recX = this.transform.position.x - pos.x > 0 ? 1 : -1;
        int recY = this.transform.position.y - pos.y > 0 ? 1 : -1;
        /*Debug.Log("recX: " + recX);
        Debug.Log("recY: " + recY);*/

        rigid.AddForce(new Vector2(recX, recY) * KnockBackPower, ForceMode2D.Impulse);

        StartCoroutine(KnockBackTime());

        //rigid 쓰게 되면 발생하는 문제가
        //밀려나는 애들이 다른애들에게 부딪히면 부딛힌 애들이 물리적으로 밀려나서 멀리 날아가
        //게 되는 경우가 생김
        //그래서 RigidBody2D 컴포넌트에서 linear Drag에 값을 줘서 저항을 좀 주자.

    }

    IEnumerator KnockBackTime()
    {
        yield return new WaitForSeconds(0.1f);
        //넉백되는 물리적 속도를 0으로 초기화 해서 멈춰줌.
        rigid.velocity = new Vector2(0f, 0f);

        //넉백후 0.5초 뒤 Idle 상태가 됨.
        yield return new WaitForSeconds(0.5f);
        if (!isMonsterDie)
        {
            isRunAble = true;
            Monster_State = MonsterState.MonsterIdle;
        }
        else
        {
            isRunAble = false;
            Monster_State = MonsterState.MonsterDie;
        }
    }


    //플레이어가 한 공격이 호출하는 함수
    void MonsterHPCal(int pcAttackDM)
    {
        //몬스터 체력 계산
        Monster_HP -= pcAttackDM;
    }

    void isMonsterHPzero()
    {
        if (Monster_HP <= 0)
        {
            isMonsterDie = true;
            Monster_State = MonsterState.MonsterDie;
            thisAnim.SetTrigger("Death");

            //사망처리를 했는데도 간혹 플레이어 공격에 맞는 경우가 있음.
            //collier 꺼주기
            GetComponents<BoxCollider2D>()[0].enabled = false;
            GetComponents<BoxCollider2D>()[1].enabled = false;
        }
    }

    //오브젝트 점멸
    void ObjFlash()
    { 
        
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
