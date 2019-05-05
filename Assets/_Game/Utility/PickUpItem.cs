using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public int held = 0;
    public Transform holder;

    private void Start()
    {
        transform.position = holder.position;
    }

    void Update()
    {
    }
}