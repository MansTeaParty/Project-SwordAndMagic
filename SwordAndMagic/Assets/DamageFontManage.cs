using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageFontManage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text text;
    [SerializeField]
    private string type;
    private Color alpha;

    private float upSpeed;
    void Start()
    {
        
        GetComponent<Canvas>().worldCamera = Camera.main;
        
        transform.localScale = transform.localScale / transform.parent.gameObject.transform.localScale.x;
        if (type == "Monster")
        {
            text.text = transform.parent.GetComponent<MonsterStat>().attackDamageForText.ToString();
            StartCoroutine(DamageFontAnimP());
        }
        if (type == "Player")
        {
            //text.text = transform.parent.GetComponent<PlayerCtrl>().attackDamageForText.ToString();
            StartCoroutine(DamageFontAnimP());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * upSpeed * Time.deltaTime;
    }
    IEnumerator DamageFontAnimM()
    {
        upSpeed = 7.0f;
        for (int i = 0; i < 10; i++)
        {
            if (i < 3)
            {
                transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
                upSpeed -= 1f;
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                text.color -= new Color(0, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.1f);
            }
        }
        Destroy(this.gameObject);
    }
    IEnumerator DamageFontAnimP()
    {
        upSpeed = 30.0f;
        for (int i = 0; i < 10; i++)
        {
            upSpeed -= 6f;
            if (i < 3)
            {
                transform.localScale += new Vector3(0.0005f, 0.0005f, 0.001f);
                
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                text.color -= new Color(0, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.1f);
            }
        }
        Destroy(this.gameObject);
    }
}
