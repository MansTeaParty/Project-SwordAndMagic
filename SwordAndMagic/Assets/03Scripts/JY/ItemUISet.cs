using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//, IPointerEnterHandler, IPointerExitHandler

public class ItemUISet : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    public GameObject ItemUI;

    private ItemInfoSet _itemInfoSet;

    private void Start()
    {
        _itemInfoSet = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
    }
  
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject pointerEnter = eventData.pointerEnter;       

        if ((pointerEnter == Button1) || (pointerEnter == Button1.transform.GetChild(0).gameObject) 
            || (pointerEnter == Button1.transform.GetChild(1).gameObject))
        {
            ItemUI.transform.GetChild(0).GetComponent<Image>().sprite = Button1.transform.GetChild(1).GetComponent<Image>().sprite;
            ItemUITextSet();
            Debug.Log("Button1");
        }      

        else if((pointerEnter == Button2) || (pointerEnter == Button2.transform.GetChild(0).gameObject)
            || (pointerEnter == Button2.transform.GetChild(1).gameObject))
        {
            ItemUI.transform.GetChild(0).GetComponent<Image>().sprite = Button2.transform.GetChild(1).GetComponent<Image>().sprite;
            ItemUITextSet();
            Debug.Log("Button2");
        }

        else if ((pointerEnter == Button3) || (pointerEnter == Button3.transform.GetChild(0).gameObject)
            || (pointerEnter == Button3.transform.GetChild(1).gameObject))
        {
            ItemUI.transform.GetChild(0).GetComponent<Image>().sprite = Button3.transform.GetChild(1).GetComponent<Image>().sprite;
            ItemUITextSet();
            Debug.Log("Button3");
        }      
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
