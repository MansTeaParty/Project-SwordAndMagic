using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������, ī�޶� ��Ʈ��

//ī�޶� �ε巴�� ����ٴ�

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
        transform.Translate(0, 0, -10); //ī�޶� ���� z������ �̵�
    }
}

//ī�޶� �״�� ����ٴ�
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