using UnityEngine.SceneManagement;
using UnityEngine;


public class To_S2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Stage 2");
        }
    }

}
