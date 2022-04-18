using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    private ItemInfoSet _itemInfoSet;
    private PlayerCtrl _playerCtrl;
    private SkillManagement _skillManagement;
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
        _playerCtrl = GameObject.Find("Player").GetComponent<PlayerCtrl>();
        _skillManagement = GameObject.Find("SkillManagement").GetComponent<SkillManagement>();
    } 

    //랜덤 아이템을 위한 난수 생성, 중복숫자 방지 기능 포함
    //이후 생성된 숫자들을 저장하는 배열randomNumbers[](길이 3)에 저장됩니다. 
    void RandomNumberSet()
    {
        for (int i = 0; i < 3; i++)
        {
            while (true)
            {
                randomNumbers[i] = Random.Range(1, 16);
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
            Debug.Log(randomNumbers[i]);
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
                            + "(Level: " + _itemInfoSet.Items[d].ItemLevel + ")"
                            + "\n" + "\t" +_itemInfoSet.Items[d].ItemAbility;
                        
                    }
                    else if (k == 1)
                    {
                        GameObject.Find("Image2").GetComponent<Image>().sprite = _itemInfoSet.Items[d].ItemImage;
                        ItemButton2.GetComponentInChildren<Text>().text = _itemInfoSet.Items[d].ItemName + " "
                            + "(Level: " + _itemInfoSet.Items[d].ItemLevel + ")"
                            + "\n" + "\t" + _itemInfoSet.Items[d].ItemAbility;
                    }
                    else
                    {
                        GameObject.Find("Image3").GetComponent<Image>().sprite = _itemInfoSet.Items[d].ItemImage;
                        ItemButton3.GetComponentInChildren<Text>().text = _itemInfoSet.Items[d].ItemName + " "
                            + "(Level: " + _itemInfoSet.Items[d].ItemLevel + ")"
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

    public void testClick()
    {

        for (int h = 0; h < _itemInfoSet.Items.Count; h++)
        {
            if (GameObject.Find("Image1").GetComponent<Image>().sprite == _itemInfoSet.Items[h].ItemImage)
            {
                _itemInfoSet.Items[h].ItemLevel += 1;

                switch (h)
                {
                    case 0:
                        PlayerStatus.instance.attackDamage += 10;
                        break;
                    case 1:
                        PlayerStatus.instance.armorPoint += 1;
                        break;
                    case 2:
                        PlayerStatus.instance.attackSpeed += 12.0f;
                        break;
                    case 3:
                        PlayerStatus.instance.maxHP += 10;
                        break;
                    case 4:
                        PlayerStatus.instance.attackSpeed += 24.0f;
                        break;
                    case 5:
                        PlayerStatus.instance.attackDamage += 20;
                        break;
                    case 6:
                        PlayerStatus.instance.movementSpeed += 1.0f;
                        break;
                    case 7:
                        PlayerStatus.instance.armorPoint += 2;
                        break;
                    case 8:
                        PlayerStatus.instance.maxHP += 20;
                        break;
                    case 9:
                        PlayerStatus.instance.maxHP += 10;
                        PlayerStatus.instance.armorPoint += 1;
                        PlayerStatus.instance.movementSpeed += 1.0f;
                        break;
                    case 10:
                        _skillManagement.ArrowActive = true;
                        if (_itemInfoSet.Items[10].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[10].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[10].Damage = 5;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[10].Damage += 5;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[10].Size = new Vector3(0.625f, 0.625f, 0.625f);
                                    break;
                                case 4:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[10].Damage += 5;
                                    break;
                                case 6:
                                    Debug.Log("관통 1 추가");
                                    _itemInfoSet.Items[10].Penetration += 1;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[10].Size = new Vector3(0.8f, 0.8f, 0.8f);
                                    break;
                                case 8:
                                    Debug.Log("타격횟수 증가");
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[10].Damage += 2;
                        }
                        break;
                    case 11:
                        _skillManagement.FireBallActive = true;
                        _skillManagement.FireBallCount = 0;
                        if (_itemInfoSet.Items[11].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[11].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[11].Damage = 15;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[11].Damage += 10;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[11].Size = new Vector3(1.25f, 1.25f, 1.25f);
                                    break;
                                case 4:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[11].Damage += 10;
                                    break;
                                case 6:
                                    _itemInfoSet.Items[11].Size = new Vector3(1.6f, 1.6f, 1.6f);
                                    break;
                                case 7:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 8:
                                    _itemInfoSet.Items[11].Penetration += 10;
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[11].Damage += 5;
                        }
                        break;
                    case 12:
                        _skillManagement.ShadowBoltActive = true;
                        if (_itemInfoSet.Items[12].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[12].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[12].Damage = 10;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[12].Damage += 10;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[12].Size = new Vector3(1.25f, 1.25f, 1.25f);
                                    break;
                                case 4:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[12].Damage += 10;
                                    break;
                                case 6:
                                    _itemInfoSet.Items[12].Penetration += 1;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[12].Size = new Vector3(1.6f, 1.6f, 1.6f);
                                    break;
                                case 8:
                                    Debug.Log("스킬 강화");
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[12].Damage += 5;
                        }
                        break;
                    case 13:
                        _skillManagement.ThunderStormActive = true;
                        break;
                    case 14:
                        _skillManagement.ThunderVeilActive = true;
                        if (_itemInfoSet.Items[14].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[14].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[14].Damage = 10;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[14].Damage += 5;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[14].CoolTime1 = 5.0f;
                                    _itemInfoSet.Items[14].CoolTime2 = 8.0f;
                                    break;
                                case 4:
                                    _itemInfoSet.Items[14].Damage += 5;
                                    break;
                                case 5:
                                    PlayerStatus.instance.armorPoint += 1;
                                    Debug.Log("방어력 증가");
                                    break;
                                case 6:
                                    _itemInfoSet.Items[14].CoolTime1 = 7.0f;
                                    _itemInfoSet.Items[14].CoolTime2 = 10.0f;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[14].Size = new Vector3(0.6f, 0.6f, 0.75f);
                                    break;
                                case 8:
                                    Debug.Log("스킬 강화");
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[14].Damage += 3;
                        }
                        break;
                    case 15:
                        _skillManagement.IceShotActive = true;
                        if (_itemInfoSet.Items[15].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[15].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[15].Damage = 15;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[15].CoolTime1 = 3.0f;
                                    Debug.Log("3초간 이동방해");
                                    break;
                                case 3:
                                    _itemInfoSet.Items[15].Damage += 5;
                                    break;
                                case 4:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 5:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 6:
                                    _itemInfoSet.Items[15].CoolTime1 = 6.0f;
                                    Debug.Log("6초간 이동방해");
                                    break;
                                case 7:
                                    _itemInfoSet.Items[15].Damage += 5;
                                    break;
                                case 8:
                                    Debug.Log("스킬 강화");
                                    break;
                            }
                        }
                        else
                        {
                            Debug.Log("1초 추가로 이동방해");
                        }
                        break;
                    case 16:
                        _skillManagement.LavasActive = true;
                        if (_itemInfoSet.Items[16].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[16].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[16].Damage = 3;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[16].Damage += 2;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[16].Size = new Vector3(1.875f, 1.6f, 1.25f);
                                    break;
                                case 4:
                                    _itemInfoSet.Items[16].CoolTime1 = 7.0f;
                                    break;
                                case 5:
                                    _itemInfoSet.Items[16].Damage += 2;
                                    break;
                                case 6:
                                    _itemInfoSet.Items[16].Size = new Vector3(2.4f, 2.0f, 1.6f);
                                    break;
                                case 7:
                                    _itemInfoSet.Items[16].CoolTime1 = 9.0f;
                                    break;
                                case 8:
                                    Debug.Log("스킬 강화");
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[16].Damage += 1;
                        }
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
        _playerCtrl.moveSpeed = PlayerStatus.instance.movementSpeed;
    }


    public void ItemClick1()
    {
        for (int h = 0; h < _itemInfoSet.Items.Count; h++)
        {
            if (GameObject.Find("Image1").GetComponent<Image>().sprite == _itemInfoSet.Items[h].ItemImage)
            {
                _itemInfoSet.Items[h].ItemLevel += 1;
                switch (h)
                {
                    case 0:
                        PlayerStatus.instance.attackDamage += 10;
                        break;
                    case 1:
                        PlayerStatus.instance.armorPoint += 1;
                        break;
                    case 2:
                        PlayerStatus.instance.attackSpeed += 12.0f;
                        break;
                    case 3:
                        PlayerStatus.instance.maxHP += 10;
                        break;
                    case 4:
                        PlayerStatus.instance.attackSpeed += 24.0f;
                        break;
                    case 5:
                        PlayerStatus.instance.attackDamage += 20;                            
                        break;
                    case 6:
                        PlayerStatus.instance.movementSpeed += 1.0f;
                        break;
                    case 7:
                        PlayerStatus.instance.armorPoint += 2;
                        break;
                    case 8:
                        PlayerStatus.instance.maxHP += 20;
                        break;
                    case 9:
                        PlayerStatus.instance.maxHP += 10;
                        PlayerStatus.instance.armorPoint += 1;
                        PlayerStatus.instance.movementSpeed += 1.0f;
                        break;
                    case 10:
                        _skillManagement.ArrowActive = true;
                        if (_itemInfoSet.Items[10].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[10].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[10].Damage = 5;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[10].Damage += 5;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[10].Size = new Vector3(0.625f, 0.625f, 0.625f);
                                    break;
                                case 4:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[10].Damage += 5;
                                    break;
                                case 6:
                                    Debug.Log("관통 1 추가");
                                    _itemInfoSet.Items[10].Penetration += 1;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[10].Size = new Vector3(0.8f, 0.8f, 0.8f);
                                    break;
                                case 8:
                                    Debug.Log("타격횟수 증가");
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[10].Damage += 2;
                        }
                        break;
                    case 11:
                        _skillManagement.FireBallActive = true;
                        _skillManagement.FireBallCount = 0;
                        if (_itemInfoSet.Items[11].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[11].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[11].Damage = 15;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[11].Damage += 10;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[11].Size = new Vector3(1.25f, 1.25f, 1.25f);
                                    break;
                                case 4:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[11].Damage += 10;
                                    break;
                                case 6:                                   
                                    _itemInfoSet.Items[11].Size = new Vector3(1.6f, 1.6f, 1.6f);
                                    break;
                                case 7:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 8:
                                    _itemInfoSet.Items[11].Penetration += 10; 
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[11].Damage += 5;
                        }
                        break;
                    case 12:
                        _skillManagement.ShadowBoltActive = true;
                        if (_itemInfoSet.Items[12].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[12].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[12].Damage = 10;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[12].Damage += 10;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[12].Size = new Vector3(1.25f, 1.25f, 1.25f);
                                    break;
                                case 4:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[12].Damage += 10;
                                    break;
                                case 6:
                                    _itemInfoSet.Items[12].Penetration += 1;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[12].Size = new Vector3(1.6f, 1.6f, 1.6f);
                                    break;
                                case 8:
                                    Debug.Log("스킬 강화");
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[12].Damage += 5;
                        }
                        break;
                    case 13:
                        _skillManagement.ThunderStormActive = true;
                        break;
                    case 14:
                        _skillManagement.ThunderVeilActive = true;
                        if (_itemInfoSet.Items[14].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[14].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[14].Damage = 10;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[14].Damage += 5;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[14].CoolTime1 = 5.0f;
                                    _itemInfoSet.Items[14].CoolTime2 = 8.0f;
                                    break;
                                case 4:
                                    _itemInfoSet.Items[14].Damage += 5;
                                    break;
                                case 5:
                                    PlayerStatus.instance.armorPoint += 1;
                                    Debug.Log("방어력 증가");
                                    break;
                                case 6:
                                    _itemInfoSet.Items[14].CoolTime1 = 7.0f;
                                    _itemInfoSet.Items[14].CoolTime2 = 10.0f;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[14].Size = new Vector3(0.6f, 0.6f, 0.75f);
                                    break;
                                case 8:
                                    Debug.Log("스킬 강화");
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[14].Damage += 3;
                        }
                        break;
                    case 15:
                        _skillManagement.IceShotActive = true;
                        if (_itemInfoSet.Items[15].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[15].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[15].Damage = 15;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[15].CoolTime1 = 3.0f;
                                    Debug.Log("3초간 이동방해");
                                    break;
                                case 3:
                                    _itemInfoSet.Items[15].Damage += 5;
                                    break;
                                case 4:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 5:
                                    Debug.Log("투사체 1개 추가");
                                    break;
                                case 6:
                                    _itemInfoSet.Items[15].CoolTime1 = 6.0f;
                                    Debug.Log("6초간 이동방해");
                                    break;
                                case 7:
                                    _itemInfoSet.Items[15].Damage += 5;
                                    break;
                                case 8:
                                    Debug.Log("스킬 강화");
                                    break;
                            }
                        }
                        else
                        {
                            Debug.Log("1초 추가로 이동방해");
                        }
                        break;
                    case 16:
                        _skillManagement.LavasActive = true;
                        if (_itemInfoSet.Items[16].ItemLevel < 9)
                        {
                            switch (_itemInfoSet.Items[16].ItemLevel)
                            {
                                case 1:
                                    _itemInfoSet.Items[16].Damage = 3;
                                    break;
                                case 2:
                                    _itemInfoSet.Items[16].Damage += 2;
                                    break;
                                case 3:
                                    _itemInfoSet.Items[16].Size = new Vector3(1.875f, 1.6f, 1.25f);
                                    break;
                                case 4:
                                    _itemInfoSet.Items[16].CoolTime1 = 7.0f;                                   
                                    break;
                                case 5:
                                    _itemInfoSet.Items[16].Damage += 2;
                                    break;
                                case 6:
                                    _itemInfoSet.Items[16].Size = new Vector3(2.4f, 2.0f, 1.6f);
                                    break;
                                case 7:
                                    _itemInfoSet.Items[16].CoolTime1 = 9.0f;                                   
                                    break;
                                case 8:
                                    Debug.Log("스킬 강화");
                                    break;
                            }
                        }
                        else
                        {
                            _itemInfoSet.Items[16].Damage += 1;
                        }
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
        _playerCtrl.moveSpeed = PlayerStatus.instance.movementSpeed;
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
                        PlayerStatus.instance.attackDamage += 10;
                        break;
                    case 1:
                        PlayerStatus.instance.armorPoint += 1;
                        break;
                    case 2:
                        PlayerStatus.instance.attackSpeed += 12.0f;
                        break;
                    case 3:
                        PlayerStatus.instance.maxHP += 10;
                        break;
                    case 4:
                        PlayerStatus.instance.attackSpeed += 24.0f;
                        break;
                    case 5:
                        PlayerStatus.instance.attackDamage += 20;
                        break;
                    case 6:
                        PlayerStatus.instance.movementSpeed += 1.0f;
                        break;
                    case 7:
                        PlayerStatus.instance.armorPoint += 2;
                        break;
                    case 8:
                        PlayerStatus.instance.maxHP += 20;
                        break;
                    case 9:
                        PlayerStatus.instance.maxHP += 10;
                        PlayerStatus.instance.armorPoint += 1;
                        PlayerStatus.instance.movementSpeed += 1.0f;
                        break;
                    case 10:
                        _skillManagement.ArrowActive = true;
                        break;
                    case 11:
                        _skillManagement.FireBallActive = true;
                        _skillManagement.FireBallCount = 0;
                        break;
                    case 12:
                        _skillManagement.ShadowBoltActive = true;
                        break;
                    case 13:
                        _skillManagement.ThunderStormActive = true;
                        break;
                    case 14:
                        _skillManagement.ThunderVeilActive = true;
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
        _playerCtrl.moveSpeed = PlayerStatus.instance.movementSpeed;
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
                        PlayerStatus.instance.attackDamage += 10;
                        break;
                    case 1:
                        PlayerStatus.instance.armorPoint += 1;
                        break;
                    case 2:
                        PlayerStatus.instance.attackSpeed += 12.0f;
                        break;
                    case 3:
                        PlayerStatus.instance.maxHP += 10;
                        break;
                    case 4:
                        PlayerStatus.instance.attackSpeed += 24.0f;
                        break;
                    case 5:
                        PlayerStatus.instance.attackDamage += 20;
                        break;
                    case 6:
                        PlayerStatus.instance.movementSpeed += 1.0f;
                        break;
                    case 7:
                        PlayerStatus.instance.armorPoint += 2;
                        break;
                    case 8:
                        PlayerStatus.instance.maxHP += 20;
                        break;
                    case 9:
                        PlayerStatus.instance.maxHP += 10;
                        PlayerStatus.instance.armorPoint += 1;
                        PlayerStatus.instance.movementSpeed += 1.0f;
                        break;
                    case 10:
                        _skillManagement.ArrowActive = true;
                        break;
                    case 11:
                        _skillManagement.FireBallActive = true;
                        _skillManagement.FireBallCount = 0;
                        break;
                    case 12:
                        _skillManagement.ShadowBoltActive = true;
                        break;
                    case 13:
                        _skillManagement.ThunderStormActive = true;
                        break;
                    case 14:
                        _skillManagement.ThunderVeilActive = true;
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
        _playerCtrl.moveSpeed = PlayerStatus.instance.movementSpeed;
    }
}
