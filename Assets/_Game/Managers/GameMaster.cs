using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    
    public FlavorMeter salt;
    public Color saltColor;
    
    public FlavorMeter sugar;
    public Color sugarColor;
    
    public FlavorMeter pepper;
    public Color pepperColor;
    
    public FlavorMeter garlic;
    public Color garlicColor;

    private int fails;
    public int failsMax = 3;
    public Transform failsContainer;

    private int success;
    public int successGoal = 6;
    public Transform successContainer;
    
    [Header("Canvas")] 
    [SerializeField] public GameObject winCanvas;
    [SerializeField] public GameObject loseCanvas;
    
    private void Awake()
    {
        instance = this;

        Time.timeScale = 1f;
        
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        
        salt.flavorBkColor = saltColor;
        sugar.flavorBkColor = sugarColor;
        pepper.flavorBkColor = pepperColor;
        garlic.flavorBkColor = garlicColor;

        fails = 0;
        success = 0;
    }
    
    private void OnEnable()
    {
        ShakeAction.OnShake += ShakeActionOnOnShake;
    }

    private void ShakeActionOnOnShake(int ingNum)
    {
        if (ingNum == 1)
        {
            salt.FlavorValue++;
        }
        if (ingNum == 2)
        {
            sugar.FlavorValue++;
        }
        if (ingNum == 3)
        {
            pepper.FlavorValue++;
        }
        if (ingNum == 4)
        {
            garlic.FlavorValue++;
        }
    }

    public void CheckValues()
    {
        foreach (FlavorMeter flavorMeter in FindObjectsOfType<FlavorMeter>())
        {
            if (flavorMeter.IsInGoal()) continue;
            
            Debug.Log("Nope");

            fails++;

            for (int i = 0; i < fails; i++)
            {
                failsContainer.GetChild(i)?.gameObject.SetActive(true);
            }

            if (fails >= failsMax)
            {
                LoseCondition();
            }

            return;
        }

        Debug.Log("Yep");
        GetComponent<CustomerManager>().CreatePermScore();
        
        success++;

        for (int i = 0; i < fails; i++)
        {
            successContainer.GetChild(i)?.gameObject.SetActive(true);
        }

        if (success >= successGoal)
        {
            WinCondition();
            return;
        }
        
        NextOrder();
    }

    public void NextOrder()
    {
        foreach (FlavorMeter flavorMeter in FindObjectsOfType<FlavorMeter>())
        {
            flavorMeter.SetGoal(Random.Range(1,20) * 5);
            flavorMeter.FlavorValue = 0;
        }
    }

    public void WinCondition()
    {
        Debug.Log("Win!", gameObject);
        winCanvas.SetActive(true);
        Time.timeScale = 0.000000001f; 
    }
    
    public void LoseCondition()
    {
        Debug.Log("Game Over", gameObject);
        loseCanvas.SetActive(true);
        Time.timeScale = 0.000000001f;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    private void OnDisable()
    {
        ShakeAction.OnShake -= ShakeActionOnOnShake;
    }
}
