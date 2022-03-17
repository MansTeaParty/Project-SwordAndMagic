using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public Vector2 CameraVelocity;
    public GameObject Target;
    public float ReactTime;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        //SmoothDamp ���� �ڵ� �ڿ� ���� ReactTime �ð���ŭ �ʰ� �÷��̾ ����.
        float posX = Mathf.SmoothDamp(transform.position.x, Target.transform.position.x, ref CameraVelocity.x, ReactTime);
        float posY = Mathf.SmoothDamp(transform.position.y, Target.transform.position.y, ref CameraVelocity.y, ReactTime);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }

}
