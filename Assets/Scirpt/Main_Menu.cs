using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ContinueGame()
    {
        // Implement your continue logic here
        // For example, you can load a specific scene where the game left off
        SceneManager.LoadScene("ContinueScene");
    }

    public void StartNewGame()
    {
        // Load the first level or initial game scene
        SceneManager.LoadScene("level1");
    }

    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
