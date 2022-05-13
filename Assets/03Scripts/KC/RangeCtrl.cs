using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject RangeBack;
    [SerializeField]
    GameObject RangeIn;
    public GameObject Attack;
    Vector2 scalevec;
    public int damage;
    public float time;
    void Start()
    {
        //RangeBack = GetComponentsInChildren<GameObject>()[0];
        //RangeIn = GetComponentsInChildren<GameObject>()[1];
        scalevec = new Vector3(RangeBack.transform.localScale.x/ (time * 100), RangeBack.transform.localScale.y/ (time * 100), 0);
        StartCoroutine(RangeOn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator RangeOn()
    {
        if (RangeBack.transform.localScale.x > RangeIn.transform.localScale.x)
        {
            
            RangeIn.transform.localScale += (Vector3)scalevec;
            yield return new WaitForSeconds(0.01f);
            StartCoroutine(RangeOn());
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            float effectScale = RangeBack.transform.localScale.x;
            GameObject go = Instantiate(Attack, transform);
            go.transform.localScale = new Vector3(effectScale, effectScale, effectScale);
            GetComponent<CapsuleCollider2D>().enabled = true;
            Destroy(RangeIn);
            Destroy(RangeBack);

            yield return new WaitForSeconds(0.3f);

            GetComponent<CapsuleCollider2D>().enabled = false;

            yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            //other.GetComponent<PlayerCtrl>().Hit(damage);
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
