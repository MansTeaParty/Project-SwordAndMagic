using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SYMath;

public class PlayerCtrl : MonoBehaviour
{
    //GetInput
    private int h = 0;
    private int v = 0;
    private bool wDown;
    private bool aDown;
    private bool sDown;
    private bool dDown;
    private bool wUp;
    private bool aUp;
    private bool sUp;
    private bool dUp;

    private bool isAnimOn; //
    private bool AttackON = true; //���� ��Ÿ�� ������ ���� ���� ���� �������� �˸��� ����. NormalAttack()�� ����� ��, false�� �ǰ� attackSpeed�ʰ� ������ Ture
    private bool RollOn = true;  //������ ��Ÿ�� ������ �����⸦ ���� ���� �������� �˸��� ����. Roll()�� ����� �� false�� �ǰ� 1�ʰ� ������ True;
    private bool StopMove = false;
    //Move&Turn
    private Transform tr;
    private Rigidbody2D rigid;
    private Vector3 currPos;
    public Vector2 moveVec;

    public float moveSpeed = 1.0f;
    public float attackSpeed;

    private Vector2 RollVec; //������ ������ ���� ����

    [SerializeField]
    private GameObject FirePos; // ����ü�� �����Ǵ� ��ġ�� ��� �ִ� ��ü, FirePosPivot�� �θ�� �ΰ� �־ FirePosPivot�� ĳ���� ��ġ���� ���콺 ���� ���� ȸ���ϸ� FirePos�� ĳ���͸� �߽����� ���� �׸��� ����

    [SerializeField]
    private GameObject FirePosPivot; // ĳ���Ϳ� ���ӵǾ ������ ��ǥ���� ������ ���콺 ���⿡ ���� ȸ�� -> FirePosPivot = ��� ���� 

    public Animator anim;

    //BaseAttack
    public GameObject curBaseAttack;

    public int Damage;
    public GameObject NowAttackObj;
    private GameObject tempObj;

    public GameObject nomalAttack_Projectile;
    public GameObject PowerAttack_Projectile;
    //public GameManager gameManager;
    private int attackCount = 0;

    void Awake()
    {
        tr = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        //StartCoroutine("BaseAttack");
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();



    }
    private void FixedUpdate()
    {
        //���Ϳ��� ��ų� �˹�Ǵ� ������ ������ �÷��̾ �з���
        //������ �ӵ� �ʱ�ȭ
        rigid.velocity = new Vector2(0f, 0f);

        GetInput();
        Turn();
        Move();
        
    }
    void update()
    {

    }

    void GetInput()
    {
        if (!StopMove)
        {
            h = ((int)Input.GetAxisRaw("Horizontal"));
            v = ((int)Input.GetAxisRaw("Vertical"));
        }
        wDown = Input.GetKey(KeyCode.W);
        wUp = Input.GetKeyUp(KeyCode.W);
        sDown = Input.GetKey(KeyCode.S);
        sUp = Input.GetKeyUp(KeyCode.S);
        aDown = Input.GetKey(KeyCode.A);
        aUp = Input.GetKeyUp(KeyCode.A);
        dDown = Input.GetKey(KeyCode.D);
        dUp = Input.GetKeyUp(KeyCode.D);

        if (Input.GetMouseButton(0))
        {
            StartCoroutine(NormalAttack());
        }

        if (Input.GetKey("space"))
        {
            StartCoroutine(Roll());
        }
    }

