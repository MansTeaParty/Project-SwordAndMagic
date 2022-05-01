using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPButtonCtrl : MonoBehaviour
{
    public PlayerStatus _playerstatus;

    void Update()
    {        
        if(_playerstatus.playerSP <= 0)
        {
            for(int i=1; i<7; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }           
        }
        else if(_playerstatus.playerSP >= 1)
        {
            for (int i = 1; i < 7; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }       
    }
}
