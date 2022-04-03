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
    private bool AttackON = true; //공격 쿨타임 때문에 공격 쿨이 전부 돌았음을 알리는 변수. NormalAttack()가 실행될 때, false기 되고 attackSpeed초가 지나면 Ture
    private bool RollOn = true;  //구르기 쿨타임 때문에 구르기를 쿨이 전부 돌았음을 알리는 변수. Roll()이 실행될 때 false가 되고 1초가 지나면 True;
    private bool StopMove = false;
    //Move&Turn
    private Transform tr;
    private Rigidbody2D rigid;
    private Vector3 currPos;
    public Vector2 moveVec;

    public float moveSpeed = 1.0f;
    public float attackSpeed;

    private Vector2 RollVec; //구르는 방향을 담을 벡터

    [SerializeField]
    private GameObject FirePos; // 투사체가 생성되는 위치를 담고 있는 객체, FirePosPivot을 부모로 두고 있어서 FirePosPivot이 캐릭터 위치에서 마우스 방향 따라 회전하면 FirePos는 캐릭터를 중심으로 원을 그리며 공전

    [SerializeField]
    private GameObject FirePosPivot; // 캐릭터에 종속되어서 동일한 좌표값을 가지며 마우스 방향에 따라 회전 -> FirePosPivot = 사격 방향 

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
        //몬스터에게 닿거나 넉백되는 적에게 맞으면 플레이어가 밀려남
        //물리적 속도 초기화
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
                Instantiate(PowerAttack_Projectile, FirePos.transform.position, FirePosPivot.transform.rotation); // FirePos = 투사체의 생성위치, FirePosPivot = 투사체가 날라가는 방향 
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

            if (attackCount % 3 == 0) //강공격
            {
                Damage = 30;
                NowAttackObj = PowerAttack_Projectile;
            }
            else //약공격
            {
                Damage = 15;
                NowAttackObj = nomalAttack_Projectile;
            }

            //스폰하고 난 후 메시지 전달
            tempObj = Instantiate(NowAttackObj, FirePos.transform.position, FirePosPivot.transform.rotation);

            tempObj.SendMessage("RecieveDamage", Damage);

            yield return new WaitForSeconds(attackSpeed);
            AttackON = true;

        }
    }
    IEnumerator Roll() //구르기
    {
        if (RollOn)
        {
            RollOn = false; // 구르기 쿨타임을 위한 변수
            StopMove = true; // 구르는 애니메이션 동안 다른 애니메이션이 재생되지 않게 하기 위한 변수 -> 애니메이션 이벤트로 구르기 애니메이션의 마지막 스프라이트 재생 시, 호출하여 false 처리
            rigid.velocity = Vector2.zero; //혹시 모르는 충돌로 인한 좆같은 상황을 막기 위해서
            anim.SetBool("isRoll", true);
            anim.SetTrigger("Roll");
            RollVec = FirePosPivot.transform.TransformDirection(Vector2.right); // FirePosPivot이 PC에서 마우스로 향하는 로테이션 값을 가지고 있기에 구르기를 입력할 때, 
            yield return new WaitForSeconds(1.0f);                              // 해당 시점의 FirePosPivot의 로테이션값을 벡터값으로  변환하여 RollVec에 저장하고 Move()에서 해당 벡터값을 이용하여 구르기 이동
            RollOn = true;
        } // 구르기로 캐릭터가 이동하는 효과는 Move()에 있어요
    }
    #region Movement
    void Move()
    {
        moveVec = (Vector2.up * v) + (Vector2.right * h);
        if (!StopMove) // 평상 시의 이동. 구르는 동안 이동을 입력받지 않기 위함. StopMove = 구르기 애니메이션이 시작할 때 Ture가 되고 구르기 애니메이션의 마지막 스프라이트 때 False가 됨
        {
            tr.position += (Vector3)moveVec * moveSpeed * Time.deltaTime;
        }
        else // StopMove가 True인 상황은 구르기 애니메이션이 진행 중일 때 밖에 없기에 StopMove가 True일 때, 캐릭터가 마우스 방향으로 빠르게 이동하도록 설정 했어요. 
        {
            tr.position += (Vector3)RollVec * 50.0f * Time.deltaTime; // 구르기로 이동 캐릭터가 이동하는 효과
        }

        if (wDown || aDown || sDown || dDown)// 키보드 입력 -> 걷기 애니메이션 전환
        {
            if (!isAnimOn) //이미 애니메이션이 출력 중인데 또 시동을 걸어서 첫 번째 스프라이트만 재생되는 걸 방지하기 위함
            {
                anim.SetBool("isMove", true);
                anim.SetTrigger("W");
                isAnimOn = true; 

            }
        }
        else //키보드에서 아무것도 입력받고 있지 않는 상태 -> Idle로 전환
        {
            anim.SetBool("isMove", false);
            isAnimOn = false;
        }
    }
    void AttackOver() //공격 모션의 이벤트 함수 -> 공격 애니메이션 맨 마지막 스프라이트 때 호출 됨
    {
        anim.SetBool("isAttack", false);
    }
    void RollOver() //구르기 모션의 이벤트 함수 ->구르기 애니메이션 맨 마지막 스프라이트 때 호출 됨
    {
        rigid.velocity = Vector2.zero; //혹시 모르는 충돌로 인한 좆같은 상황을 막기 위해서
        StopMove = false;
        anim.SetBool("isRoll", false);
    }
    void AnimOver()// 걷기와 Idle 모션의 이벤트 함수 -> 위와 동일
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

        if( 45.0f<= rotateDegree && rotateDegree < 135.0f)//마우스가 PC보다 위에 있을 때
        {
            anim.SetInteger("Vertical", 1 );
            anim.SetInteger("Horizontal", 0);
            anim.SetTrigger("DirChange"); // 방향이 변경되었음을 알리기 위해서
        }
        else if ( -135.0f < rotateDegree && rotateDegree < -45.0f) //마우스가 PC보다 아래에 있을 때
        {
            anim.SetInteger("Vertical", -1);
            anim.SetInteger("Horizontal", 0);
            anim.SetTrigger("DirChange");
        }
        else if (-45.0f < rotateDegree && rotateDegree <= 45.0f) //마우스가 PC보다 오른쪽에 있을 때
        {
            anim.SetInteger("Vertical", 0);
            anim.SetInteger("Horizontal", 1);
            anim.SetTrigger("DirChange");
        }
        else if ((rotateDegree <= 180.0f && rotateDegree > 135.0f) || (rotateDegree <= -135.0f && rotateDegree > -180.0f)) //마우스가 PC보다 왼쪽에 있을 때
        {
            anim.SetInteger("Vertical", 0);
            anim.SetInteger("Horizontal", -1);
            anim.SetTrigger("DirChange");
        }
        FirePosPivot.transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree); // 사격 방향 설정, PC에서 마우스로 향하는 방향과 동일
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
