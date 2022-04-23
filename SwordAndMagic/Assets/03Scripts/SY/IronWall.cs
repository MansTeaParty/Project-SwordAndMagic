using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronWall : MonoBehaviour
{
    public static IronWall instance=null;
    //적을 밀쳐내는 스킬(철벽)
    public int attackDamage;

    public IndividualSkill parentIndividualSkill;


    void singleton()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        singleton();

        transform.localScale = new Vector3(transform.localScale.x * PlayerStatus.instance.projectileScale, transform.localScale.y * PlayerStatus.instance.projectileScale, 1);
        parentIndividualSkill = GetComponentInParent<IndividualSkill>();

        if (parentIndividualSkill.IronWallDamage)
        {
            //Debug.Log("damage!!!");
            attackDamage = (2 * (10 + Mathf.FloorToInt(PlayerStatus.instance.getAttackDamage() /10)));
        }
        //attackDamage = (2 * (10 + Mathf.FloorToInt(PlayerStatus.instance.attackDamage / 10)));
        Destroy(gameObject, 0.2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.ToString());
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<MonsterStat>().Hit(attackDamage);
        }
    }


    public void RecieveDamage(int dm)
    {
        attackDamage = dm;
    }


}
