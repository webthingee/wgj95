using UnityEngine;

public class MovingHolder : MonoBehaviour
{
    public Vector2 randDir;
    public float moveSpeedMin = 2f;
    public float moveSpeedMax = 7f;

    private float moveSpeed;
    
    void Start()
    {
        randDir = Random.insideUnitCircle;
        moveSpeed = Random.Range(moveSpeedMin, moveSpeedMax);
    }

    void Update()
    {
        transform.position = (Vector2)transform.position + randDir * moveSpeed * Time.deltaTime;

        if (transform.position.x > 8f || transform.position.x < -7f)
        {
            randDir = Random.insideUnitCircle;
            moveSpeed = Random.Range(2f, 10f);
        }
        
        if (transform.position.y > 7f || transform.position.y < -7f)
        {
            randDir = Random.insideUnitCircle;
            moveSpeed = Random.Range(2f, 10f);
        }

        if (transform.position.x > 9f || transform.position.x < -8f || transform.position.y > 8f ||
            transform.position.y < -8f)
        {
            transform.position = Vector2.zero;
        }
    }
}