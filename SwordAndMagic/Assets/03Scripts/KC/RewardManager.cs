using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    private ItemInfoSet _itemInfoSet;
    private TestMove _testMove;
    public GameObject RewardUI;

    //보상 UI출력시 플레이어가 클릭하는 아이템 버튼입니다. 
    public GameObject ItemButton1;
    public GameObject ItemButton2;
    public GameObject ItemButton3;  

    //난수생성 함수에 필요한 요소, 난수생성으로 생성된 숫자들을 저장하는 배열과 이전 숫자와 같은 숫자인지 확인하는 bool변수 입니다.
    private int[] randomNumbers = new int[3];
    private bool isSame;

    //RewardManager 싱글톤패턴 적용
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
    //RewardManager 싱글톤패턴 적용
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

    //랜덤 아이템을 위한 난수 생성, 중복숫자 방지 기능 포함
    //이후 생성된 숫자들을 저장하는 배열randomNumbers[](길이 3)에 저장됩니다. 
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

    //기본적으로 보상 UI는 setactive()값이 false로 되어 있어 OpenRewardUI에서 sendMessage를 통해 UI를 활성화 해주고 이후 난수 생성 함수가 실행됩니다.
    // 앞에 난수 생성 함수에서 생성된 3개의 숫자(순서)에 맞는 이미지와 아이템 효과및 이름이 출력됩니다. 
    // h가 생성된 난수.
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

    //원하는 보상을 선택할 때 작동하는 함수. 
    //버튼에 상속되어 있는 이미지와 h(난수생성으로 결정된 숫자)번째의 ItemImage가 동일하면 해당 아이템 이미지의 아이템 효과가 캐릭터 능력치에 적용됩니다.
    //이후 다시 UI는 setActive()가 false가 되며 TimeScale은 1이 됩니다.
    //및의 ItemClick2()와 ItemClick3()는 모두 ItemClick1()과 같은 기능을 합니다.
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
