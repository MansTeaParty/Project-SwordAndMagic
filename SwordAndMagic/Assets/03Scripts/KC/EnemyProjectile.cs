using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private GameObject TraceTarget;

    int damge = 10;
    Vector3 toPcVec;
    // Start is called before the first frame update
    void Start()
    {
        TraceTarget = GameObject.FindGameObjectWithTag("Player");
        
        toPcVec = new Vector2(TraceTarget.transform.position.x - transform.position.x, TraceTarget.transform.position.y - transform.position.y);
        float angle = Mathf.Rad2Deg * (Mathf.Atan2(toPcVec.normalized.y, toPcVec.normalized.x));
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = angleAxis;
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(Destroythis());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += toPcVec.normalized * 50.0f * Time.deltaTime;
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
    }
    IEnumerator Destroythis()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}
