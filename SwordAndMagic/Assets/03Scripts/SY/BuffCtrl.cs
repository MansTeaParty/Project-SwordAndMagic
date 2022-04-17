using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCtrl : MonoBehaviour
{
    public CustomStatus BuffValue;

    [SerializeField] PlayerStatus targetPlayerStatus;
    [SerializeField] BuffManager ParentBuffManager;

    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        BuffValue = GetComponent<CustomStatus>();
        ParentBuffManager = GetComponentInParent<BuffManager>();
        targetPlayerStatus = ParentBuffManager.playerStatus;
        StartCoroutine(BuffLife(lifetime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator BuffLife(float lifetime)
    {
        CustomStatus.SumStatus(targetPlayerStatus.buffStatus, BuffValue);
        yield return new WaitForSeconds(lifetime);

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        CustomStatus.SubStatus(targetPlayerStatus.buffStatus, BuffValue);
        //Debug.Log("¹öÇÁ³¡");
    }

}
