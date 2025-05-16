using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Calculate score and update UI
/// </summary>
public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int score = 0;
    [SerializeField] private int targetScore = 1000;

    [Header("Modify Score")]
    public int pointMultiplier = 1;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start() => ResetScore();

    public void UpdateUI()
    {
        string textFormat = $"<style=Title>Rp {FormatNumber(score)} </style> / {FormatNumber(targetScore)}";
        scoreText.text = textFormat;
    }

    private string FormatNumber(int num)
    {
        if (num >= 1_000_000) // Triliun
            return $"{num / 1_000_000.0:F1} T";
        if (num >= 1_000) // Miliar
            return $"{num / 1_000.0:F1} M";
        if (num >= 1) // Juta
            return $"{num / 1} Jt";

        return num.ToString();
    }

    public int GetScore() { return score; }

    public IEnumerator TransferScore(int point, float duration = 0.2f)
    {
        point = MultiplyPoint(point);

        int startScore = score;
        int endScore = score + point;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            score = Mathf.RoundToInt(Mathf.Lerp(startScore, endScore, normalizedTime));
            UpdateUI();
            yield return score = endScore;
        }
    }

    /// <summary>
    /// Multiply all point by pointMultiplier from MolePoolConfig. Default = 1
    /// </summary>
    public int MultiplyPoint(int point)
    {
        return point * pointMultiplier;
    }

    // --------- Target Score ---------
    public void SetTargetScore(int target)
    {
        targetScore = target;
        UpdateUI();
    }

    public int GetTargetScore() { return targetScore; }

    public bool IsTargetScoreReached() { return score >= targetScore; }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
}
