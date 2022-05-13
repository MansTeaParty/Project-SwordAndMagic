using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{ 
    private int damge = 10;

    public float BaseTileSpeed;
    public float TileSpeed;

    [SerializeField]
    private GameObject TraceTarget;
    private Vector3 toPcVec;
    private bool isTrace;

    // Start is called before the first frame update
    void Start()
    {
        TraceTarget = GameObject.FindGameObjectWithTag("Player");
        isTrace = false;
        GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(Destroythis());
    }

    // Update is called once per frame
    void Update()
    {
        TileMove();
    }

    void TileMove()
    {
        if (!isTrace)
        {
            transform.Translate(Vector2.right * TileSpeed * Time.deltaTime);
        }
        else
        {
            transform.position -= toPcVec.normalized * TileSpeed * Time.deltaTime;
        }
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
        yield return new WaitForSeconds(15.0f);
        Destroy(this.gameObject);
    }

    public void SetTarget(bool send, float speed)
    {
        toPcVec = new Vector3
                   ( transform.position.x - TraceTarget.transform.position.x,
                     transform.position.y - TraceTarget.transform.position.y,
                    0);

        

        TileSpeed = speed;
        isTrace = send;
    }
}
