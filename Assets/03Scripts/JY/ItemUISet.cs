using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//, IPointerEnterHandler, IPointerExitHandler

public class ItemUISet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject ItemUI;

    private ItemInfoSet _itemInfoSet;

    private void Start()
    {
        _itemInfoSet = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
    }
  
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject pointerEnter = eventData.pointerEnter;


        ItemUI.transform.GetChild(0).GetComponent<Image>().sprite = gameObject.transform.GetChild(1).GetComponent<Image>().sprite;
        ItemUITextSet();

        ItemUI.SetActive(true);      
    }
    
    void ItemUITextSet()
    {
        for (int i = 0; i < _itemInfoSet.Items.Count; i++)
        {
            if (ItemUI.transform.GetChild(0).GetComponent<Image>().sprite == _itemInfoSet.Items[i].ItemImage)
            {
                ItemUI.transform.GetChild(1).GetComponent<Text>().text = _itemInfoSet.Items[i].ItemAbility;
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {      
        ItemUI.SetActive(false);
    }
}
