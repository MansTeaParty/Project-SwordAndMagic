using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
