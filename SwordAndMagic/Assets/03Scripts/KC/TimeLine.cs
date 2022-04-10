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

    //게임 매니저가 일정 시간마다 호출하여 몬스터 풀이 생성됨.
    public void NextPool()
    {
        isNextPool = true;

        NowSpawnPool = MonsterPoolSettingList[poolNum];

        if (isNextPool)
        {
            PoolControl();      //몬스터풀 스폰  
            poolNum++;          //다음 몬스터 풀의 생성을 위해 몬스터풀 인덱스 +1 시킴.
                                //그럼 다음 호출 때 다음 몬스터 풀이 생성될 것임.
            isNextPool = false;
        }

    }

    void PoolControl()
    {
        GameObject MonsterPool_ABC = Instantiate(NowSpawnPool, this.transform.position, Quaternion.identity);
        MonsterPool_ABC.transform.SetParent(this.transform, false);
    }
}
