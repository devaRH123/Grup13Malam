using UnityEngine;
using UnityEngine.SceneManagement;

public class FallTrigger : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    private void Update()
    {
        transform.position = new Vector2(Player.position.x, -9.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // when the player triggers enter restart the level/scene
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
