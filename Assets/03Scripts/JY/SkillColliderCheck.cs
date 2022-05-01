using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillColliderCheck : MonoBehaviour
{    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Monster"))
        {                 
            Destroy(gameObject); 
        }
    }
}
