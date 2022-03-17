using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject[] TargetSpawners;
    private GameObject Spawner;
    private bool isSpawnAble;

    private int StageNum;

    void Start()
    {
        Spawner = GameObject.FindGameObjectWithTag("Spawner");
        isSpawnAble = true;
        StageNum = 1;
    }

    void Update()
    {
        if(isSpawnAble)
        {
            for (int i = 0; i < 1; i++)
            {
                StartCoroutine(SpawnAble());
            }
        }
        
    }

    IEnumerator SpawnAble()
    {
        print("스폰 가능");
        print("스폰 메시지 전달");
        Spawner.SendMessage("SpawnCheck", StageNum);
        isSpawnAble = false;

        yield return new WaitForSeconds(10.0f);
        isSpawnAble = true;
    }
}
