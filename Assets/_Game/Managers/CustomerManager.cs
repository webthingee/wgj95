using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{    
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

    private bool countdownTimerExpired;

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

            if (countdownCounter == 0 && !countdownTimerExpired)
            {
                countdownTimerExpired = true;
                Debug.Log("c");
                GameMaster.instance.CheckValues(true);
            }
        }
    }

    private void Start()
    {
        NewCustomer();
        NewCountdownTimer();
    }

    private void Update()
    {
        CountdownCounter -= Time.deltaTime * 0.01f;
    }
    
    public void NewCustomer()
    {
        int cIndx = Random.Range(0, customers.Length);
        
        cName.text = customers[cIndx].customerName;
        cSprite.sprite = customers[cIndx].customerSprite;        
    }
    
    public void NewCountdownTimer()
    {
        CountdownCounter = countdownInitValue;
        countdownTimerExpired = false;
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