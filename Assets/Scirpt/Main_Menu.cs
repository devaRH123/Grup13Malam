using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Stage1()
    {
        // Load the first level or initial game scene
        Debug.Log("tes tombol");
        SceneManager.LoadScene("Stage1");
    }

    public void Stage2()
    {
        // Load the first level or initial game scene
        Debug.Log("tes tombol");
        SceneManager.LoadScene("Stage2");
    }
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
