using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public enum MonsterState {MonsterIdle, MonsterRun, MonsterAttack, MonsterDie, MonsterHit };
    public MonsterState Monster_State;

    //������ Ÿ��
    private GameObject TraceTarget;
    private SpriteRenderer ThisSpriteRenderer;
    private Rigidbody2D rigid;
    private Animator thisAnim;

    public string MonsterName;
    public int Attack_Type; // ���� :0, ���Ÿ� : 1

    public float Attack_Range; //������ 0
    public float Attack_Cooltime;

    public int Monster_HP;
    public int Monster_Damage;
    public float Monster_MoveSpeed;
    public float Monster_DropRate; // 10% = 0.1
    public int Monster_Exp;

    public float KnockBackPower;


    //�̵� ��������?
    private bool isRunAble;
    //��� �ߴ���?
    private bool isMonsterDie;
    //���� ��������?
    private bool isAttackAble;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ThisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisAnim = GetComponent<Animator>();
        TraceTarget = GameObject.FindGameObjectWithTag("Player");

        //ó�� ������ �� idle ���·� �ϰ��� ��.
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


    //�÷��̾� �ٶ󺸴� ���� ����
    void LookingDirection()
    {
        float objPosXCal = transform.position.x - TraceTarget.transform.position.x;

        //�� ��ü�� �÷��̾�� ���ʿ� ����
        if (objPosXCal < 0f)
        {
            ThisSpriteRenderer.flipX = false;
        }
        //�� ��ü�� �÷��̾�� �����ʿ� ����
        else //if (objPosXCal > 3.0f)
        {
            ThisSpriteRenderer.flipX = true;
        }
    }

    void Move()
    {
        //�� ��ü ������ = moveToward�Ἥ ���� �������� �̵���ų ��
        //���� ���� : TraceTarget ����
        //new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0)
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0),
            Monster_MoveSpeed * Time.deltaTime);

    }

    //�÷��̾���� �浹
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

        //�÷��̾� ���ݿ� �¾Ҵ��� üũ
    private void OnTriggerEnter2D(Collider2D coll)
    {
        //�÷��̾� ���ݿ� �¾�����
        if (coll.gameObject.tag =="PlayerAttack")
        {

            //���� ����
            //�� ���ָ� �Ʒ����� run �Ķ���͸� false ó���ص� 
            //���� ���°� run �����̱� ������ run�� �ٽ� true�� �Ǽ� ��� ������.
            //�׷��� ���¸� �ٲپ���� ȿ�� ����.
            //Monster_State = MonsterState.MonsterHit;

            //�̵� ����
            //isRunAble = false;
            //thisAnim.SetBool("Run", false);

            //���� ü�� Ȯ��
            //isMonsterHPzero();

            //hit �ִϸ��̼����� ��ȯ
            //thisAnim.SetTrigger("Hit");

            //�˹�
            //KnockBack(coll.transform.position);

        }
    }

    public void Hit(int attackDamage)
    {
        Vector2 knockBackVec = new Vector2(transform.position.x - TraceTarget.transform.position.x, transform.position.y - TraceTarget.transform.position.y);

        StartCoroutine(KnockBack(knockBackVec, attackDamage));
    }
    
    void KnockBack(Vector2 pos)
    {
        /*
        //� ���⿡�� ���ݿ� ������ �𸣱� ������ �з��� ������ ����ؾ���.
        //�� ��ü�� ������ x - ���� ��ü�� x �� recX�� 0���� ũ�� 1 , ������ -1
        //recX�� 1�̸� ���������� �з����� recX�� -1�̸� �������� �з���
        //rexY�� 1�̸� ���� �з����� recY�� -1�̸� �Ʒ��� �з���.
        int recX = this.transform.position.x - pos.x > 0 ? 1 : -1;
        int recY = this.transform.position.y - pos.y > 0 ? 1 : -1;
        //Debug.Log("recX: " + recX);
        //Debug.Log("recY: " + recY);
        rigid.AddForce(new Vector2(recX, recY) * KnockBackPower, ForceMode2D.Impulse);
        */

        //rigid ���� �Ǹ� �߻��ϴ� ������
        //�з����� �ֵ��� �ٸ��ֵ鿡�� �ε����� �ε��� �ֵ��� ���������� �з����� �ָ� ���ư�
        //�� �Ǵ� ��찡 ����
        //�׷��� RigidBody2D ������Ʈ���� linear Drag�� ���� �༭ ������ �� ����.

    }

    IEnumerator KnockBack(Vector2 m_knockBackVec, int m_attackDamage)
    {
        float a = KnockBackPower;
        Monster_HP -= m_attackDamage;

        thisAnim.SetTrigger("Hit");

        for (int i = 0; i < 10; i++)
        {
            transform.position += (Vector3)m_knockBackVec.normalized *a ;
            a -= KnockBackPower / 10.0f;
            yield return new WaitForSeconds(0.01f);
        }


        isMonsterHPzero();

        //�˹��� 0.5�� �� Idle ���°� ��.
        yield return new WaitForSeconds(10.5f);
        if (!isMonsterDie)
        {
            isRunAble = true;
            Monster_State = MonsterState.MonsterIdle;
        }
    }


    void isMonsterHPzero()
    {
        if (Monster_HP <= 0)
        {
            Monster_State = MonsterState.MonsterDie;

            isMonsterDie = true;
            thisAnim.SetTrigger("Death");

            //���ó���� �ߴµ��� ��Ȥ �÷��̾� ���ݿ� �´� ��찡 ����.
            //collier ���ֱ�
            GetComponents<BoxCollider2D>()[0].enabled = false;
            GetComponents<BoxCollider2D>()[1].enabled = false;


            StartCoroutine(ObjFlash());
        }
    }

    //������Ʈ ����
    //�ϴ� ��������Ʈ ������ �����Ѵ� �������
    //�ÿ������� ��������Ʈ ���� ������� �����ϴ� ������ �غ���
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
            Debug.Log("���� ������ �÷��̾� �ֽ��ϴ�.");

            Monster_State = MonsterState.MonsterAttack;

            thisAnim.SetBool("Attack", true);
        }
        else if(dis > Attack_Range && !isAttackAble)
        {
            Debug.Log("���� ������ �÷��̾� �����ϴ�.");

            Monster_State = MonsterState.MonsterIdle;

            thisAnim.SetBool("Attack", false);
        }*/

    }
}
