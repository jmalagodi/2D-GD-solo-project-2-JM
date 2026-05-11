using UnityEngine;

public class KillBox : MonoBehaviour
{
    public GameOverManager gameOverManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverManager.TriggerGameOver();
        }
    }
}