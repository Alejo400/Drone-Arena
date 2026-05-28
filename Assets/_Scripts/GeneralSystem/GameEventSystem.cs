using System;

//General intermediary between observers and subjects
public static class GameEventSystem
{
    public static event Action<float, float> OnPlayerHealth;
    public static event Action OnPlayerDied;
    public static event Action OnGameOver;

    public static event Action<int> OnEnemyKilled;
    public static event Action<int> OnScoreChanged;

    public static void InvokePlayerHealthChanged(float currentHealth, float maxHealth)
    {
        OnPlayerHealth?.Invoke(currentHealth,maxHealth);
    }
    
    public static void InvokeOnPlayerDied()
    {
        OnPlayerDied?.Invoke();
    }

    public static void InvokeGameOver()
    {
        OnGameOver?.Invoke();
    }
    public static void InvokeEnemyKilled(int value)
    {
        OnEnemyKilled?.Invoke(value);
    }
    public static void InvokeScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }
}