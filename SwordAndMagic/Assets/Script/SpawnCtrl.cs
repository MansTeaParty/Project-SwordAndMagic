using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCtrl : MonoBehaviour
{
    //����Ǯ�� ���� A,B,C��
    public enum MonsterPool { MonsterPool_A, MonsterPool_B, MonsterPool_C };
    public MonsterPool MonsterPool_State; //����Ǯ�� ���� ����

    //������ ���� ����
    public GameObject Monster_A;
    public GameObject Monster_B;
    public GameObject Monster_C;

    private bool SpawnApprove; //�����ص� �ȴٴ� ���� ����

    //������ ���� ��
    private int SpawnValue_A;
    private int SpawnValue_B;
    private int SpawnValue_C;

    private int Stage_num;

    void Start()
    {
        SpawnValue_A = 0;
        SpawnValue_B = 0;
        SpawnValue_C = 0;
        //�ʱ� ���´� A��
        MonsterPool_State = MonsterPool.MonsterPool_A;
        SpawnApprove = false;
    }

    void Update()
    {
        if (SpawnApprove)// ���ͻ��� ���ε�
        {
            for (int i = 0; i < 1; i++) //�ѹ��� ��
            {
                MonsterPoolCheck();
            }
        }
    }

    void MonsterPoolCheck()
    {
        switch(MonsterPool_State)
        {
            case MonsterPool.MonsterPool_A:
                //���� �ʱ�ȭ
                SpawnValue_A = 4;
                SpawnValue_B = 2;
                SpawnValue_C = 1;
                //���� ����
                MonsterSpawnStart();
                MonsterPool_State = MonsterPool.MonsterPool_B;
                SpawnApprove = false;
                break;

            case MonsterPool.MonsterPool_B:
                //���� �ʱ�ȭ
                SpawnValue_A = 6;
                SpawnValue_B = 2;
                SpawnValue_C = 0;
                //���� ����
                MonsterSpawnStart();
                MonsterPool_State = MonsterPool.MonsterPool_C;
                SpawnApprove = false;
                break;

            case MonsterPool.MonsterPool_C:
                //���� �ʱ�ȭ
                SpawnValue_A = 12;
                SpawnValue_B = 0;
                SpawnValue_C = 0;
                //���� ����
                MonsterSpawnStart();
                MonsterPool_State = MonsterPool.MonsterPool_A;
                SpawnApprove = false;
                break;

            default:
                break;
        }
    }

    void MonsterSpawnStart()
    {
        for (int i = 0; i < SpawnValue_A; i++)
        {
            Instantiate(Monster_A, GetRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < SpawnValue_B; i++)
        {
            Instantiate(Monster_B, GetRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < SpawnValue_C; i++)
        {
            Instantiate(Monster_C, GetRandomPosition(), Quaternion.identity);
        }

    }

    public Vector3 GetRandomPosition()
    {
        float radius = 100f;
        Vector3 SpawnerPosition = transform.position;

        float a = SpawnerPosition.x;
        float b = SpawnerPosition.y;

        float x = Random.Range(-radius + a, radius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - a, 2));
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;

        Vector3 randomPosition = new Vector3(x, y, 0);

        return randomPosition;
    }



    public void SpawnCheck(int num)
    {
        print("���� ���� ����!");
        Stage_num = num;
        print(Stage_num + " ��������");
        SpawnApprove = true;    }
}
