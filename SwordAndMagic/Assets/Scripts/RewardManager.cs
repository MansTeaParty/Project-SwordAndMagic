using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    private ItemInfoSet _itemInfoSet;
    private TestMove _testMove;
    public GameObject RewardUI;

    public GameObject ItemButton1;
    public GameObject ItemButton2;
    public GameObject ItemButton3;  

    private int[] randomNumbers = new int[3];
    private bool isSame;

    private static RewardManager _instance;   
    public static RewardManager Instance
    {
        get
        {           
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(RewardManager)) as RewardManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }       
        else if (_instance != this)
        {
            Destroy(gameObject);
        }        
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {       
        _itemInfoSet = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
        _testMove = GameObject.Find("Player").GetComponent<TestMove>();
    } 

    void RandomNumberSet()
    {
        for (int i = 0; i < 3; i++)
        {
            while (true)
            {
                randomNumbers[i] = Random.Range(1, 11);
                isSame = false;
                for (int j = 0; j < i; j++)
                {
                    if (randomNumbers[j] == randomNumbers[i])
                    {
                        isSame = true;
                        break;
                    }
                }
                if (!isSame)
                {
                    break;
                }
            }
            //Debug.Log(randomNumbers[i]);
        }
    }

    void ItemSet()
    {
        RewardUI.SetActive(true);
        RandomNumberSet();
        for (int k = 0; k < randomNumbers.Length; k++)
        {
            for (int d = 0; d < _itemInfoSet.Items.Count; d++)
            {
                if (randomNumbers[k] == d + 1)
                {
                    if (k == 0)
                    {                       
                        GameObject.Find("Image1").GetComponent<Image>().sprite = _itemInfoSet.Items[d].ItemImage;
                        ItemButton1.GetComponentInChildren<Text>().text = _itemInfoSet.Items[d].ItemName + " " 
                            + "(Item Level: " + _itemInfoSet.Items[d].ItemLevel + ")"
                            + "\n" + "\t" +_itemInfoSet.Items[d].ItemAbility;
                        
                    }
                    else if (k == 1)
                    {
                        GameObject.Find("Image2").GetComponent<Image>().sprite = _itemInfoSet.Items[d].ItemImage;
                        ItemButton2.GetComponentInChildren<Text>().text = _itemInfoSet.Items[d].ItemName + " "
                            + "(Item Level: " + _itemInfoSet.Items[d].ItemLevel + ")"
                            + "\n" + "\t" + _itemInfoSet.Items[d].ItemAbility;
                    }
                    else
                    {
                        GameObject.Find("Image3").GetComponent<Image>().sprite = _itemInfoSet.Items[d].ItemImage;
                        ItemButton3.GetComponentInChildren<Text>().text = _itemInfoSet.Items[d].ItemName + " "
                            + "(Item Level: " + _itemInfoSet.Items[d].ItemLevel + ")"
                            + "\n" + "\t" + _itemInfoSet.Items[d].ItemAbility;
                    }
                }
            }
        }        
    }

    //public void ItemClick()
    //{
    //    for (int h = 0; h < _itemInfoSet.Items.Count; h++)
    //    {
    //        if (GameObject.Find("Image1").GetComponent<Image>().sprite == _itemInfoSet.Items[h].ItemImage)
    //        {
    //            _itemInfoSet.Items[h].ItemLevel += 1;
    //            if (h < 3)
    //            {
    //                testNumberA += _itemInfoSet.Items[h].PlusPoint;
    //            }
    //            else if ((h > 2) && (h < 6))
    //            {
    //                testNumberB += _itemInfoSet.Items[h].PlusPoint;
    //            }
    //            else if ((h > 5) && (h < 9))
    //            {
    //                testNumberC += _itemInfoSet.Items[h].PlusPoint;
    //            }
    //            else
    //            {
    //                testNumberA += _itemInfoSet.Items[h].PlusPoint;
    //                testNumberB += _itemInfoSet.Items[h].PlusPoint;
    //                testNumberC += _itemInfoSet.Items[h].PlusPoint;
    //            }
    //        }
    //    }
    //    Debug.Log(testNumberA);
    //    Debug.Log(testNumberB);
    //    Debug.Log(testNumberC);
    //}
    public void ItemClick1()
    {
        for (int h = 0; h < _itemInfoSet.Items.Count; h++)
        {
            if (GameObject.Find("Image1").GetComponent<Image>().sprite == _itemInfoSet.Items[h].ItemImage)
            {   
                switch(h)
                {
                    case 0:
                        PlayerStatus.instance.AD_Power += 10;
                        break;
                    case 1:
                        PlayerStatus.instance.Armor_Point += 1;
                        break;
                    case 2:
                        PlayerStatus.instance.AD_Speed += 12.0f;
                        break;
                    case 3:
                        PlayerStatus.instance.Max_HP += 10;
                        break;
                    case 4:
                        PlayerStatus.instance.AD_Speed += 24.0f;
                        break;
                    case 5:
                        PlayerStatus.instance.AD_Power += 20;                            
                        break;
                    case 6:
                        PlayerStatus.instance.Move_Speed += 1.0f;
                        break;
                    case 7:
                        PlayerStatus.instance.Armor_Point += 2;
                        break;
                    case 8:
                        PlayerStatus.instance.Max_HP += 20;
                        break;
                    case 9:
                        PlayerStatus.instance.Max_HP += 10;
                        PlayerStatus.instance.Armor_Point += 1;
                        PlayerStatus.instance.Move_Speed += 1.0f;
                        break;
                }
            }
        }       
        RewardUI.SetActive(false);
        Time.timeScale = 1;
        if(Time.timeScale == 1)
        {
            Debug.Log("Time.timeScale = 1");                
        }
        _testMove.moveSpeed = PlayerStatus.instance.Move_Speed;
    }
    public void ItemClick2()
    {
        for (int h = 0; h < _itemInfoSet.Items.Count; h++)
        {
            if (GameObject.Find("Image2").GetComponent<Image>().sprite == _itemInfoSet.Items[h].ItemImage)
            {
                _itemInfoSet.Items[h].ItemLevel += 1;
                switch (h)
                {
                    case 0:
                        PlayerStatus.instance.AD_Power += 10;
                        break;
                    case 1:
                        PlayerStatus.instance.Armor_Point += 1;
                        break;
                    case 2:
                        PlayerStatus.instance.AD_Speed += 12.0f;
                        break;
                    case 3:
                        PlayerStatus.instance.Max_HP += 10;
                        break;
                    case 4:
                        PlayerStatus.instance.AD_Speed += 24.0f;
                        break;
                    case 5:
                        PlayerStatus.instance.AD_Power += 20;
                        break;
                    case 6:
                        PlayerStatus.instance.Move_Speed += 1.0f;
                        break;
                    case 7:
                        PlayerStatus.instance.Armor_Point += 2;
                        break;
                    case 8:
                        PlayerStatus.instance.Max_HP += 20;
                        break;
                    case 9:
                        PlayerStatus.instance.Max_HP += 10;
                        PlayerStatus.instance.Armor_Point += 1;
                        PlayerStatus.instance.Move_Speed += 1.0f;
                        break;
                }
            }
        }       
        RewardUI.SetActive(false);
        Time.timeScale = 1;
        if (Time.timeScale == 1)
        {
            Debug.Log("Time.timeScale = 1");
        }
        _testMove.moveSpeed = PlayerStatus.instance.Move_Speed;
    }
    public void ItemClick3()
    {
        for (int h = 0; h < _itemInfoSet.Items.Count; h++)
        {
            if (GameObject.Find("Image3").GetComponent<Image>().sprite == _itemInfoSet.Items[h].ItemImage)
            {
                _itemInfoSet.Items[h].ItemLevel += 1;
                switch (h)
                {
                    case 0:
                        PlayerStatus.instance.AD_Power += 10;
                        break;
                    case 1:
                        PlayerStatus.instance.Armor_Point += 1;
                        break;
                    case 2:
                        PlayerStatus.instance.AD_Speed += 12.0f;
                        break;
                    case 3:
                        PlayerStatus.instance.Max_HP += 10;
                        break;
                    case 4:
                        PlayerStatus.instance.AD_Speed += 24.0f;
                        break;
                    case 5:
                        PlayerStatus.instance.AD_Power += 20;
                        break;
                    case 6:
                        PlayerStatus.instance.Move_Speed += 1.0f;
                        break;
                    case 7:
                        PlayerStatus.instance.Armor_Point += 2;
                        break;
                    case 8:
                        PlayerStatus.instance.Max_HP += 20;
                        break;
                    case 9:
                        PlayerStatus.instance.Max_HP += 10;
                        PlayerStatus.instance.Armor_Point += 1;
                        PlayerStatus.instance.Move_Speed += 1.0f;
                        break;
                }
            }
        }
        RewardUI.SetActive(false);
        Time.timeScale = 1;
        if (Time.timeScale == 1)
        {
            Debug.Log("Time.timeScale = 1");
        }
        _testMove.moveSpeed = PlayerStatus.instance.Move_Speed;
    }
}
