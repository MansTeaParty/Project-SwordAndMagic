using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolCtrl : MonoBehaviour
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
    public int SpawnValue_A;
    public int SpawnValue_B;
    public int SpawnValue_C;

    private int Stage_num;

    private GameObject MonsterManager;

    void Start()
    {
        MonsterManager = GameObject.FindGameObjectWithTag("MonsterManager");
        SpawnApprove = true;
    }

    void Update()
    {
        if (SpawnApprove == true)
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
                //스폰 시작
                StartCoroutine(MonsterSpawnStart());
                SpawnApprove = false;
                break;

            case MonsterPool.MonsterPool_B:
                StartCoroutine(MonsterSpawnStart());
                SpawnApprove = false;
                break;

            case MonsterPool.MonsterPool_C:
                StartCoroutine(MonsterSpawnStart());
                SpawnApprove = false;
                break;

            default:
                break;
        }
    }

    IEnumerator MonsterSpawnStart()
    {
        while (SpawnApprove == true)
        {
            for (int i = 0; i < SpawnValue_A; i++)
            {
                //소환되는 모든 몬스터는 씬에서 활성화되어 있는 MonsterManager를
                //부모로 하여 MonsterManager 객체 밑에 생성됨.
                GameObject Mons_A = Instantiate(Monster_A, GetRandomPosition(), Quaternion.identity);
                Mons_A.transform.SetParent(MonsterManager.transform, false);
                yield return new WaitForSeconds(1f);
            }
            for (int i = 0; i < SpawnValue_B; i++)
            {
                GameObject Mons_B = Instantiate(Monster_B, GetRandomPosition(), Quaternion.identity);
                Mons_B.transform.SetParent(MonsterManager.transform, false);
                yield return new WaitForSeconds(1f);
            }
            for (int i = 0; i < SpawnValue_C; i++)
            {
                GameObject Mons_C = Instantiate(Monster_C, GetRandomPosition(), Quaternion.identity);
                Mons_C.transform.SetParent(MonsterManager.transform, false); 
                yield return new WaitForSeconds(1f);
            }

            //Destroy(gameObject);
            //지우면 플레이 했을 때 리스트대로 나오는지 확인가능
            //활성화 하면 몬스터 풀에 할당된 모든 몬스터가 다 소환 완료 되면 
            //객체 스스로 지워짐

        }
    }

    //스폰할 위치 결정
    public Vector3 GetRandomPosition()
    {
        float radius = 140f;
        //스폰할 위치 기준이 어디?
        //플레이어 주위 원 반경 밖에서 랜덤 스폰할 거
        //근데 스포너는 캐릭터를 따라다니고 있음 -> 캐릭터 위치 = 스포너 위치
        //즉 스포너 위치를 캐릭터 위치 대신 써도 된다.
        Vector3 SpawnerPosition = transform.position;

        //랜덤 위치를 정하려면 원 반지름 길이만 알면 되는게 아니라
        //어디를 기준으로 반지름 길이 만큼 밖에서 스폰할것인지?
        //스포너의 현 위치를 기준으로 반지름 만큼 떨어진 위치에서 랜덤 생성
        float anchorPosX = SpawnerPosition.x;
        float anchorPosY = SpawnerPosition.y;

        //랜덤 생성 위치의 수평 지점 계산
        //anchorPosX에서 반지름 만큼 빼거나 더한 값이
        //랜덤스폰할 좌표의 y좌표
        float x = Random.Range(-radius + anchorPosX, radius + anchorPosX);

        //랜덤 생성 위치의 수직 지점 계산
        //Mathf.Sqrt : 제곱근 반환
        //Mathf.Pow(A,B)  : A의 B승 반환
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - anchorPosX, 2));

        //y_b에 곱할건데 뭘 곱하냐 -1 또는 1을 곱할거
        //Random.Range(0, 2) -> 0 또는 1이 나오게 함.
        //삼항연산자는 Random.Range(0, 2)통해 나온 값이 0이냐? 라고 묻는것.
        //0이 나오면 참이므로 -1을 y_b에 곱하는 것이고
        //1이 나오면 거짓이므로 1을 y_b에 곱하는 것.
        //왜 이렇게 하느냐?
        //몬스터가 위에서만 나오는 법이 없으므로 아래에서도 나오게 하기 위해서
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;

        //랜덤스폰할 좌표의 y좌표
        float y = y_b + anchorPosY;

        Vector3 randomPosition = new Vector3(x, y, 0);

        return randomPosition;
    }

    /*public void SpawnCheck()
    {
        print("몬스터 스폰 시작!");
        //Stage_num = num;
        //print(Stage_num + " 스테이지");
        SpawnApprove = true;    
    }*/
}
