using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolCtrl : MonoBehaviour
{
    //����Ǯ�� ���� A,B,C��
    public enum MonsterPool { MonsterPool_A, MonsterPool_B, MonsterPool_C };
    public MonsterPool MonsterPool_State; //����Ǯ�� ���� ����

    //������ ���� ����
    public GameObject Monster_A;
    public GameObject Monster_B;
    public GameObject Monster_C;
    public GameObject Monster_D;

    public bool SpawnApprove; //�����ص� �ȴٴ� ���� ����

    //������ ���� ��
    public int SpawnValue_A;
    public int SpawnValue_B;
    public int SpawnValue_C;
    public int SpawnValue_D;

    private GameObject MonsterManager;

    void Start()
    {
        MonsterManager = GameObject.FindGameObjectWithTag("MonsterManager");
        SpawnApprove = true;

        MonsterPoolCheck();
    }

    void MonsterPoolCheck()
    {
        switch(MonsterPool_State)
        {
            case MonsterPool.MonsterPool_A:
                //���� ����
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
        if(SpawnApprove == true)
        {
            for (int i = 0; i < SpawnValue_A; i++)
            {
                //��ȯ�Ǵ� ��� ���ʹ� ������ Ȱ��ȭ�Ǿ� �ִ� MonsterManager��
                //�θ�� �Ͽ� MonsterManager ��ü �ؿ� ������.
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
            for (int i = 0; i < SpawnValue_D; i++)
            {
                GameObject Mons_D = Instantiate(Monster_D, GetRandomPosition(), Quaternion.identity);
                Mons_D.transform.SetParent(MonsterManager.transform, false);
                yield return new WaitForSeconds(1f);
            }


            Destroy(gameObject);
            //����� �÷��� ���� �� ����Ʈ��� �������� Ȯ�ΰ���
            //Ȱ��ȭ �ϸ� ���� Ǯ�� �Ҵ�� ��� ���Ͱ� �� ��ȯ �Ϸ� �Ǹ� 
            //��ü ������ ������
        }
    }

    //������ ��ġ ����
    //�÷��̾� ��ġ ���� Ÿ�������� ����
    public Vector3 GetRandomPosition()
    {
        float radius = 140f;
        //������ ��ġ ������ ���?
        //�÷��̾� ���� �� �ݰ� �ۿ��� ���� ������ ��
        //�ٵ� �����ʴ� ĳ���͸� ����ٴϰ� ���� -> ĳ���� ��ġ = ������ ��ġ
        //�� ������ ��ġ�� ĳ���� ��ġ ��� �ᵵ �ȴ�.
        Vector3 SpawnerPosition = transform.position;

        //���� ��ġ�� ���Ϸ��� �� ������ ���̸� �˸� �Ǵ°� �ƴ϶�
        //��� �������� ������ ���� ��ŭ �ۿ��� �����Ұ�����?
        //�������� �� ��ġ�� �������� ������ ��ŭ ������ ��ġ���� ���� ����
        float anchorPosX = SpawnerPosition.x;
        float anchorPosY = SpawnerPosition.y;

        //���� ���� ��ġ�� ���� ���� ���
        //anchorPosX���� ������ ��ŭ ���ų� ���� ����
        //���������� ��ǥ�� y��ǥ
        float x = Random.Range(-radius + anchorPosX, radius + anchorPosX);

        //���� ���� ��ġ�� ���� ���� ���
        //Mathf.Sqrt : ������ ��ȯ
        //Mathf.Pow(A,B)  : A�� B�� ��ȯ
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - anchorPosX, 2));

        //y_b�� ���Ұǵ� �� ���ϳ� -1 �Ǵ� 1�� ���Ұ�
        //Random.Range(0, 2) -> 0 �Ǵ� 1�� ������ ��.
        //���׿����ڴ� Random.Range(0, 2)���� ���� ���� 0�̳�? ��� ���°�.
        //0�� ������ ���̹Ƿ� -1�� y_b�� ���ϴ� ���̰�
        //1�� ������ �����̹Ƿ� 1�� y_b�� ���ϴ� ��.
        //�� �̷��� �ϴ���?
        //���Ͱ� �������� ������ ���� �����Ƿ� �Ʒ������� ������ �ϱ� ���ؼ�
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;

        //���������� ��ǥ�� y��ǥ
        float y = y_b + anchorPosY;

        //y���� �������μ� Ÿ�������� �����ǵ���
        Vector3 randomPosition = new Vector3(x, y/1.4f, 0);

        return randomPosition;
    }
}
