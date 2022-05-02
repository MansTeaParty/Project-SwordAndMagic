using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject TraceTarget;

    public int damge = 10;
    public int Type;
    public float Speed = 50f;
    Vector3 toPcVec;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(Destroythis());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            /*if (!collision.gameObject.GetComponent<PlayerCtrl>().isRoll)
            {
                collision.gameObject.GetComponent<PlayerCtrl>().Hit(damge);
                if(Type == 0)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    Speed = 0f;
                    GetComponent<Animator>().SetTrigger("End");
                }
            }*/
        }

        else if (collision.tag == "PlayerAttack")
        {
            //Destroy(this.gameObject);
        }
    }
    IEnumerator Destroythis()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }
    void over()
    {
        Destroy(this.gameObject);
    }
}
