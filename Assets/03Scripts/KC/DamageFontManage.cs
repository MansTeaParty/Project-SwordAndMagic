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

        monsterid = GetComponentInParent<MonsterCtrl>().MonsterId;

        if (type == "Enemy")
        {
            text.text = transform.parent.GetComponent<MonsterCtrl>().attackDamageForText.ToString();
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
            upSpeed += 0.8f; // ��Ʈ�� ��µǸ� ������ �ö󰡸鼭
                             // ��Ʈ�� ������ ���� ���� -> ��Ʈ�� ���� Ŀ���� ȿ��
            if (i < 3)
            {
                float scale = 0f;
                if (monsterid == 1) //����
                {
                    scale = 0.0008f;
                }
                if (monsterid == 2) //��
                {
                    scale = 0.0007f;
                }
                if (monsterid == 3) //Spiked ������
                {
                    scale = 0.0007f;
                }
                if (monsterid == 4) // Tentacle ������
                {
                    scale = 0.0007f;
                }
                if (monsterid == 5) // �̹�
                {
                    scale = 0.001f;
                }

                transform.localScale += new Vector3(scale, scale, 0.001f);

                yield return new WaitForSeconds(0.1f);
            }
            else // i >= 3
            {
                //���� ����������� �÷� R ���� ���� ��
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