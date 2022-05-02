using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾ �������(=testBox)�� �浹���� �ÿ� �߻��մϴ�. 
//�÷��̾�� ���ڰ� �浹�ϸ� ���ڴ� tag�� �÷��̾����� Ȯ���ϰ� tag�� �÷��̾��� RewardManager�� �ִ� ItemSet()�Լ��� SendMessage�� �մϴ�. 
//�浹�ÿ��� TimeScale���� 0���� �� �Ͻ����� ��ŵ�ϴ�. 
public class OpenRewardUI : MonoBehaviour
{
    private RewardManager _rewardManager;
    private ItemInfoSet _iteminfoset;

    private void Awake()
    {
        _rewardManager = GameObject.Find("RewardManager").GetComponent<RewardManager>();
        _iteminfoset = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            if (Time.timeScale == 0)
            {
                Debug.Log("Time.timeScale = 0");
            }
            _iteminfoset.SendMessage("ItemAbilitySet", SendMessageOptions.DontRequireReceiver);
            _rewardManager.SendMessage("ItemSet", SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
