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

    private bool SpawnApprove; //�����ص� �ȴٴ� ���� ����

    //������ ���� ��
    public int SpawnValue_A;
    public int SpawnValue_B;
    public int SpawnValue_C;

    private int Stage_num;

    void Start()
    { 
        SpawnApprove = true;
    }

    void Update()
    {
        if (SpawnApprove == true)
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
                //���� ����
                StartCoroutine(MonsterSpawnStart());
                SpawnApprove = false;
                break;

            case MonsterPool.MonsterPool_B:
                //���� ����
                StartCoroutine(MonsterSpawnStart());
                SpawnApprove = false;
                break;

            case MonsterPool.MonsterPool_C:
                //���� ����
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
                Instantiate(Monster_A, GetRandomPosition(), Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
            for (int i = 0; i < SpawnValue_B; i++)
            {
                Instantiate(Monster_B, GetRandomPosition(), Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
            for (int i = 0; i < SpawnValue_C; i++)
            {
                Instantiate(Monster_C, GetRandomPosition(), Quaternion.identity);
                yield return new WaitForSeconds(1f);
            }
        }
    }

    //������ ��ġ ����
    public Vector3 GetRandomPosition()
    {
        float radius = 100f;
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

        Vector3 randomPosition = new Vector3(x, y, 0);

        return randomPosition;
    }

    /*public void SpawnCheck()
    {
        print("���� ���� ����!");
        //Stage_num = num;
        //print(Stage_num + " ��������");
        SpawnApprove = true;    
    }*/
}
