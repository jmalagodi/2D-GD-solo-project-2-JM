using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public CanvasGroup gameOverGroup; // holds text + button
    public float fadeDuration = 1f;

    bool gameOverTriggered = false;

    void Start()
    {
        gameOverGroup.alpha = 0f;
        gameOverGroup.interactable = false;
        gameOverGroup.blocksRaycasts = false;
    }

    public void TriggerGameOver()
    {
        if (gameOverTriggered) return;

        gameOverTriggered = true;
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        // freeze gameplay
        Time.timeScale = 0f;

        gameOverGroup.gameObject.SetActive(true);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;

            gameOverGroup.alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);

            yield return null;
        }

        gameOverGroup.alpha = 1f;
        gameOverGroup.interactable = true;
        gameOverGroup.blocksRaycasts = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}