using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] Button retryButton;
    [SerializeField] GameManager gameManager;

    void Awake()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (retryButton != null)
            retryButton.onClick.AddListener(OnRetryButtonClicked);

        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnEnable()
    {
        GameEventSystem.OnGameOver += ShowGameOver;
    }

    void OnDisable()
    {
        GameEventSystem.OnGameOver -= ShowGameOver;

        if (retryButton != null)
            retryButton.onClick.RemoveListener(OnRetryButtonClicked);
    }

    void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    void OnRetryButtonClicked()
    {
        if (gameManager == null)
            return;

        gameManager.RestartGame();
    }
}