using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimplePauseScript : MonoBehaviour
{
    public GameObject Maingame;
    public GameObject PauseUI;
    private bool paused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            paused = !paused;
        }

        if (paused)
        {
            Time.timeScale = 0;
            Maingame.SetActive(false);
            PauseUI.SetActive(true);
        }

        else
        {
            PauseUI.SetActive(false);
            Maingame.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Level Restarted");
    }

    public void MainMenu()
    {
        Debug.Log("MainMenu");
        Application.Quit();
        
    }
}
