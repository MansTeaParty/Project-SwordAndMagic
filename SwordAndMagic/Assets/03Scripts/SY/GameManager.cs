using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotoMain()
    {
        SceneManager.LoadScene("SY_WorkScene"); //임시로 씬 재로드
    }

    public void playerDie()
    {
        GameOverUI.SetActive(true);
    }
}
