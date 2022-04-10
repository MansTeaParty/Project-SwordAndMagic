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
    public  int     attackDamageForText;         //DamageFontManage.cs�� ���޵� �ؽ�Ʈ
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
    public float    KnockBackPower;    //�˹� ������ ������ ����, �ϴ� 1

    //���� ��ü�� �÷��̾� ĳ���� ������ �Ÿ�
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
        //�� ��ü ������ = moveToward�Ἥ ���� �������� �̵���ų ��
        //���� ���� : TraceTarget ����
        //new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0)
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0),
            MonsterMoveSpeed * Time.deltaTime);

        //���Ͱ� �ٶ� ���� ����
        float objPosXCal = transform.position.x - TraceTarget.transform.position.x;
        if (!isDead)
        {
            if (objPosXCal < 0f)//�� ��ü�� �÷��̾�� ���ʿ� ����
            {
                ThisSpriteRenderer.flipX = false;
            }
            else//�� ��ü�� �÷��̾�� �����ʿ� ����
            {
                ThisSpriteRenderer.flipX = true;
            }
        }
    }

    private void AttackMove()
    {
        if (isAttack && !isDead)
        {
            //���� ��쿡 ���Ͽ� ���� �̵� ó��
            //�ִϸ��̼� ó���� �Ʒ��� Attack_B �ڷ�ƾ���� ó��
            //isAttack ������ false ���ٰ� ���� ��Ÿ�ӿ� ���� true�� ��ȯ�Ǹ�
            //������Ʈ�� ���� �����ϴ� �̵�ó���� ����
            if (MonsterId == 2)
                transform.position += toPcVec.normalized * 120.0f * Time.deltaTime;
        }
    }

    //�÷��̾ ȣ��
    public void Hit(int attackDamage)
    {
        Vector2 knockBackVec = new Vector2(transform.position.x - TraceTarget.transform.position.x, transform.position.y - TraceTarget.transform.position.y);

        StartCoroutine(KnockBack(knockBackVec, attackDamage));
    }
    
    IEnumerator KnockBack(Vector2 m_knockBackVec, int m_attackDamage)
    {
        float _knockBackPower = KnockBackPower;
        float temp = MonsterMoveSpeed;          // �˹��� ���� �����ϰ��� �ӵ��� ����
        MonsterMoveSpeed = 0f;
        attackDamageForText = m_attackDamage;   // damagefontmanager���� ������ �ؽ�Ʈ�� m_attackDamage
        Instantiate(HitEft, transform);         // ��Ʈ ����Ʈ ���
        StartCoroutine(MakeDamageFont());       // ������ ��Ʈ �����ϴ� �ڷ�ƾ

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
    /// Attack_C~~ -> Spiked������
    /// Attack_D~~ -> Tentacle������
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

    //�� ���� ����
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


    //Spiked������ ���� ����
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

    //Tentacle ������ ���� ����
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
        //�׳� ���ڸ� ���� -> �÷��̾� ���������� �����ϴ� �Լ��� �ҷ����� ���� ����.
        TraceTarget.GetComponent<PlayerCtrl>().playerExp += MonsterExp;

        //or
        //TraceTarget.GetComponent<PlayerCtrl>().AcceptExp(MonsterExp);
        //ó�� AcceptExp(MonsterExp) �Լ��� ȣ��

        //����ġ�� �����ϸ� UI ������ ����ġ �ٰ� ���ϴ� ���� ������ �� �ֵ��� ��������� ��.

        // 1. �ܼ��� ���ڸ� ������ ���
        // ���� ���� + PlayerCtrl.cs�� ���� �����ϴ� �Լ��� ȣ��
        // -> ���ŷӴ� �׳� ���ް� ������ ���ÿ� �ϸ� �� ����.
        
        // 2. �׳� �Լ� ȣ��
        // ��ó�� �Լ� ȣ���ؼ� ����ġ�� �����ϰ� �� �Լ� ���� �����ϴ� ������ �ۼ�

    }

}