    IEnumerator NormalAttack()
    {
        if (AttackON && !StopMove)
        {
            attackCount += 1;

            /*if (attackCount % 3 == 0)
            {
                AttackON = false;
                anim.SetBool("isAttack", true);
                anim.SetTrigger("Attack");
                Instantiate(PowerAttack_Projectile, FirePos.transform.position, FirePosPivot.transform.rotation); // FirePos = ����ü�� ������ġ, FirePosPivot = ����ü�� ���󰡴� ���� 
                yield return new WaitForSeconds(attackSpeed);
                AttackON = true;
            }
            else
            {
                AttackON = false;
                anim.SetBool("isAttack", true);
                anim.SetTrigger("Attack");
                Instantiate(nomalAttack_Projectile, FirePos.transform.position, FirePosPivot.transform.rotation);
                yield return new WaitForSeconds(attackSpeed);
                AttackON = true;
            }*/


            AttackON = false;
            anim.SetBool("isAttack", true);
            anim.SetTrigger("Attack");

            if (attackCount % 3 == 0) //������
            {
                Damage = 30;
                NowAttackObj = PowerAttack_Projectile;
            }
            else //�����
            {
                Damage = 15;
                NowAttackObj = nomalAttack_Projectile;
            }

            //�����ϰ� �� �� �޽��� ����
            tempObj = Instantiate(NowAttackObj, FirePos.transform.position, FirePosPivot.transform.rotation);

            tempObj.SendMessage("RecieveDamage", Damage);

            yield return new WaitForSeconds(attackSpeed);
            AttackON = true;

        }
    }
    IEnumerator Roll() //������
    {
        if (RollOn)
        {
            RollOn = false; // ������ ��Ÿ���� ���� ����
            StopMove = true; // ������ �ִϸ��̼� ���� �ٸ� �ִϸ��̼��� ������� �ʰ� �ϱ� ���� ���� -> �ִϸ��̼� �̺�Ʈ�� ������ �ִϸ��̼��� ������ ��������Ʈ ��� ��, ȣ���Ͽ� false ó��
            rigid.velocity = Vector2.zero; //Ȥ�� �𸣴� �浹�� ���� ������ ��Ȳ�� ���� ���ؼ�
            anim.SetBool("isRoll", true);
            anim.SetTrigger("Roll");
            RollVec = FirePosPivot.transform.TransformDirection(Vector2.right); // FirePosPivot�� PC���� ���콺�� ���ϴ� �����̼� ���� ������ �ֱ⿡ �����⸦ �Է��� ��, 
            yield return new WaitForSeconds(1.0f);                              // �ش� ������ FirePosPivot�� �����̼ǰ��� ���Ͱ�����  ��ȯ�Ͽ� RollVec�� �����ϰ� Move()���� �ش� ���Ͱ��� �̿��Ͽ� ������ �̵�
            RollOn = true;
        } // ������� ĳ���Ͱ� �̵��ϴ� ȿ���� Move()�� �־��
    }
    #region Movement
    void Move()
    {
        moveVec = (Vector2.up * v) + (Vector2.right * h);
        if (!StopMove) // ��� ���� �̵�. ������ ���� �̵��� �Է¹��� �ʱ� ����. StopMove = ������ �ִϸ��̼��� ������ �� Ture�� �ǰ� ������ �ִϸ��̼��� ������ ��������Ʈ �� False�� ��
        {
            tr.position += (Vector3)moveVec * moveSpeed * Time.deltaTime;
        }
        else // StopMove�� True�� ��Ȳ�� ������ �ִϸ��̼��� ���� ���� �� �ۿ� ���⿡ StopMove�� True�� ��, ĳ���Ͱ� ���콺 �������� ������ �̵��ϵ��� ���� �߾��. 
        {
            tr.position += (Vector3)RollVec * 50.0f * Time.deltaTime; // ������� �̵� ĳ���Ͱ� �̵��ϴ� ȿ��
        }

        if (wDown || aDown || sDown || dDown)// Ű���� �Է� -> �ȱ� �ִϸ��̼� ��ȯ
        {
            if (!isAnimOn) //�̹� �ִϸ��̼��� ��� ���ε� �� �õ��� �ɾ ù ��° ��������Ʈ�� ����Ǵ� �� �����ϱ� ����
            {
                anim.SetBool("isMove", true);
                anim.SetTrigger("W");
                isAnimOn = true; 

            }
        }
        else //Ű���忡�� �ƹ��͵� �Է¹ް� ���� �ʴ� ���� -> Idle�� ��ȯ
        {
            anim.SetBool("isMove", false);
            isAnimOn = false;
        }
    }
    void AttackOver() //���� ����� �̺�Ʈ �Լ� -> ���� �ִϸ��̼� �� ������ ��������Ʈ �� ȣ�� ��
    {
        anim.SetBool("isAttack", false);
    }
    void RollOver() //������ ����� �̺�Ʈ �Լ� ->������ �ִϸ��̼� �� ������ ��������Ʈ �� ȣ�� ��
    {
        rigid.velocity = Vector2.zero; //Ȥ�� �𸣴� �浹�� ���� ������ ��Ȳ�� ���� ���ؼ�
        StopMove = false;
        anim.SetBool("isRoll", false);
    }
    void AnimOver()// �ȱ�� Idle ����� �̺�Ʈ �Լ� -> ���� ����
    {
        isAnimOn = false;
    }
    void Turn()
    {

        Vector3 mPosition = Input.mousePosition; 
        Vector3 oPosition = transform.position; 
        mPosition.z = oPosition.z - Camera.main.transform.position.z;

        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        if( 45.0f<= rotateDegree && rotateDegree < 135.0f)//���콺�� PC���� ���� ���� ��
        {
            anim.SetInteger("Vertical", 1 );
            anim.SetInteger("Horizontal", 0);
            anim.SetTrigger("DirChange"); // ������ ����Ǿ����� �˸��� ���ؼ�
        }
        else if ( -135.0f < rotateDegree && rotateDegree < -45.0f) //���콺�� PC���� �Ʒ��� ���� ��
        {
            anim.SetInteger("Vertical", -1);
            anim.SetInteger("Horizontal", 0);
            anim.SetTrigger("DirChange");
        }
        else if (-45.0f < rotateDegree && rotateDegree <= 45.0f) //���콺�� PC���� �����ʿ� ���� ��
        {
            anim.SetInteger("Vertical", 0);
            anim.SetInteger("Horizontal", 1);
            anim.SetTrigger("DirChange");
        }
        else if ((rotateDegree <= 180.0f && rotateDegree > 135.0f) || (rotateDegree <= -135.0f && rotateDegree > -180.0f)) //���콺�� PC���� ���ʿ� ���� ��
        {
            anim.SetInteger("Vertical", 0);
            anim.SetInteger("Horizontal", -1);
            anim.SetTrigger("DirChange");
        }
        FirePosPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree); // ��� ���� ����, PC���� ���콺�� ���ϴ� ����� ����
    }

    #endregion

    IEnumerator BaseAttack()
    {
        while (true)
        {
            Instantiate(curBaseAttack, tr/*, ȸ����*/);//����� �÷��̾ �θ�� �α⿡ �÷��̾ ����ٴ�
            yield return new WaitForSeconds(4f);
        }
    }

}
