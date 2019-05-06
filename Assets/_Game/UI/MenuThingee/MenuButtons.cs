using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public bool startActive;
    private GameObject visibleCanvas;
    
    [Header("Canvas List")]
    public GameObject backgroundUICanvas;
    public GameObject buttonUICanvas;
    public GameObject titleUICanvas;
    public GameObject instructionsUICanvas;
    public GameObject audioUICanvas;

    private void OnEnable()
    {
        CloseAll();
        if (startActive) ToggleActive(titleUICanvas);
    }

    public void Update()
    {
        UsingThisUI();
    }
    
    private void UsingThisUI()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleActive(titleUICanvas);
        }
    }
    
    public void LoadGame(int gameSceneIndex) 
    {
        CloseAll();
        Time.timeScale = 1;
        
        SceneManager.LoadScene(gameSceneIndex, LoadSceneMode.Single);
    }

    public void ToggleActive(GameObject canvasToActivate)
    {
        if (visibleCanvas == canvasToActivate)
        {
            CloseAll();
        }
        else
        {
            CloseAll();
            Time.timeScale = 0.0000000001f;

            visibleCanvas = canvasToActivate;
            
            visibleCanvas.SetActive(true);
            buttonUICanvas.SetActive(true);
            backgroundUICanvas.SetActive(true);
        }        
    }
    
    public void CloseAll()
    {
        titleUICanvas.SetActive(false);
        instructionsUICanvas.SetActive(false);
        audioUICanvas.SetActive(false);        
        buttonUICanvas.SetActive(false);
        backgroundUICanvas.SetActive(false);
        
        Time.timeScale = 1;
        visibleCanvas = null;
    }
    
    public void QuitGame() 
    {
        #if UNITY_EDITOR // If we're in Unity Editor, stop play mode
                if (UnityEditor.EditorApplication.isPlaying == true)
                    UnityEditor.EditorApplication.isPlaying = false;
        #else // If we are in a built application, quit to desktop
                    Application.Quit();
        #endif
    }
}