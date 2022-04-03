using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MousePointer : MonoBehaviour
{
    [SerializeField]
    Texture2D CrossHair;

    private Vector3 mPosition;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(CrossHair, Vector2.zero, CursorMode.ForceSoftware);
        
    }

    // Update is called once per frame
    void Update()
    {
        //mPosition = Input.mousePosition;
        //this.transform.position = Camera.main.ScreenToWorldPoint(mPosition);
    }
}
