using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceCtrl : MonoBehaviour
{
    public Vector2 TraceVelocity;
    public GameObject Target;
    public float ReactTime;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //SmoothDamp 쓰면 코드 뒤에 써진 ReactTime 시간만큼 늦게 플레이어를 따라감.
        float posX = Mathf.SmoothDamp(transform.position.x, Target.transform.position.x, ref TraceVelocity.x, ReactTime);
        float posY = Mathf.SmoothDamp(transform.position.y, Target.transform.position.y, ref TraceVelocity.y, ReactTime);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

}
