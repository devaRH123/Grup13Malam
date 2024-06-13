using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        // SceneManager.LoadScene("ContinueScene");
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Stage 1 Prototype");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
