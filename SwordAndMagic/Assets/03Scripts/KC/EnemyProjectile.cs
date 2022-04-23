using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{ 
    private int damge = 10;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(Destroythis());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate( Vector2.right * 40.0f * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            /*if (!collision.gameObject.GetComponent<PlayerCtrl>().isRoll)
            {
                collision.gameObject.GetComponent<PlayerCtrl>().Hit(damge);
                Destroy(this.gameObject);
            }*/
        }
        else if (collision.tag == "PlayerAttack")
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator Destroythis()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
