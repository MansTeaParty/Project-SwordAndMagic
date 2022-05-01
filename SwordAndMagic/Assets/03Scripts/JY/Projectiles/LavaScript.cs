using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    //private float Timer = 0.0f;
    [SerializeField]
    private float LavaCooldown1;
    [SerializeField]
    private int Damage;

    private ItemInfoSet _itemInfoSet;

    private SkillManagement _skillManagement;
    private void Start()
    {
        _itemInfoSet = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
        _skillManagement = GameObject.Find("SkillManagement").GetComponent<SkillManagement>();

        LavaCooldown1 = _itemInfoSet.Items[16].CoolTime1;
        Damage = _itemInfoSet.Items[16].Damage;
        this.transform.localScale = _itemInfoSet.Items[16].Size;

        StartCoroutine(LavaDestroy());
    }

    IEnumerator LavaDestroy()
    {
        yield return new WaitForSeconds(LavaCooldown1);
        Destroy(gameObject);
    }

    /*private void Update()
    {
        Timer += Time.deltaTime;
        /*if ((0.0f <= Timer) && (Timer < LavaCooldown1))
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            this.GetComponent<BoxCollider2D>().enabled = true;
            //Debug.Log("ON");
        }
        else if ((LavaCooldown1 <= Timer) && (Timer < LavaCooldown2))
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log("OFF");                
        }
        else
        {
            Timer = 0.0f;
        }

        if(Timer > LavaCooldown1)
        {
            Destroy(gameObject);
        }
    }*/
}
