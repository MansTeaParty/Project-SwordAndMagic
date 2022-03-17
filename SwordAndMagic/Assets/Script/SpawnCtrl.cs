using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCtrl : MonoBehaviour
{
    //몬스터풀의 종류 A,B,C형
    public enum MonsterPool { MonsterPool_A, MonsterPool_B, MonsterPool_C };
    public MonsterPool MonsterPool_State; //몬스터풀의 현재 상태

    //생성할 몬스터 종류
    public GameObject Monster_A;
    public GameObject Monster_B;
    public GameObject Monster_C;

    private bool SpawnApprove; //생성해도 된다는 승인 변수

    //스폰할 몬스터 수
    private int SpawnValue_A;
    private int SpawnValue_B;
    private int SpawnValue_C;

    private int Stage_num;

    void Start()
    {
        SpawnValue_A = 0;
        SpawnValue_B = 0;
        SpawnValue_C = 0;
        //초기 상태는 A형
        MonsterPool_State = MonsterPool.MonsterPool_A;
        SpawnApprove = false;
    }

    void Update()
    {
        if (SpawnApprove)// 몬스터생성 승인됨
        {
            for (int i = 0; i < 1; i++) //한번만 콜
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
                //변수 초기화
                SpawnValue_A = 4;
                SpawnValue_B = 2;
                SpawnValue_C = 1;
                //스폰 시작
                MonsterSpawnStart();
                MonsterPool_State = MonsterPool.MonsterPool_B;
                SpawnApprove = false;
                break;

            case MonsterPool.MonsterPool_B:
                //변수 초기화
                SpawnValue_A = 6;
                SpawnValue_B = 2;
                SpawnValue_C = 0;
                //스폰 시작
                MonsterSpawnStart();
                MonsterPool_State = MonsterPool.MonsterPool_C;
                SpawnApprove = false;
                break;

            case MonsterPool.MonsterPool_C:
                //변수 초기화
                SpawnValue_A = 12;
                SpawnValue_B = 0;
                SpawnValue_C = 0;
                //스폰 시작
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
        print("몬스터 스폰 시작!");
        Stage_num = num;
        print(Stage_num + " 스테이지");
        SpawnApprove = true;    }
}
