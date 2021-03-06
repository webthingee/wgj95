﻿using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;

    public bool testMode;

    public int shakeIncreaseValue = 2;
    
    public FlavorMeter salt;
    public Color saltColor;
    
    public FlavorMeter saffron;
    public Color saffronColor;
    
    public FlavorMeter pepper;
    public Color pepperColor;
    
    public FlavorMeter mint;
    public Color mintColor;

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
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("not the instance " + name);
            Destroy(gameObject);
        }
        
        Time.timeScale = 1f;
                
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        
        salt.flavorBkColor = saltColor;
        saffron.flavorBkColor = saffronColor;
        pepper.flavorBkColor = pepperColor;
        mint.flavorBkColor = mintColor;

        fails = 0;
        success = 0;
    }
    
    private void OnEnable()
    {
        ShakeAction.OnShake += ShakeActionOnOnShake;
    }

    private void Start()
    {
        NextOrder();
        Invoke("SetMusic", 1f);
    }

    private void SetMusic()
    {
        FindObjectOfType<MainMusic>().ChangeSongSelection(0.15f);
    }

    private void ShakeActionOnOnShake(int ingNum)
    {
        if (ingNum == 1)
        {
            salt.FlavorValue += shakeIncreaseValue;
            FindObjectOfType<MainMusic>().ChangeInGameSpiceLevel("salt", salt.FlavorValue/100f);
        }
        if (ingNum == 2)
        {
            saffron.FlavorValue += shakeIncreaseValue;
            FindObjectOfType<MainMusic>().ChangeInGameSpiceLevel("saffron", saffron.FlavorValue/100f);
        }
        if (ingNum == 3)
        {
            pepper.FlavorValue += shakeIncreaseValue;
            FindObjectOfType<MainMusic>().ChangeInGameSpiceLevel("pepper", pepper.FlavorValue/100f);
        }
        if (ingNum == 4)
        {
            mint.FlavorValue += shakeIncreaseValue;
            FindObjectOfType<MainMusic>().ChangeInGameSpiceLevel("mint", mint.FlavorValue/100f);
        }
    }

    public void CheckValues(bool isTimeExpired = false)
    {
        Debug.Log("check");
        foreach (FlavorMeter flavorMeter in FindObjectsOfType<FlavorMeter>())
        {
            if (flavorMeter.IsInGoal()) continue; // if non fail, we exit on the last check.
            
            Debug.Log("Nope");
            RuntimeManager.PlayOneShot("event:/SFX/Nope", Vector3.zero);

            fails++;

            for (int i = 0; i < fails; i++)
            {
                failsContainer.GetChild(i)?.gameObject.SetActive(true);
            }

            if (fails >= failsMax)
            {
                LoseCondition();
                return;
            }
            
            if (isTimeExpired) NextOrder();
            return;
        }

        Debug.Log("Yep");
        RuntimeManager.PlayOneShot("event:/SFX/Yep", Vector3.zero);
        GetComponent<CustomerManager>().CreatePermScore();
        
        success++;

        for (int i = 0; i < success; i++)
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
            flavorMeter.SetGoal(Random.Range(25,95));
            flavorMeter.FlavorValue = 0;
            
            if (testMode)
            {
                flavorMeter.SetGoal(50);
                flavorMeter.FlavorValue = 50;
            }

            GetComponent<CustomerManager>().NewCustomer();            
            GetComponent<CustomerManager>().NewCountdownTimer();            
        }
    }

    public void WinCondition()
    {
        Debug.Log("Win!", gameObject);
        FindObjectOfType<MainMusic>().ChangeSongSelection(0.22f);
        FindObjectOfType<MainMusic>().ChangeEndGame(0.33f);
        winCanvas.SetActive(true);
        Time.timeScale = 0.000000001f; 
    }
    
    public void LoseCondition()
    {
        Debug.Log("Game Over", gameObject);
        FindObjectOfType<MainMusic>().ChangeSongSelection(0.22f);
        FindObjectOfType<MainMusic>().ChangeEndGame(0.66f);
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