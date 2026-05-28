using TMPro;
using UnityEngine;

public class ScoreTextUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    const string ScoreLabelColor = "#FF9100";

    void OnEnable()
    {
        GameEventSystem.OnScoreChanged += UpdateScoreText;
    }

    void OnDisable()
    {
        GameEventSystem.OnScoreChanged -= UpdateScoreText;
    }

    void Awake()
    {
        if (scoreText == null)
            scoreText = GetComponent<TextMeshProUGUI>();

        if (scoreText != null)
            scoreText.richText = true;
    }

    void Start()
    {
        UpdateScoreText(0);
    }

    void UpdateScoreText(int currentScore)
    {
        if (scoreText == null)
            return;

        scoreText.text = $"<color={ScoreLabelColor}>SCORE:</color> {currentScore}";
    }
}
