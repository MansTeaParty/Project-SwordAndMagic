using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private int projectileSpeed = 4;
    [SerializeField]
    private int Penetration;
    [SerializeField]
    private int Damage;

    float angle;
    Vector2 target, _target;
    public List<GameObject> FoundMonsters;
    public GameObject Monster;
    public float shortDis;

    private SkillManagement _skillManagement;
    private ItemInfoSet _itemInfoSet;  

    private void Start()
    {        
        _skillManagement = GameObject.Find("SkillManagement").GetComponent<SkillManagement>();
        _itemInfoSet = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
        Damage = _itemInfoSet.Items[10].Damage;
        Penetration = _itemInfoSet.Items[10].Penetration;     
        this.transform.localScale = _itemInfoSet.Items[10].Size;

        FoundMonsters = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        shortDis = Vector2.Distance(gameObject.transform.position, FoundMonsters[0].transform.position); // 첫번째를 기준으로 잡아주기 
        Monster = FoundMonsters[0]; // 첫번째를 먼저         
        foreach (GameObject found in FoundMonsters)
        {
            float Distance = Vector3.Distance(gameObject.transform.position, found.transform.position);

            if (Distance < shortDis) // 위에서 잡은 기준으로 거리 재기
            {
                shortDis = Distance;
                Monster = found;
            }
        }
        target = transform.position;
        _target = Monster.transform.position;
        angle = Mathf.Atan2(_target.y - target.y, _target.x - target.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void FixedUpdate()
    {        
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject _monster = collision.gameObject;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Penetration -= 1;
            if (Penetration == 0)
            {
                Destroy(gameObject);
            }
            if (_skillManagement.ThunderStormActive == true)
            {
                Instantiate(_skillManagement.ThunderStorm, _monster.transform.position, _monster.transform.rotation);
            }
        }
    }
    void OnBecameInvisible()//화면밖으로 나갈때
    {
        Destroy(this.gameObject);//총알 파괴
    }
}
