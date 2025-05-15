using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int score = 0;
    [SerializeField] private int targetScore = 1000;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        string textFormat = $"<style=Title>Rp {FormatNumber(score)} </style> / {FormatNumber(targetScore)}";
        scoreText.text = textFormat;
    }

    private string FormatNumber(int num)
    {
        if (num >= 1_000_000) // Triliun
            return $"{num / 1_000_000.00:F2} T";
        if (num >= 1_000) // Miliar
            return $"{num / 1_000.0:F1} M";
        if (num >= 1) // Juta
            return $"{num / 1} Jt";

        return num.ToString();
    }

    public int GetScore() { return score; }

    public void AddScore(int point)
    {
        score += point;
        UpdateUI();
    }

    public IEnumerator TransferScore(int point, float duration = 0.2f)
    {
        int startScore = score;
        int endScore = score + point;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            score = Mathf.RoundToInt(Mathf.Lerp(startScore, endScore, normalizedTime));
            UpdateUI();
            yield return null;
        }

        score = endScore;
        UpdateUI();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
}
