using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using SYMath;

public class PlayerCtrl : MonoBehaviour
{
    
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


    //Anim
    public Animator anim;

    //BaseAttack
    public GameObject curBaseAttack;

    
    [Header("PlayerStatus")]//Move&Turn
    public int playerLevel = 1;        //���� ���ӿ����� ���� = Game_Level
    public int playerExp = 0;          //���� ���ӿ����� ����ġ = Class_Exp
    public int maxHP = 100;
    public int currentHP = 100;
    public int armorPoint = 1;         //����
    public int attackDamage = 10;      //�⺻���ݷ�
    public float attackSpeed = 3.0f;   //���ݼӵ�
    public float movementSpeed = 1.0f;  //�̵��ӵ� ���


    [Header("PlayerSubStatus")]
    public int penetration = 1;        //����ü ���� Ƚ��
    public float projectileSpeed = 1.0f;//����ü �̵��ӵ� ���
    public float projectileScale = 1.0f;//����ü ũ�� ���
    public float knockBack = 0.0f;     //���ݽ� �˹�Ÿ�
    public float expBonus = 1.0f;      //�÷��̾� ȹ�� ����ġ�� ���
    public float goldBonus = 1.0f;     //�÷��̾� ȹ�� ��差 ���
 


    //public GameManager gameManager;

    void Awake()
    {
        tr = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        StartCoroutine("BaseAttack");
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
        //Turn();   //�̱���
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

    }
    #region Movement
    void Move()
    {
        
    
        moveVec = (Vector2.up * v) + (Vector2.right * h);
        //tr.LookAt(tr.position + (Vector3)moveVec);
        //tr.TransformDirection(tr.position + (Vector3)moveVec);
        tr.position += (Vector3)moveVec * moveSpeed * Time.deltaTime;
        anim.SetInteger("Horizontal", h);
        anim.SetInteger("Vertical", v);
        
        if(wDown)anim.SetTrigger("W");
        if (aDown) anim.SetTrigger("A");
        if (sDown) anim.SetTrigger("S");
        if (dDown) anim.SetTrigger("D");

        
        //if (movevec.magnitude > 0)
        //{
        //    anim.setfloat("speed", 1.0f);
        //}
        //else
        //{
        //    anim.setfloat("speed", 0.0f);
        //}
        //if (tr.position != currpos)
        //{
        //    anim.setfloat("speed", 1.0f);
        //    vector3.lerp(tr.position, currpos, time.deltatime * 10.0f);
        //}
        //else
        //{
        //    anim.setfloat("speed", 0.0f);
        //}
        
    }

    void Turn()
    {

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
