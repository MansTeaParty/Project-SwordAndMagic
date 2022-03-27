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

        //poolNum = 0; -> 이거 땜시 리스트가 잘 안 돌아감.
        //이거 주석처리하니깐 리스트 잘됨 왜지?
        //아니 초기화 해줘도 ㅈㄹ, 안 해 줘도 ㅈㄹ
        //알다가도 모르겠네 젠장

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
            for (int i = 0; i < 10; i++)
            {
                PoolControl();
            }
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
