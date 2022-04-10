using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    [SerializeField]
    private int penetration = 3;

    private GameObject[] monster;

    private PlayerCtrl PlayerCharacter;

    public int attackDamage;
    public float attackSpeed;

    int a = 0;
    bool hit;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerCharacter = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        //penetration = PlayerCharacter.penetration;
        //transform.localScale = new Vector3(transform.localScale.x * PC.projectile_size, transform.localScale.y * PC.projectile_size, 1);

        //Destroy(gameObject, 1);
        Destroy(gameObject, 5.0f);

        System.Array.Resize(ref monster, penetration);
        GetComponent<BoxCollider2D>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * attackSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.ToString());
        /*if (collision.gameObject.tag == "Enemy")
         {
             collision.GetComponent < MonsterCtrl>().MonsterDamaged(attackDamage);
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
         }*/
        if (hit == false)
        {
            hit = true;
            if (collision.gameObject.tag == "Monster")
            {
                if (penetration > 0)
                {
                    int index = System.Array.IndexOf(monster, collision.gameObject);
                    if (index == -1)
                    {
                        monster[a] = collision.gameObject;
                        a += 1;
                        penetration -= 1;      //몬스터 스크립트에 데미지와 넉백거리를 전달하며 몬스터 피격처리 실행//
                        //collision.gameObject.GetComponent<MonsterAI>().Hit(this.gameObject, attackDamage, knockBack);

                        collision.GetComponent<MonsterStat>().Hit(attackDamage);
                        if (penetration <= 0)
                        {
                            Destroy(this.gameObject);
                        }
                    }
                }
            }
            hit = false;
        }
    }


    public void RecieveDamage(int dm)
    {
        attackDamage = dm;
    }
}
