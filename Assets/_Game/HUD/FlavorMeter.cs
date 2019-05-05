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

    void Start()
    {
        SetGoal(Random.Range(1,20) * 5);
    }

    public void SetGoal(int newGoal)
    {
        goal = newGoal;
    }

    // Update is called once per frame
    void Update()
    {
        ResetColors();
        goalMeter.transform.GetChild(goal / 10).GetComponent<Image>().color = Color.green;

        if (Random.value > .99f)
        {
            FlavorValue--;
        }
        
        pointsEarned = 100 - (Mathf.Abs(goal - flavorValue));
    }

    public bool IsInGoal()
    {
        return Mathf.Abs(goal - flavorValue) <= 9f;
    }

    public void ResetColors()
    {
        foreach (Image img in goalMeter.GetComponentsInChildren<Image>())
        {
            img.color = flavorBkColor;
        }
    }
}