using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    public List<GameObject> MonsterPoolSettingList;
    public GameObject NowSpawnPool; 

    private bool isNextPool;

    public int poolNum;
    void Start()
    {
        isNextPool = false;
    }

    //���� �Ŵ����� ���� �ð����� ȣ���Ͽ� ���� Ǯ�� ������.
    public void NextPool()
    {
        isNextPool = true;

        NowSpawnPool = MonsterPoolSettingList[poolNum];

        if (isNextPool)
        {
            PoolControl();      //����Ǯ ����  
            poolNum++;          //���� ���� Ǯ�� ������ ���� ����Ǯ �ε��� +1 ��Ŵ.
                                //�׷� ���� ȣ�� �� ���� ���� Ǯ�� ������ ����.
            isNextPool = false;
        }

    }

    void PoolControl()
    {
        GameObject MonsterPool_ABC = Instantiate(NowSpawnPool, new Vector3(0,0,0), Quaternion.identity);
        MonsterPool_ABC.transform.SetParent(this.transform, false);
    }
}
