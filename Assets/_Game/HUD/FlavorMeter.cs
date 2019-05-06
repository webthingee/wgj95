using UnityEngine;
using UnityEngine.UI;

public class FlavorMeter : MonoBehaviour
{
    public Transform goalMeter;
    public int goal;
    public int pointsEarned;
    
    [SerializeField] private int flavorID;
    public Slider flavorSlider;
    public int flavorValue;
    public Color flavorBkColor = Color.black;
    public Color flavorGoalColor = Color.green;

    public int FlavorValue
    {
        get { return flavorValue; }
        set
        {
            flavorValue = value;

            if (flavorValue <= 0) flavorValue = 0;
            if (flavorValue >= 100) flavorValue = 100;
            
            flavorSlider.value = flavorValue;
        }
    }

    private void Awake()
    {
        flavorSlider = GetComponentInChildren<Slider>();
    }

    public void SetGoal(int newGoal)
    {
        goal = newGoal;
    }

    private void Update()
    {
        ResetColors();
        goalMeter.transform.GetChild(goal / 10).GetComponent<Image>().color = flavorGoalColor;

        if (Random.value > .995f)
        {
            FlavorValue--;
        }
        
        pointsEarned = 100 - Mathf.Abs(goal - flavorValue);
    }

    public bool IsInGoal()
    {
        return Mathf.Abs(goal - flavorValue) <= 5f;
    }

    public void ResetColors()
    {
        foreach (Image img in goalMeter.GetComponentsInChildren<Image>())
        {
            img.color = flavorBkColor;
        }
    }
}