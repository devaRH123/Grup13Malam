using UnityEngine.SceneManagement;
using UnityEngine;


public class S1_Quit : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
