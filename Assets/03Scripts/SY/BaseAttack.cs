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
    public float movementSpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.ToString());
        if (hit == false)
        {
            hit = true;
            if (collision.gameObject.tag == "Enemy")
            {
                if (penetration > 0)
                {
                    int index = System.Array.IndexOf(monster, collision.gameObject);
                    if (index == -1)
                    {
                        monster[a] = collision.gameObject;
                        a += 1;
                        penetration -= 1;
                        collision.GetComponent<MonsterCtrl>().Hit(attackDamage);
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

    public void setPenetration(int penetrationValue)
    {
        penetration = penetrationValue;
    }

}
