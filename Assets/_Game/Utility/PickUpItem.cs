using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Transform holder;

    private void Start()
    {
        transform.position = holder.position;
    }

    void Update()
    {
        if (FindObjectOfType<Hand>().objInHand == gameObject)
        {
            transform.up = Vector3.zero - transform.position;
        }
        else
        {            
            transform.up = Vector3.up;
        }
    }
}