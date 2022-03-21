using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//전세윤, 카메라 컨트롤

//카메라가 부드럽게 따라다님

public class CameraCtrl : MonoBehaviour
{
    public GameObject Target;
    Transform tr;
    void Start()
    {
        tr = Target.transform;
    }
    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, tr.position, 2f * Time.deltaTime);
        transform.Translate(0, 0, -10); //카메라를 원래 z축으로 이동
    }
}

//카메라가 그대로 따라다님
/*
using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour
{
    public GameObject Target;
    Transform tr;
    void Start()
    {
        tr = Target.transform;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(tr.position.x, tr.position.y, transform.position.z);
    }
}*/