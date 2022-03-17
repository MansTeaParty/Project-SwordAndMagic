using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SYMath;

public class PlayerCtrl : MonoBehaviour
{
    //GetInput
    public int h = 0;
    public int v = 0;
    public bool wDown;
    public bool aDown;
    public bool sDown;
    public bool dDown;
    public bool wUp;
    public bool aUp;
    public bool sUp;
    public bool dUp;

    //Move&Turn
    private Transform tr;
    private Rigidbody2D rigid;
    private Vector3 currPos;
    public Vector2 moveVec;
    public float moveSpeed = 1.0f;


    //Anim
    public Animator anim;


    //public GameManager gameManager;

    void Awake()
    {
        tr = GetComponent<Transform>();
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody2D>();
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

        //if (moveVec.magnitude > 0)
        //{
        //    anim.SetFloat("Speed", 1.0f);
        //}
        //else
        //{
        //    anim.SetFloat("Speed", 0.0f);
        //}


        if (tr.position != currPos)
        {
            anim.SetFloat("Speed", 1.0f);
            Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
        }
        else
        {
            anim.SetFloat("Speed", 0.0f);
        }
        
    }


}
