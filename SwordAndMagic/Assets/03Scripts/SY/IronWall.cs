using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronWall : MonoBehaviour
{
    public int attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x * PlayerStatus.instance.projectileScale, transform.localScale.y * PlayerStatus.instance.projectileScale, 1);

        Destroy(gameObject, 0.2f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.ToString());
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<MonsterCtrl>().MonsterDamaged(attackDamage);
        }
    }


    public void RecieveDamage(int dm)
    {
        attackDamage = dm;
    }
}
