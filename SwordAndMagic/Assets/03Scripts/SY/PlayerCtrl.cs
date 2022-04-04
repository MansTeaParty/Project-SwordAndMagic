using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using SYMath;

//������, �÷��̾� Ŭ����
//
//22/03/21
//���� ���� ������ ���ӸŴ������� �����ϴ� �� �մ��� ��

public class PlayerCtrl : MonoBehaviour
{

    public GameManager GameManager;
    
    [Header ("GetInput")]//GetInput
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

    
    [Header("Move&Turn")]//Move&Turn
    private Transform tr;
    private Rigidbody2D rigid;
    private Vector3 currPos;
    public Vector2 moveVec;
    public float moveSpeed = 1.0f;
    private bool Moveable = true;   /// ��� ���� �̵�. ������ ���� �̵��� �Է¹��� �ʱ� ����. 


    //Animation
    public Animator anim;
    private SpriteRenderer render;



    [Header("BaseAttack")]//BaseAttack
    public GameObject curBaseAttack;
    [SerializeField]
    private GameObject FirePos; // ����ü�� �����Ǵ� ��ġ�� ��� �ִ� ��ü, FirePosPivot�� �θ�� �ΰ� �־ FirePosPivot�� ĳ���� ��ġ���� ���콺 ���� ���� ȸ���ϸ� FirePos�� ĳ���͸� �߽����� ���� �׸��� ����
    [SerializeField]
    private GameObject FirePosPivot;    // ĳ���Ϳ� ���ӵǾ ������ ��ǥ���� ������ ���콺 ���⿡ ���� ȸ�� -> FirePosPivot = ��� ���� 
    private bool Attackable = true;     //�⺻�����ֱ�Ȯ�� NormalAttack()�� false, attackSpeed�ʰ� ������ True
    public GameObject normalAttackProjectile;



    [Header("Battle")]
    private bool isDamaged = false;
    private bool isDie = false;
    private Collider2D playerCollider;



    [Header("PlayerStatus")]//PlayerStatus
    public PlayerStatus PlayerStatus;
    
    public int playerLevel = 1;        //���� ���ӿ����� ���� = Game_Level
    public int playerExp = 0;          //���� ���ӿ����� ����ġ = Class_Exp

    public int currentHP = 100;
    public Slider HP_Bar;
    public Text HP_Text;

    /*
    public int maxHP = 100;
    public int armorPoint = 1;         //����
    public int attackDamage = 10;      //�⺻���ݷ�
    public float attackSpeed = 0.5f;   //���ݼӵ�
    public float movementSpeed = 1.0f;  //�̵��ӵ� ���


    [Header("PlayerSubStatus")]
    public int penetration = 1;        //����ü ���� Ƚ��
    public float projectileSpeed = 1.0f;//����ü �̵��ӵ� ���
    public float projectileScale = 1.0f;//����ü ũ�� ���
    public float knockBack = 0.0f;     //���ݽ� �˹�Ÿ�
    public float expBonus = 1.0f;      //�÷��̾� ȹ�� ����ġ�� ���
    public float goldBonus = 1.0f;     //�÷��̾� ȹ�� ��差 ���
    */

    //Attack


    //public GameManager gameManager;

    void Awake()
    {
        tr = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
        PlayerStatus = GetComponentInChildren<PlayerStatus>();

        HP_Bar.maxValue = PlayerStatus.maxHP;
        HP_Bar.value = currentHP;
        HP_Text.text = "HP : " + currentHP;
        //StartCoroutine("BaseAttack");
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();   //�̱���
    }

