using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float moveSpeed = 0.0f;
    private Animator anim;
    Vector2 movement = new Vector2();
    Rigidbody2D rigidbody2D;
    Collider2D _Collider2D;
   
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _Collider2D = GetComponent<CircleCollider2D>();
        anim = this.GetComponent<Animator>();
        moveSpeed = PlayerStatus.instance.Move_Speed;
    }
    private void FixedUpdate()
    {      
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rigidbody2D.velocity = movement * moveSpeed;

        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
    }   
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("RewardBox"))
        {
            moveSpeed = 0.0f;            
        }       
    }
}
