using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    public GameObject TimeLineCtrler;

    public List<GameObject> MonsterPoolSettingList;
    public GameObject NowSpawnPool; 

    private bool isNextPool;

    public int poolNum;
    void Start()
    {
        //TimeLineCtrler = GameObject.FindGameObjectWithTag("TimeLineController");
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
        GameObject MonsterPool_ABC = Instantiate(NowSpawnPool, this.transform.position, Quaternion.identity);
        MonsterPool_ABC.transform.SetParent(this.transform, false);
    }
}
