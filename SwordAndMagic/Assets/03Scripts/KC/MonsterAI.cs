using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private GameObject MonsterManager;
    //������ Ÿ�� ����
    public GameObject TraceTarget;
    //������ �ӵ�
    public float MonsterMoveSpeed;

    void Start()
    {
        //���� ����� �÷��̾�
        TraceTarget = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Trace();
    }

    public void Trace()
    {
        //�� ��ü ������ = moveToward�Ἥ ���� �������� �̵���ų ��
        //���� ���� : TraceTarget ����
        //new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0)
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(TraceTarget.transform.position.x, TraceTarget.transform.position.y, 0), 
            MonsterMoveSpeed * Time.deltaTime);
    }
}
