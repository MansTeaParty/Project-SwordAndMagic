using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronWall : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x * PlayerStatus.instance.projectileScale, transform.localScale.y * PlayerStatus.instance.projectileScale, 1);

        Destroy(gameObject, 0.2f);
    }


}
