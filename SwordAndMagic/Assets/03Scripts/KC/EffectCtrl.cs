using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCtrl : MonoBehaviour
{
    Animator anim;
    public int AnimSet;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("animSet", AnimSet);
        transform.position += new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
        if(GameObject.FindGameObjectWithTag("Player").transform.position.x < transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Over()
    {
        Destroy(this.gameObject);
    }
}
