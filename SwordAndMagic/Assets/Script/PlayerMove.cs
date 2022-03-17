using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform PlayerTr;
    public float PlayerSpeed;

    void Start()
    {
        PlayerTr = gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        PlayerTr.position += new Vector3(h, v, 0) * PlayerSpeed * Time.deltaTime;
        
    }
}
