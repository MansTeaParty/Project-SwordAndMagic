using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//������, ���̽�ƽ ��Ʈ���� ���� �ڵ�
//https://youtu.be/_WOYSZsDNHQ ����
/*
��ġ���� panel�� �ְ� �е�� ��ƽ�� ��ġ�Ͽ� ���
 
 
*/

public class JoyStick : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler, IDragHandler
{
    public Transform player;    //ĳ����
    Vector3 move;
    public float speed;

    public RectTransform pad;   //�е�� ��ƽ�� ��ġ����
    public RectTransform stick;

    public float[] temp = new float[3];
    public void OnDrag(PointerEventData eventData)
    {
        stick.position = eventData.position;
        //��ƽ �̵��ݰ� ����, �е��� ������-��ƽ�� ������ ��ŭ�� �̵�����
        stick.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)pad.position, (pad.rect.width * 0.5f)-(stick.rect.width*0.5f));

        move = new Vector2(stick.localPosition.x,stick.localPosition.y).normalized;

        temp[0] = move.x;
        temp[1] = move.y;
        temp[2] = move.z;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pad.position = eventData.position;
        pad.gameObject.SetActive(true);
        StartCoroutine("Movement");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.localPosition = Vector2.zero;
        pad.gameObject.SetActive(false);
        StopCoroutine("Movement");
        move = Vector2.zero;
    }

    IEnumerator Movement()
    {
        while(true)
        {
            player.Translate(move * speed * Time.deltaTime);
            //if (move != Vector2.zero)
            //    player.rotation = Quaternion.Slerp(player.rotation);
            yield return null; 
        }
    }

}
