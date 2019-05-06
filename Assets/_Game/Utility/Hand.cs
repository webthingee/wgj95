using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour
{
    public LayerMask pickupLayers;

    public int held;
    public GameObject objInHand;
    
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        transform.position = mousePos;

        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            objInHand = GetGameObj(mousePos);
            if (objInHand?.GetComponent<PickUpItem>() == null) return;

            if (held % 2 != 0)
            {
                objInHand.transform.parent = objInHand.GetComponent<PickUpItem>().holder;
                objInHand.transform.position = objInHand.GetComponent<PickUpItem>().holder.position;

                objInHand = null;

                held++;
            }
            else
            {
                objInHand.transform.parent = FindObjectOfType<Hand>().transform;
                objInHand.transform.position = FindObjectOfType<Hand>().transform.position;

                held++;

                if (held == 11) held = 1;
            }
        }
    }

    private GameObject GetGameObj(Vector3 start)
    {
        // Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, enemyAttackRange, 1 << LayerMask.NameToLayer("Player"));
        Collider2D hitCollider =Physics2D.OverlapCircle(start, 0.25f, pickupLayers);
        {
            return hitCollider?.gameObject;
        }
    }
}