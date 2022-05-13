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

    public  float   MonsterMoveSpeed;           // ���� ���� �̵��ӵ�
    public  float   BaseMonsterMoveSpeed;       // �⺻ �̵��ӵ� �Ӽ� ��
    public  int     attackDamageForText;        // DamageFontManage.cs�� ���޵� �ؽ�Ʈ
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
    public  float   KnockBackPower;    //�˹� ������ ������ ����

    //���� ��ü�� �÷��̾� ĳ���� ������ �Ÿ�
    private Vector3 toPcVec;

    [SerializeField]
    private GameObject HitEft;

    public GameObject RewardBox;
    public GameObject Mimic;

    [SerializeField]
    private GameObject attackProjectile_0; // ������
    [SerializeField]
    private GameObject attackProjectile_1; // ���� ���� ����

    [SerializeField]
    private GameObject TelePortEffect;  //  �ڷ���Ʈ ����Ʈ
    private Vector3 TelePortPoint;      //  �ڷ���Ʈ�� ���� �������� ���� �ʿ䰡 ����
    public int SelectAttackNum;
    public List<GameObject> TileList;

    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player");

        //  �̹Ϳ� ���ؼ� ������ ���°� �ƴϸ�
        //  �÷��̾� ���ݿ� ���� �ʵ��� �ڽ��ݶ��̴� ���ֱ�
        if (MonsterId == 5 && isTrans == false)
        {
            BoxColliderOnOff(false);
        }

        //  ������ �׻� �˹������ ����
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

        //  �� ��ü �������� moveToward �Ἥ new Vector3 �������� �̵���ų ��
        //  new Vector3 = TraceTarget�� ������
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0),
            MonsterMoveSpeed * Time.deltaTime);

        //  ���Ͱ� �ٶ� ���� ����
        
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
            //  ���� ��쿡 ���Ͽ� ���� �̵� ó��
            //  �ִϸ��̼� ó���� �Ʒ��� Attack_B �ڷ�ƾ���� ó��
            //  isAttack ������ false ���ٰ� ���� ��Ÿ�ӿ� ���� true�� ��ȯ�Ǹ�
            //  ������Ʈ�� ���� �����ϴ� �̵�ó���� ����
            if (MonsterId == 2)
            {
                transform.position += toPcVec.normalized * 120.0f * Time.deltaTime;
            }
        }

        if (MonsterId == 5 && isTrans == false)
        {
            //  �̹��� �񷽰� �ٸ��� �÷��̾��� �ǽð� ��ġ�� �����ؼ� �����Ÿ� ���� ������ ������.
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

    //�÷��̾� ������ ȣ��
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
            
            MonsterMoveSpeed = 0;

            //���ó���� �ߴµ��� ��Ȥ �÷��̾� ���ݿ� �´� ��찡 ����.
            //collier ���ֱ�
            BoxColliderOnOff(false);
            
            thisAnim.SetBool("isDuringAnim", true);
            thisAnim.SetBool("isDead", true);
            thisAnim.SetTrigger("Die");

            StartCoroutine(ObjFlash());
        }
    }

    //  ������Ʈ ����
    //  ��������Ʈ ������ �����Ѵ� ���
    IEnumerator ObjFlash()
    {
        yield return new WaitForSeconds(1.0f);

        //  �̹� �̿��� ���͸� ����� �����ϵ���
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

            //  �������� ���� ������ ����� ������ ���� ��� �̹� or ���� ���� ���
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
    /// ���� ������ ����
    /// </summary>
    /// Attack_B~~ -> ��
    /// Attack_C~~ -> Spiked������
    /// Attack_D~~ -> Tentacle������
    /// MimicTrans -> �̹� ����
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

    //  �� ���� ����
    IEnumerator Attack_B()
    {
        isSuperArmor = true;
        thisAnim.SetTrigger("Ready");
        float temp = BaseMonsterMoveSpeed;
        MonsterMoveSpeed = 0.0f;

        yield return new WaitForSeconds(0.7f);

        //  toPcVec���� ���⼭ �ʱ�ȭ ���ִ� ����
        //  ���� ���� �غ� �ϴ� �ڼ����� 0.7�� ���� �÷��̾��� ��ġ�� ����
        //  isAttack�� true�� �Ǵ� ���� ������Ʈ�� ���� ������ ����� ���ε�
        //  0.3�� �� �÷��̾�� ���� 0.7�� ���� ������ ������ ��ġ���� ��� ���� ����.
        //  ��, ���⼭ toPcVec���� �ʱ�ȭ���� �ʰ� update�� ���� ���
        //  ���� �÷��̾��� �ǽð� ��ġ�� �����ϱ� ������ �÷��̾� ���忡���� ���ϱ� �ʹ� �����.
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

    //  Spiked������ ���� ����
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

    //  Tentacle ������ ���� ����
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

        //  for�� 3�� ���� -1, 0, 1
        for (int i = -1; i < 2; i++)
        {
            //  i�� 15�� �������μ�
            //  -15��, 0��, 15�� �� ������ �����ϰ� 
            float plusAngle = i * 15f;

            //  ������ ���� ��
            Quaternion angleAxis = Quaternion.Euler(
                0, 0, Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x) 
                * Mathf.Rad2Deg + plusAngle);

            //  ����ü ����
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
        //Ư�� ������ ���Ͱ� ������� ���
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
        //  ���� �����ϱ� ���� �ִ� �ڽ� �ݶ��̴� ���ֱ�
        BoxColliderOnOff(true);

        //  ���������ϱ� �̵��ϱ� ���� �̵��ӵ� ���� �ʱ�ȭ
        MonsterMoveSpeed = BaseMonsterMoveSpeed;
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>

    //  ���� �ൿ ���� 
    //  GM���� Ÿ�̸Ӱ� 0�Ǹ� ȣ���� ��
    public void BossPattern(Vector3 BossPos)
    {
        // ������ ���� ��ġ�� �޾ƿ�.
        // ���� �������� �÷��̾� �������� 3������ �ɾ��.
        // �� ���� ������ ���� ��ġ�� ���ϴ� ��.
        // ���� : �ƽ� ���� ���� ��ġ �������� ���� �����Ǵµ� �� ���ο����� 
        // �ڷ���Ʈ�ϸ鼭 �����ϱ� ����
        TelePortPoint = BossPos;

        //  ���� ���� �ڷ�ƾ ����
        StartCoroutine(BaseAttack_Boss());          // ���� �⺻ ���� - �÷��̾� ��ġ�� ���� ���� �Ѱ� ����
        StartCoroutine(SpecialAttack_Boss());       // ���� Ư�� ���� - �ڷ���Ʈ ���� ���� ����
    }

    IEnumerator SpecialAttack_Boss()
    {
        StartCoroutine(TelePort());
        yield return new WaitForSeconds(1.5f);

        SelectAttack();
        yield return new WaitForSeconds(1.5f);
    }

    // �ڷ���Ʈ
    IEnumerator TelePort()
    {
        TelePortEffect.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        TelePortEffect.SetActive(false);

        float randx = Random.Range(-1, 2);  //  -1  or  0 or 1
        float randy = Random.Range(-1, 2);
        float posx = randx * 100;           //  -100 or 0 or 100
        float posy = randy * 60;            //  -60  or 0 or 60

        //  �ڷ���Ʈ ��ġ ���ϱ�
        Vector3 teleportpos = TelePortPoint + new Vector3(posx, posy, 0);
        Vector3 isnewpos = teleportpos;

        //  ���� ���� �ִ� ��ġ�� ���ο� �ڷ���Ʈ ��ġ�� ������ 
        if (transform.position == isnewpos)
        { StartCoroutine(TelePort()); } // ���ο� ��ġ ���ϱ�
        // ���� ������ ����� ��ġ�� �̵�
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
        // �÷��̾� ���� ��ġ�� ���� ���� �Ѱ� �����ϴ� ���� �⺻ ����
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
            case 0: // ����ü�� ���������� �߻�
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

            case 1: //  �����¿� 4�������� 3������ ������ ����ü�� ������ �����ָ鼭 �߻�
                for (int k = 0; k < 20; k++) //�� 20���� �߻��Ұǵ�
                {
                    float plusAngle2 = k * 3; // 1�� ��� ���� ���� 3�� ��ŭ ������ �������� ��Ŵ�.

                    for (int j = 0; j < 4; j++) // �� 20���� 4��(4��������) ����̴�.
                    {
                        float other = j * 90f; // j�� 90���� �����ָ� �����¿� ������ ������ ��.
                        for (int i = -1; i < 2; i++) // i�� -1,0,1�� ���Ͽ�
                        {
                            float plusAngle = i * 15f + other; //15���� ���������μ� -15��, 0��, +15�� -> 3������ ������ �Ұ��̴�.

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

            case 2: // �����¿� 4�������� ���ٱ�� ������ ����ü�� ������ �����ָ鼭 �߻�
                for (int i = 0; i < 60; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        float plusAngle = i * 10f + (j * 90); // i*10 ->���� ���� ���� ������ �߰� ����, j*90 -> ����

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

            case 3: // ��Ը� ���� ����
                for (int j = 0; j < 5; j++) // 5��
                {
                    for (int i = 0; i < 20; i++) //25��
                    {
                        // �÷��̾� ���� ��ġ ����
                        // x ���� ��, y���� ���� ���� �������� ���ؼ�
                        // �ش� ��ġ�� �Ѹ�
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
                            float plusAngle = i * 10f + (j * 90); // i*10 ->���� ���� ���� ������ �߰� ����, j*90 -> ����

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
                        tilecount++; // -> 15�� �� ��
                    }
                    yield return new WaitForSeconds(0.4f);
                }
                yield return new WaitForSeconds(1.0f);
                
                // ����Ʈ�� ��� ��ü���� �ӵ��� 0����
                for (int j = 0; j < tilecount; j++)
                {
                    TileList[j].GetComponent<EnemyProjectile>().TileSpeed = 0;
                }
                yield return new WaitForSeconds(1.0f);
                
                //  ����Ʈ�� ��� ��ü�鿡�� ���� Ÿ���� ��ġ�� �ӵ� ����
                for (int k = 0; k < tilecount; k++)
                {
                    Debug.Log(tilecount);
                    float tilespeed = TileList[k].GetComponent<EnemyProjectile>().BaseTileSpeed *= 1.8f;
                    TileList[k].GetComponent<EnemyProjectile>().SetTarget(true, tilespeed);
                    yield return new WaitForSeconds(0.1f);
                }

                //����Ʈ�� �ִ� ��ü ������ ����
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

    void BoxColliderOnOff(bool val)
    {
        GetComponents<BoxCollider2D>()[0].enabled = val;
        GetComponents<BoxCollider2D>()[1].enabled = val;
    }
}
