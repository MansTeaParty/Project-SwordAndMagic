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

    private int monsterid;

    private float upSpeed;
    void Start()
    {
        
        GetComponent<Canvas>().worldCamera = Camera.main;

        transform.localScale = transform.localScale / transform.parent.gameObject.transform.localScale.x;

        monsterid = GetComponentInParent<MonsterStat>().MonsterId;

        if (type == "Monster")
        {
            text.text = transform.parent.GetComponent<MonsterStat>().attackDamageForText.ToString();
            //text.text = "<color=red>" + textValue + "</color>";
            text.color = new Color(255, 0, 0);

            StartCoroutine(DamageFontAnimM());
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
        upSpeed = 0.0f;
        for (int i = 0; i < 10; i++)
        {
            upSpeed += 0.8f;// 폰트가 출력되면 서서히 올라가면서
            if (i < 3)
            {
                //폰트의 스케일 값을 증가 -> 폰트가 점점 커지는 효과
                float scale = 0.0f;
                if (monsterid == 1) //박쥐
                {
                    scale = 0.0008f;
                }
                if (monsterid == 2) //골렘
                {
                    scale = 0.0007f;
                }
                if (monsterid == 3) //Spiked 슬라임
                {
                    scale = 0.0007f;
                }
                if (monsterid == 4) // Tentacle 슬라임
                {
                    scale = 0.0007f;
                }

                transform.localScale += new Vector3(scale, scale, 0.001f);

                yield return new WaitForSeconds(0.1f);
            }
            else // i >= 3
            {
                //점차 희미해지도록 컬러 R 값을 점점 뺌
                text.color -= new Color(30, 0, 0, 0.1f);
                yield return new WaitForSeconds(0.08f);
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
