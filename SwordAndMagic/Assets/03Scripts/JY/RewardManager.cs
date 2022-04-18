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

    //���� UI��½� �÷��̾ Ŭ���ϴ� ������ ��ư�Դϴ�. 
    public GameObject ItemButton1;
    public GameObject ItemButton2;
    public GameObject ItemButton3;  

    //�������� �Լ��� �ʿ��� ���, ������������ ������ ���ڵ��� �����ϴ� �迭�� ���� ���ڿ� ���� �������� Ȯ���ϴ� bool���� �Դϴ�.
    private int[] randomNumbers = new int[3];
    private bool isSame;

    //RewardManager �̱������� ����
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
    //RewardManager �̱������� ����
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

    //���� �������� ���� ���� ����, �ߺ����� ���� ��� ����
    //���� ������ ���ڵ��� �����ϴ� �迭randomNumbers[](���� 3)�� ����˴ϴ�. 
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

    //�⺻������ ���� UI�� setactive()���� false�� �Ǿ� �־� OpenRewardUI���� sendMessage�� ���� UI�� Ȱ��ȭ ���ְ� ���� ���� ���� �Լ��� ����˴ϴ�.
    // �տ� ���� ���� �Լ����� ������ 3���� ����(����)�� �´� �̹����� ������ ȿ���� �̸��� ��µ˴ϴ�. 
    // h�� ������ ����.
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

    //���ϴ� ������ ������ �� �۵��ϴ� �Լ�. 
    //��ư�� ��ӵǾ� �ִ� �̹����� h(������������ ������ ����)��°�� ItemImage�� �����ϸ� �ش� ������ �̹����� ������ ȿ���� ĳ���� �ɷ�ġ�� ����˴ϴ�.
    //���� �ٽ� UI�� setActive()�� false�� �Ǹ� TimeScale�� 1�� �˴ϴ�.
    //���� ItemClick2()�� ItemClick3()�� ��� ItemClick1()�� ���� ����� �մϴ�.

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
                                    Debug.Log("����ü 1�� �߰�");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[10].Damage += 5;
                                    break;
                                case 6:
                                    Debug.Log("���� 1 �߰�");
                                    _itemInfoSet.Items[10].Penetration += 1;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[10].Size = new Vector3(0.8f, 0.8f, 0.8f);
                                    break;
                                case 8:
                                    Debug.Log("Ÿ��Ƚ�� ����");
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
                                    Debug.Log("����ü 1�� �߰�");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[11].Damage += 10;
                                    break;
                                case 6:
                                    _itemInfoSet.Items[11].Size = new Vector3(1.6f, 1.6f, 1.6f);
                                    break;
                                case 7:
                                    Debug.Log("����ü 1�� �߰�");
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
                                    Debug.Log("����ü 1�� �߰�");
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
                                    Debug.Log("��ų ��ȭ");
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
                                    Debug.Log("���� ����");
                                    break;
                                case 6:
                                    _itemInfoSet.Items[14].CoolTime1 = 7.0f;
                                    _itemInfoSet.Items[14].CoolTime2 = 10.0f;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[14].Size = new Vector3(0.6f, 0.6f, 0.75f);
                                    break;
                                case 8:
                                    Debug.Log("��ų ��ȭ");
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
                                    Debug.Log("3�ʰ� �̵�����");
                                    break;
                                case 3:
                                    _itemInfoSet.Items[15].Damage += 5;
                                    break;
                                case 4:
                                    Debug.Log("����ü 1�� �߰�");
                                    break;
                                case 5:
                                    Debug.Log("����ü 1�� �߰�");
                                    break;
                                case 6:
                                    _itemInfoSet.Items[15].CoolTime1 = 6.0f;
                                    Debug.Log("6�ʰ� �̵�����");
                                    break;
                                case 7:
                                    _itemInfoSet.Items[15].Damage += 5;
                                    break;
                                case 8:
                                    Debug.Log("��ų ��ȭ");
                                    break;
                            }
                        }
                        else
                        {
                            Debug.Log("1�� �߰��� �̵�����");
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
                                    Debug.Log("��ų ��ȭ");
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
                                    Debug.Log("����ü 1�� �߰�");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[10].Damage += 5;
                                    break;
                                case 6:
                                    Debug.Log("���� 1 �߰�");
                                    _itemInfoSet.Items[10].Penetration += 1;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[10].Size = new Vector3(0.8f, 0.8f, 0.8f);
                                    break;
                                case 8:
                                    Debug.Log("Ÿ��Ƚ�� ����");
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
                                    Debug.Log("����ü 1�� �߰�");
                                    break;
                                case 5:
                                    _itemInfoSet.Items[11].Damage += 10;
                                    break;
                                case 6:                                   
                                    _itemInfoSet.Items[11].Size = new Vector3(1.6f, 1.6f, 1.6f);
                                    break;
                                case 7:
                                    Debug.Log("����ü 1�� �߰�");
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
                                    Debug.Log("����ü 1�� �߰�");
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
                                    Debug.Log("��ų ��ȭ");
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
                                    Debug.Log("���� ����");
                                    break;
                                case 6:
                                    _itemInfoSet.Items[14].CoolTime1 = 7.0f;
                                    _itemInfoSet.Items[14].CoolTime2 = 10.0f;
                                    break;
                                case 7:
                                    _itemInfoSet.Items[14].Size = new Vector3(0.6f, 0.6f, 0.75f);
                                    break;
                                case 8:
                                    Debug.Log("��ų ��ȭ");
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
                                    Debug.Log("3�ʰ� �̵�����");
                                    break;
                                case 3:
                                    _itemInfoSet.Items[15].Damage += 5;
                                    break;
                                case 4:
                                    Debug.Log("����ü 1�� �߰�");
                                    break;
                                case 5:
                                    Debug.Log("����ü 1�� �߰�");
                                    break;
                                case 6:
                                    _itemInfoSet.Items[15].CoolTime1 = 6.0f;
                                    Debug.Log("6�ʰ� �̵�����");
                                    break;
                                case 7:
                                    _itemInfoSet.Items[15].Damage += 5;
                                    break;
                                case 8:
                                    Debug.Log("��ų ��ȭ");
                                    break;
                            }
                        }
                        else
                        {
                            Debug.Log("1�� �߰��� �̵�����");
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
                                    Debug.Log("��ų ��ȭ");
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
