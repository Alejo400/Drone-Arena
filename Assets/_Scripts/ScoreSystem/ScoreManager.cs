using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int currentScore;

    void OnEnable()
    {
        GameEventSystem.OnEnemyKilled += AddScore;
    }

    void OnDisable()
    {
        GameEventSystem.OnEnemyKilled -= AddScore;
    }

    void Start()
    {
        currentScore = 0;
        GameEventSystem.InvokeScoreChanged(currentScore);
    }

    void AddScore(int points)
    {
        currentScore += points;
        GameEventSystem.InvokeScoreChanged(currentScore);
    }
}