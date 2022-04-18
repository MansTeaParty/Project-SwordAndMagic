using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPButtonCtrl : MonoBehaviour
{
    public PlayerCtrl _playerctrl;

    void Update()
    {        
        if(_playerctrl.PlayerSP <= 0)
        {
            for(int i=1; i<7; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }           
        }
        else if(_playerctrl.PlayerSP >= 1)
        {
            for (int i = 1; i < 7; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }       
    }
}
