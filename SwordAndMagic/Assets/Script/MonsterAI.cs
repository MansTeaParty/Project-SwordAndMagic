using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private GameObject MonsterManager;
    //추적할 타깃 변수
    public GameObject TraceTarget;
    //추적할 속도
    public float MonsterMoveSpeed;

    void Start()
    {
        //추적 대상은 플레이어
        TraceTarget = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Trace();
    }

    public void Trace()
    {
        //이 객체 포지션 = moveToward써서 지정 방향으로 이동시킬 것
        //지정 방향 : TraceTarget 방향
        //new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0)
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0), 
            MonsterMoveSpeed * Time.deltaTime);


    }
}
