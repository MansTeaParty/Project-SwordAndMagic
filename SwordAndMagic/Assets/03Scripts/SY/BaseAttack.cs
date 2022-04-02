using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    [SerializeField]
    private int penetration = 3;

    private PlayerCtrl PlayerCharacter;

    public int attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerCharacter = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        //penetration = PlayerCharacter.penetration;
        //transform.localScale = new Vector3(transform.localScale.x * PC.projectile_size, transform.localScale.y * PC.projectile_size, 1);

        //Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * 10.0f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.ToString());
        if (collision.gameObject.tag == "Enemy")
        {
            collision.SendMessage("MonsterHPCal", attackDamage);
            if (penetration > 0)
            {
                penetration -= 1;
                //collision.tag = "Dead"; //데미지 방식????
                Destroy(collision.gameObject);
                if (penetration <= 0)
                {
                    Destroy(this.gameObject);
                }

            }
            else
            {

            }
        }
    }


    public void RecieveDamage(int dm)
    {
        attackDamage = dm;
    }
}
