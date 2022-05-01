using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class BuffManager : MonoBehaviour
{
    public PlayerStatus playerStatus;
    public GameObject Buff;

    // Start is called before the first frame update
    void Start()
    {
        playerStatus = PlayerStatus.instance;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject BuffToPlayer(CustomStatus buffStatus, float lifetime)
    {
        var instance = Instantiate(Buff,transform);
        CustomStatus.SumStatus(instance.GetComponent<BuffCtrl>().BuffValue, buffStatus);
        return instance;
    }
   // 

}