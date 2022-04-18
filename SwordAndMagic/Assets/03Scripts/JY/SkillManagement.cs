using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManagement : MonoBehaviour
{
    private ItemInfoSet _itemInfoSet;

    [SerializeField]
    private GameObject FirePos;
    [SerializeField]
    private GameObject FirePosPivot;
    [SerializeField]
    private GameObject PlusProjectile1;
    [SerializeField]
    private GameObject PlusProjectile2;

    [SerializeField]
    private Transform SpawnPos;
    // FirePos = 투사체의 생성위치, FirePosPivot = 투사체가 날라가는 방향 

    private bool AttackON = true;
    public float attackSpeed = 0.5f;

    public GameObject Arrow;
    public GameObject FireBall;
    public GameObject ShadowBolt;
    public GameObject ThunderStorm;
    public GameObject ThunderVeil;
    public GameObject IceShot;
    public GameObject Lavas;

    public bool ArrowActive = false;
    public bool FireBallActive = false;
    public bool ShadowBoltActive = false;
    public bool ThunderStormActive = false;
    public bool ThunderVeilActive = false;
    public bool IceShotActive = false;
    public bool LavasActive = false;

    private bool ArrowIsDelay;
    private float ArrowDelayTime = 1.0f;

    private bool IceShotIsDelay;
    private float IceShotDelayTime = 5.0f;

    private bool LavaIsDelay;
    private float LavaDelayTime; 

    public int FireBallCount = 0;

    private void Start()
    {
        _itemInfoSet = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
        LavaDelayTime = _itemInfoSet.Items[16].CoolTime2;
    }

    void Update()
    {
        if (ArrowActive == true)
        {
            if (!ArrowIsDelay)
            {
                ArrowIsDelay = true;
                StartCoroutine(ArrowAttack());
            }
        }
        IceShotAttack();
        LavaAttack();
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(NormalAttack());
            if (FireBallActive == true)
            {
                FireBallCount += 1;
            }
            if ((FireBallCount == 5) && (FireBallActive == true))
            {
                FireBallAttack();
            }
        }
        else if (Input.GetKey("space") && (ShadowBoltActive == true))
        {
            ShadowBoltAttack();
        }
    }

    IEnumerator NormalAttack()
    {
        if (AttackON)
        {
            AttackON = false;            
            yield return new WaitForSeconds(attackSpeed);
            AttackON = true;
        }
    }

    IEnumerator ArrowAttack()
    {
        Instantiate(Arrow, FirePos.transform.position, FirePosPivot.transform.rotation);
        if (_itemInfoSet.Items[10].ItemLevel >= 4)
        {
            Instantiate(Arrow, PlusProjectile1.transform.position, PlusProjectile1.transform.rotation);
            Debug.Log("test1");
            if (_itemInfoSet.Items[10].ItemLevel >= 8)
            {
                Instantiate(Arrow, PlusProjectile2.transform.position, PlusProjectile2.transform.rotation);
                Debug.Log("test2");
            }
        }
        yield return new WaitForSeconds(ArrowDelayTime);
        ArrowIsDelay = false;
    }

    void FireBallAttack()
    {
        Instantiate(FireBall, FirePos.transform.position, FirePosPivot.transform.rotation);
        FireBallCount = 0;
    }

    void ShadowBoltAttack()
    {
        Instantiate(ShadowBolt, FirePos.transform.position, FirePosPivot.transform.rotation);
    }

    void IceShotAttack()
    {
        if (IceShotActive == true)
        {
            if (!IceShotIsDelay)
            {
                IceShotIsDelay = true;
                StartCoroutine(_IceShot());
            }
        }
    }
    IEnumerator _IceShot()
    {
        Instantiate(IceShot, FirePos.transform.position, FirePosPivot.transform.rotation);
        yield return new WaitForSeconds(IceShotDelayTime);
        IceShotIsDelay = false;
    }

    void LavaAttack()
    {
        if (LavasActive == true)
        {
            if (!LavaIsDelay)
            {
                LavaIsDelay = true;
                StartCoroutine(_LavaAttack());
            }
        }        
    }
    IEnumerator _LavaAttack()
    {
        Instantiate(Lavas, SpawnPos.position, SpawnPos.rotation);
        yield return new WaitForSeconds(LavaDelayTime);
        LavaIsDelay = false;
    }
}


