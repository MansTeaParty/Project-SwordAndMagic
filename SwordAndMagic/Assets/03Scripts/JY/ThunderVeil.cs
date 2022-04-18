using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderVeil : MonoBehaviour
{
    private float Timer = 0.0f;
    [SerializeField]
    private float VeilCooldown1;
    [SerializeField]
    private float VeilCooldown2;
    [SerializeField]
    private int Damage;

    private ItemInfoSet _itemInfoSet;

    private SkillManagement _skillManagement;
    private void Start()
    {
        _itemInfoSet = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
        _skillManagement = GameObject.Find("SkillManagement").GetComponent<SkillManagement>();        
    }
    private void Update()
    {
        VeilCooldown1 = _itemInfoSet.Items[14].CoolTime1;
        VeilCooldown2 = _itemInfoSet.Items[14].CoolTime2;
        Damage = _itemInfoSet.Items[14].Damage;
        this.transform.localScale = _itemInfoSet.Items[14].Size;

        if (_skillManagement.ThunderVeilActive == true)
        {            
            Timer += Time.deltaTime;
            if ((0.0f <= Timer) && (Timer < VeilCooldown1))
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
                this.GetComponent<BoxCollider2D>().enabled = true;
                //Debug.Log("ON");
            }
            else if ((VeilCooldown1 <= Timer) && (Timer < VeilCooldown2))
            {
                this.GetComponent<SpriteRenderer>().enabled = false;
                this.GetComponent<BoxCollider2D>().enabled = false;
                //Debug.Log("OFF");                
            }
            else
            {
                Timer = 0.0f;
            }
        }               
    }    
}
