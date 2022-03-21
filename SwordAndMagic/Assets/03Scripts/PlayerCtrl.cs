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
    public int playerLevel = 1;        //현재 게임에서의 레벨 = Game_Level
    public int playerExp = 0;          //현재 게임에서의 경험치 = Class_Exp
    public int maxHP = 100;
    public int currentHP = 100;
    public int armorPoint = 1;         //방어력
    public int attackDamage = 10;      //기본공격력
    public float attackSpeed = 3.0f;   //공격속도
    public float movementSpeed = 1.0f;  //이동속도 계수


    [Header("PlayerSubStatus")]
    public int penetration = 1;        //투사체 관통 횟수
    public float projectileSpeed = 1.0f;//투사체 이동속도 계수
    public float projectileScale = 1.0f;//투사체 크기 계수
    public float knockBack = 0.0f;     //공격시 넉백거리
    public float expBonus = 1.0f;      //플레이어 획득 경험치량 계수
    public float goldBonus = 1.0f;     //플레이어 획득 골드량 계수
 


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
        //Turn();   //미구현
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
        //먼저 계산을 위해 마우스와 게임 오브젝트의 현재의 좌표를 임시로 저장합니다.
        Vector3 mPosition = Input.mousePosition; //마우스 좌표 저장
            Vector3 oPosition = transform.position; //게임 오브젝트 좌표 저장

            //카메라가 앞면에서 뒤로 보고 있기 때문에, 마우스 position의 z축 정보에
            //게임 오브젝트와 카메라와의 z축의 차이를 입력시켜줘야 합니다.
            mPosition.z = oPosition.z - Camera.main.transform.position.z;

            //화면의 픽셀별로 변화되는 마우스의 좌표를 유니티의 좌표로 변화해 줘야 합니다.
            //그래야, 위치를 찾아갈 수 있겠습니다.
            Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);

            //다음은 아크탄젠트(arctan, 역탄젠트)로 게임 오브젝트의 좌표와 마우스 포인트의 좌표를
            //이용하여 각도를 구한 후, 오일러(Euler)회전 함수를 사용하여 게임 오브젝트를 회전시키기
            //위해, 각 축의 거리차를 구한 후 오일러 회전함수에 적용시킵니다.

            //우선 각 축의 거리를 계산하여, dy, dx에 저장해 둡니다.
            float dy = target.y - oPosition.y;
            float dx = target.x - oPosition.x;

            //오릴러 회전 함수를 0에서 180 또는 0에서 -180의 각도를 입력 받는데 반하여
            //(물론 270과 같은 값의 입력도 전혀 문제없습니다.) 아크탄젠트 Atan2()함수의 결과 값은
            //라디안 값(180도가 파이(3.141592654...)로)으로 출력되므로
            //라디안 값을 각도로 변화하기 위해 Rad2Deg를 곱해주어야 각도가 됩니다.
            float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

            //구해진 각도를 오일러 회전 함수에 적용하여 z축을 기준으로 게임 오브젝트를 회전시킵니다.
            transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree);
    }

    #endregion

    IEnumerator BaseAttack()
    {
        while (true)
        {
            Instantiate(curBaseAttack, tr/*, 회전값*/);//현재는 플레이어를 부모로 두기에 플레이어를 따라다님
            yield return new WaitForSeconds(4f);
        }
    }

}
