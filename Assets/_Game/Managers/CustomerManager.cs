using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public string customerName;
    public Sprite customerSprite;
    
    public float countdownInitValue = 1f;
    private float countdownCounter = 1f;

    public TextMeshProUGUI cName;
    public Image cSprite;
    public ImageSlider cCountdown;

    public float permScore;
    public float score;
    public HudText scoreDisplay;

    public Transform meters;

    public Customer[] customers;

    public float CountdownCounter
    {
        get { return countdownCounter; }
        set
        {
            countdownCounter = value;
            
            if (countdownCounter <= 0) countdownCounter = 0;
            if (countdownCounter >= 1) countdownCounter = 1;
                        
            cCountdown.ImageValue = countdownCounter;
            scoreDisplay.FloatValue = permScore + CalculateScore();

            if (countdownCounter == 0)
            {
                GameMaster.instance.LoseCondition();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        int cIndx = Random.Range(0, customers.Length);
        
        cName.text = customers[cIndx].customerName;
        cSprite.sprite = customers[cIndx].customerSprite;
        
        countdownCounter = countdownInitValue;
    }

    // Update is called once per frame
    void Update()
    {
        CountdownCounter -= Time.deltaTime * 0.01f;
    }

    public float CalculateScore()
    {
        float scoreAdd = 0;
        
        foreach (FlavorMeter flavorMeter in meters.GetComponentsInChildren<FlavorMeter>())
        {
            scoreAdd += flavorMeter.pointsEarned;
        }

        score = scoreAdd;
        return score;
    }

    public void CreatePermScore()
    {
        permScore += score;
    }
}

[System.Serializable]
public class Customer
{
    public string customerName;
    public Sprite customerSprite;
}
