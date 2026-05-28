using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isGameOver;

    void OnEnable()
    {
        GameEventSystem.OnPlayerDied += HandlePlayerDied;
    }

    void OnDisable()
    {
        GameEventSystem.OnPlayerDied -= HandlePlayerDied;
    }

    void HandlePlayerDied()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        Time.timeScale = 0f;

        GameEventSystem.InvokeGameOver();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;

        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}