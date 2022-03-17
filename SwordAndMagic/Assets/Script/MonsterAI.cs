using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public GameObject TraceTarget;
    public float MonsterMoveSpeed;

    void Start()
    {
        TraceTarget = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Trace();
    }

    public void Trace()
    {
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0), 
            MonsterMoveSpeed * Time.deltaTime);


    }
}
