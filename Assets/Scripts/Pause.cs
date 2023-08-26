using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    private bool paused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                UnPauseGame();
            else
                PauseGame();
                
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        paused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        paused = false;
    }
}
