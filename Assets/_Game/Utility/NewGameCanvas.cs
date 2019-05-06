using UnityEngine;

public class NewGameCanvas : MonoBehaviour
{
    private void Update()
    {
        Time.timeScale = 0.000001f;
    }

    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
}