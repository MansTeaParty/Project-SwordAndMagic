using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_Attack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int penetration = 3;

    private PlayerCtrl PC;

    public int thisAttackDamage;

    void Start()
    {
        //PC = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        //penetration = PC.penetration;
        //transform.localScale = new Vector3(transform.localScale.x * PC.projectile_size, transform.localScale.y * PC.projectile_size, 1);

        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right *50.0f* Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Monster")
        {
            //Destroy(gameObject);
            collision.SendMessage("MonsterHPCal", thisAttackDamage);

            if (penetration > 0)
            {
                penetration -= 1;

                //collision.tag = "Dead";
                //Destroy(collision.gameObject);
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
        thisAttackDamage = dm;
    }
}
