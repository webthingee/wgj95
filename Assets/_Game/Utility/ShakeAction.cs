using System;
using System.Collections;
using UnityEngine;

public class ShakeAction : MonoBehaviour
{
    public bool startShake;
    public bool endShake;

    public float shakeStrength;
    private float shakeStrengthTime;

    public static event Action<int> OnShake;

    private void Start()
    {
        shakeStrengthTime = Time.time + shakeStrength * 100 * Time.deltaTime;
    }

    private void Update()
    {
        if (endShake)
        {
            if (Time.time < shakeStrengthTime)
            {
                var i = FindObjectOfType<Hand>().GetComponentInChildren<Ingredient>();
                if (i != null) OnShake(i.ingNum);
            }

            endShake = false;
            shakeStrengthTime = Time.time + shakeStrength * 100 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        startShake = false;
        endShake = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        startShake = true;
        endShake = false;    
    }
}