    void GetInput()
    {
        h = ((int)Input.GetAxisRaw("Horizontal"));
        v = ((int)Input.GetAxisRaw("Vertical"));
        wDown = Input.GetKeyDown(KeyCode.W);
        wUp = Input.GetKeyUp(KeyCode.W);
        sDown = Input.GetKeyDown(KeyCode.S);
        sUp = Input.GetKeyUp(KeyCode.S);

        aDown = Input.GetKeyDown(KeyCode.A);
        aUp = Input.GetKeyUp(KeyCode.A);
        dDown = Input.GetKeyDown(KeyCode.D);
        dUp = Input.GetKeyUp(KeyCode.D);

        if (Input.GetMouseButton(0)&&Attackable&& Moveable && !isDie)
        {
            StartCoroutine(BaseAttack());
        }
    }
    #region Movement
    void Move()
    {
        
    
        moveVec = (Vector2.up * v) + (Vector2.right * h);
        //tr.LookAt(tr.position + (Vector3)moveVec);
        //tr.TransformDirection(tr.position + (Vector3)moveVec);
        if (Moveable&&!isDie)
        {
            tr.position += (Vector3)moveVec * moveSpeed * Time.deltaTime;
            anim.SetInteger("Horizontal", h);
            anim.SetInteger("Vertical", v);

            if (wDown) anim.SetTrigger("W");
            if (aDown) anim.SetTrigger("A");
            if (sDown) anim.SetTrigger("S");
            if (dDown) anim.SetTrigger("D");
        }
        else    //��������
        {
            //tr.position += (Vector3)RollVec * 50.0f * Time.deltaTime; // ������� �̵� ĳ���Ͱ� �̵��ϴ� ȿ��
        }

        //anim.SetInteger("Horizontal", h);
        //anim.SetInteger("Vertical", v);
        
        //if(wDown)anim.SetTrigger("W");
        //if (aDown) anim.SetTrigger("A");
        //if (sDown) anim.SetTrigger("S");
        //if (dDown) anim.SetTrigger("D");


        
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


        FirePosPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree); // ��� ���� ����, PC���� ���콺�� ���ϴ� ����� ����
        /*
         //���콺 ������ �ٶ�
        //https://robotree.tistory.com/7
        //���� ����� ���� ���콺�� ���� ������Ʈ�� ������ ��ǥ�� �ӽ÷� �����մϴ�.
        Vector3 mPosition = Input.mousePosition; //���콺 ��ǥ ����
            Vector3 oPosition = transform.position; //���� ������Ʈ ��ǥ ����

            //ī�޶� �ո鿡�� �ڷ� ���� �ֱ� ������, ���콺 position�� z�� ������
            //���� ������Ʈ�� ī�޶���� z���� ���̸� �Է½������ �մϴ�.
            mPosition.z = oPosition.z - Camera.main.transform.position.z;

            //ȭ���� �ȼ����� ��ȭ�Ǵ� ���콺�� ��ǥ�� ����Ƽ�� ��ǥ�� ��ȭ�� ��� �մϴ�.
            //�׷���, ��ġ�� ã�ư� �� �ְڽ��ϴ�.
            Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

            //������ ��ũź��Ʈ(arctan, ��ź��Ʈ)�� ���� ������Ʈ�� ��ǥ�� ���콺 ����Ʈ�� ��ǥ��
            //�̿��Ͽ� ������ ���� ��, ���Ϸ�(Euler)ȸ�� �Լ��� ����Ͽ� ���� ������Ʈ�� ȸ����Ű��
            //����, �� ���� �Ÿ����� ���� �� ���Ϸ� ȸ���Լ��� �����ŵ�ϴ�.

            //�켱 �� ���� �Ÿ��� ����Ͽ�, dy, dx�� ������ �Ӵϴ�.
            float dy = target.y - oPosition.y;
            float dx = target.x - oPosition.x;

            //������ ȸ�� �Լ��� 0���� 180 �Ǵ� 0���� -180�� ������ �Է� �޴µ� ���Ͽ�
            //(���� 270�� ���� ���� �Էµ� ���� ���������ϴ�.) ��ũź��Ʈ Atan2()�Լ��� ��� ����
            //���� ��(180���� ����(3.141592654...)��)���� ��µǹǷ�
            //���� ���� ������ ��ȭ�ϱ� ���� Rad2Deg�� �����־�� ������ �˴ϴ�.
            float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

            //������ ������ ���Ϸ� ȸ�� �Լ��� �����Ͽ� z���� �������� ���� ������Ʈ�� ȸ����ŵ�ϴ�.
            transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
        */
    }

    #endregion

    IEnumerator BaseAttack()
    {

                Attackable = false;
                //anim.SetBool("isAttack", true);
                //anim.SetTrigger("Attack");
                Instantiate(normalAttackProjectile, FirePos.transform.position, FirePosPivot.transform.rotation);
                yield return new WaitForSeconds(PlayerStatus.attackSpeed);
                Attackable = true;

        
        //while (true)
        //{
        //    Instantiate(curBaseAttack, tr/*, ȸ����*/);//����� �÷��̾ �θ�� �α⿡ �÷��̾ ����ٴ�
        //    yield return new WaitForSeconds(4f);
        //}
        
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (!isDamaged)
            { 
                MonsterCtrl enemyObject = other.GetComponent<MonsterCtrl>();
                currentHP -= enemyObject.monsterDamage;
                HP_Bar.value = currentHP;   //MaxHp�� ���� �þ�� �纸���ϹǷ� xur/max�� ����
                HP_Text.text = "HP : " + currentHP;
                Debug.Log(currentHP+","+HP_Bar.value);
                if (currentHP <= 0)
                {
                    StartCoroutine(PlayerDie());
                }
                else
                {
                    StartCoroutine(OnDamage());
                }
            }
        }
    }
    IEnumerator OnDamage()  //�������� ���� ����
    {
        isDamaged = true;
        render.color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(1f);
        render.color = new Color(1, 1, 1, 1);
        isDamaged = false;

    }

    IEnumerator PlayerDie()
    {
        playerCollider.enabled = false;
        isDie = true;


        yield return new WaitForSeconds(1);
        while (render.color.a > 0)
        {
            var color = render.color;
            //color.a is 0 to 1. So .5*time.deltaTime will take 2 seconds to fade out
            color.a -= (1.0f * Time.deltaTime);

            render.color = color;
            //wait for a frame
            yield return null;
        }
        GameManager.playerDie();
    }
}
