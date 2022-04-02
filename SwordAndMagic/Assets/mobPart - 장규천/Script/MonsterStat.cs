using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : MonoBehaviour
{
    public enum MonsterState {MonsterIdle, MonsterRun, MonsterAttack, MonsterDie, MonsterHit };
    public MonsterState monsterState;

    //������ Ÿ��
    public GameObject TraceTarget;
    private SpriteRenderer ThisSpriteRenderer;
    private Rigidbody2D rigid;
    private Animator thisAnim;

    public BoxCollider2D boxColl;
    public CapsuleCollider2D capColl;

    public string monsterName;
    public int attackType; // ���� :0, ���Ÿ� : 1

    public float attackRange; //������ 0
    public float attackCoolTime;

    public int monsterHP;
    public int monsterDamage;
    public float monsterMoveSpeed;
    public float monsterDropRate; // 10% = 0.1
    public int monsterExp;

    public float KnockBackPower;


    //�̵� ��������?
    private bool isRunAble;
    //���� ��������?
    private bool isMonsterDie;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        ThisSpriteRenderer = GetComponent<SpriteRenderer>();
        thisAnim = GetComponent<Animator>();
        TraceTarget = GameObject.FindGameObjectWithTag("Player");

        //ó�� ������ �� idle ���·� �ϰ��� ��.
        monsterState = MonsterState.MonsterIdle;
        isRunAble = true;
        isMonsterDie = false;

    }

    void Update()
    {
        NowMonsterState();
        Debug.Log(monsterState);
    }

    void NowMonsterState()
    {
        switch (monsterState)
        {
            case MonsterState.MonsterIdle:
                if (isRunAble == true)
                {
                    monsterState = MonsterState.MonsterRun;
                }
                break;

            case MonsterState.MonsterRun:
                thisAnim.SetBool("Run", true);
                LookingDirection();
                Move();
                break;

            case MonsterState.MonsterAttack:
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
            monsterMoveSpeed * Time.deltaTime);

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
            monsterState = MonsterState.MonsterHit;

            //�̵� ����
            isRunAble = false;
            thisAnim.SetBool("Run", false);

            //hit �ִϸ��̼����� ��ȯ
            thisAnim.SetTrigger("Hit");


            //���� ü�� Ȯ��
            isMonsterHPzero();

            //�˹�
            KnockBack(coll.transform.position);

        }
    }

    void KnockBack(Vector2 pos)
    {
        //� ���⿡�� ���ݿ� ������ �𸣱� ������ �з��� ������ ����ؾ���.
        //�� ��ü�� ������ x - ���� ��ü�� x �� recX�� 0���� ũ�� 1 , ������ -1
        //recX�� 1�̸� ���������� �з����� recX�� -1�̸� �������� �з���
        //rexY�� 1�̸� ���� �з����� recY�� -1�̸� �Ʒ��� �з���.
        int recX = this.transform.position.x - pos.x > 0 ? 1 : -1;
        int recY = this.transform.position.y - pos.y > 0 ? 1 : -1;
        /*Debug.Log("recX: " + recX);
        Debug.Log("recY: " + recY);*/

        rigid.AddForce(new Vector2(recX, recY) * KnockBackPower, ForceMode2D.Impulse);

        StartCoroutine(KnockBackTime());

        //rigid ���� �Ǹ� �߻��ϴ� ������
        //�з����� �ֵ��� �ٸ��ֵ鿡�� �ε����� �ε��� �ֵ��� ���������� �з����� �ָ� ���ư�
        //�� �Ǵ� ��찡 ����
        //�׷��� RigidBody2D ������Ʈ���� linear Drag�� ���� �༭ ������ �� ����.

    }

    IEnumerator KnockBackTime()
    {
        yield return new WaitForSeconds(0.1f);
        //�˹�Ǵ� ������ �ӵ��� 0���� �ʱ�ȭ �ؼ� ������.
        rigid.velocity = new Vector2(0f, 0f);

        //�˹��� 0.5�� �� Idle ���°� ��.
        yield return new WaitForSeconds(0.5f);
        if (!isMonsterDie)
        {
            isRunAble = true;
            monsterState = MonsterState.MonsterIdle;
        }
        else
        {
            isRunAble = false;
            monsterState = MonsterState.MonsterDie;
        }
    }


    //�÷��̾ �� ������ ȣ���ϴ� �Լ�
    void MonsterHPCal(int pcAttackDM)
    {
        //���� ü�� ���
        monsterHP -= pcAttackDM;
    }

    void isMonsterHPzero()
    {
        if (monsterHP <= 0)
        {
            isMonsterDie = true;
            monsterState = MonsterState.MonsterDie;
            thisAnim.SetTrigger("Death");

            //���ó���� �ߴµ��� ��Ȥ �÷��̾� ���ݿ� �´� ��찡 ����.
            //collier ���ֱ�
            boxColl.enabled = false;
            capColl.enabled = false;
        }
    }

    //������Ʈ ����
    void ObjFlash()
    { 
        
    }

    void GiveExpToPlayer()
    { }

}
