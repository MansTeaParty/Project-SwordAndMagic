using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어가 보상상자(=testBox)에 충돌했을 시에 발생합니다. 
//플레이어와 상자가 충돌하면 상자는 tag가 플레이어인지 확인하고 tag가 플레이어라면 RewardManager에 있는 ItemSet()함수에 SendMessage를 합니다. 
//충돌시에는 TimeScale값을 0으로 해 일시정지 시킵니다. 
public class OpenRewardUI : MonoBehaviour
{
    private RewardManager _rewardManager;   

    private void Awake()
    {
        _rewardManager = GameObject.Find("RewardManager").GetComponent<RewardManager>();        
    }

    void OnTriggerEnter2D(Collider2D coll)
    {       
        if(coll.gameObject.CompareTag("Player"))
        {            
            _rewardManager.SendMessage("ItemSet", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
            Time.timeScale = 0;
            if (Time.timeScale == 0)
            {
                Debug.Log("Time.timeScale = 0");
            }
        }
    }     
}
