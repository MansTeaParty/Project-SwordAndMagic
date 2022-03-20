using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLine : MonoBehaviour
{
    public GameObject TimeLineCtrler;

    public List<GameObject> MonsterPoolSettingList;
    public GameObject NowSpawnPool; 

    private bool canIMoveNextPool;

    private int poolNum;
    void Start()
    {
        TimeLineCtrler = GameObject.FindGameObjectWithTag("TimeLineController");


        /*for (int i = 0; i < MonsterPoolSettingList.Count; i++)
        {
            MonsterPoolSettingList[i].SetActive(false);// ����Ʈ�� ���� ���� ��Ȱ��ȭ
        }*/

        canIMoveNextPool = false;
        poolNum = 0;
    }

    void Update()
    {
        if (canIMoveNextPool == true)
        {
            for (int i = 0; i < 1; i++)
            {
                PoolControl();

                canIMoveNextPool = false;
                poolNum++;
            }
        }
    }

    void PoolControl()
    {
        //����Ʈ�� ���ҵ��� ����
        GameObject MonsterPool_ABC = Instantiate(NowSpawnPool, this.transform.position, Quaternion.identity);
        MonsterPool_ABC.transform.SetParent(this.transform, false);
    }

    void NextPool()
    {
        if (canIMoveNextPool == false)
        {
            for (int i = 0; i < 1; i++)
            {
                NowSpawnPool = MonsterPoolSettingList[poolNum];
            }
        }
        canIMoveNextPool = true;
    }
}
