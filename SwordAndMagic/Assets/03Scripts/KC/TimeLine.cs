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
        TimeLineCtrler = GameObject.FindGameObjectWithTag("TimeLineController");

        //poolNum = 0; -> �̰� ���� ����Ʈ�� �� �� ���ư�.
        //�̰� �ּ�ó���ϴϱ� ����Ʈ �ߵ� ����?
        //�ƴ� �ʱ�ȭ ���൵ ����, �� �� �൵ ����
        //�˴ٰ��� �𸣰ڳ� ����

        isNextPool = false;
    }

    public void NextPool()
    {
        isNextPool = true;

        //Debug.Log("poolNum : " + poolNum);
        NowSpawnPool = MonsterPoolSettingList[poolNum];
        //Debug.Log(NowSpawnPool);

        if (isNextPool)
        {
            PoolControl();
            poolNum++;
            //Debug.Log("poolNum : " + poolNum);
            isNextPool = false;
        }

    }

    void PoolControl()
    {
        GameObject MonsterPool_ABC = Instantiate(NowSpawnPool, this.transform.position, Quaternion.identity);
        MonsterPool_ABC.transform.SetParent(this.transform, false);
    }
}
