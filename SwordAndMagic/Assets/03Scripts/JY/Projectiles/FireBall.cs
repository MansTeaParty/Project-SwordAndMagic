using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private int projectileSpeed = 3;
    [SerializeField]
    private int Penetration;
    [SerializeField]
    private int Damage;

    private ItemInfoSet _itemInfoSet;
    public MonsterCtrl _monsterCtrl;

    float angle;
    Vector2 target, mouse;
    private void Start()
    {
        _itemInfoSet = GameObject.Find("ItemList").GetComponent<ItemInfoSet>();
       

        target = transform.position;
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;

        Damage = _itemInfoSet.Items[11].Damage;
        Penetration = _itemInfoSet.Items[11].Penetration;
        this.transform.localScale = _itemInfoSet.Items[11].Size;
    } 
    private void FixedUpdate()
    {
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.Translate(Vector2.right * projectileSpeed * Time.deltaTime);       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Penetration -= 1;
            if (Penetration == 0)
            {
                Destroy(gameObject);
            }            
        }
    }
    void OnBecameInvisible()//화면밖으로 나갈때
    {
        Destroy(this.gameObject);//총알 파괴
    }
}
