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
    public string Attack_Type; // ���� :Meele, ���Ÿ� : Range

    public float Attack_Range; //������ 0
    public float AttackCooltime;
    public int MonsterHP;
    public int MonsterDamage;
    public float MonsterMoveSpeed;
    public float MonsterDropRate;
    public int MonsterExp;
    public float KnockBackPower;    //�˹� ������ ������ ����

    public GameObject DamageFont;
    public int attackDamageForText; //DamageFontManage.cs�� ����

    //���� ��ü�� �÷��̾� ĳ���� ������ �Ÿ�
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

        if (MonsterId == 2) // ��
        {
            if(!isDead)
                StartCoroutine(Attack_B_Cool());
        }
        if (MonsterId == 3) // ������
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
        //�� ��ü ������ = moveToward�Ἥ ���� �������� �̵���ų ��
        //���� ���� : TraceTarget ����
        //new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0)
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0),
            MonsterMoveSpeed * Time.deltaTime);


        //���Ͱ� �ٶ� ���� ����
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

    private void AttackMove()
    {
        if (isAttack && !isDead)
        {
            //���� ��쿡 ���Ͽ� 
            //���� �̵� ó��
            //�ִϸ��̼� ó���� ���� ����
            //isAttack ������ false ���ٰ� ���� ��Ÿ�ӿ� ���� true�� ��ȯ�Ǹ�
            //������Ʈ�� ���� �����ϴ� �̵�ó���� ����
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
   
    //�÷��̾ ȣ��
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
        float _knockBackPower = KnockBackPower;
        float temp = MonsterMoveSpeed;
        MonsterMoveSpeed = 0f;
        attackDamageForText = m_attackDamage; // damagefontmanager���� ������ �ؽ�Ʈ�� m_attackDamage
        Instantiate(HitEft, transform);       // ��Ʈ ����Ʈ ���
        StartCoroutine(MakeDamageFont());     // ������ ��Ʈ �����ϴ� �ڷ�ƾ

        //ü�� ���
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

            //�̵� �ӵ� 0���� ����
            MonsterMoveSpeed = 0;

            //���ó���� �ߴµ��� ��Ȥ �÷��̾� ���ݿ� �´� ��찡 ����.
            //collier ���ֱ�
            GetComponents<BoxCollider2D>()[0].enabled = false;
            GetComponents<BoxCollider2D>()[1].enabled = false;

            
            thisAnim.SetBool("isDuringAnim", true);
            thisAnim.SetBool("isDead", true);
            thisAnim.SetTrigger("Die");

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
    /// ���� ������ ����
    /// </summary>
    /// Attack_B~~ -> ��
    /// Attack_C~~ -> ������
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
