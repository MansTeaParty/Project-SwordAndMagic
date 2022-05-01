using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//전세윤, 조이스틱 컨트롤을 위한 코드
//https://youtu.be/_WOYSZsDNHQ 참고
/*
터치범위 panel에 넣고 패드와 스틱을 배치하여 사용
 
 
*/

public class JoyStick : MonoBehaviour, IPointerDownHandler,
    IPointerUpHandler, IDragHandler
{
    public Transform player;    //캐릭터
    Vector3 move;
    public float speed;

    public RectTransform pad;   //패드와 스틱의 위치정보
    public RectTransform stick;

    public float[] temp = new float[3];
    public void OnDrag(PointerEventData eventData)
    {
        stick.position = eventData.position;
        //스틱 이동반경 제한, 패드의 반지름-스틱의 반지름 만큼만 이동가능
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
