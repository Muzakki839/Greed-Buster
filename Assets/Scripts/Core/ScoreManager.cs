using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private int score = 0;
    [SerializeField] private int targetScore = 10;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateUI()
    {
        string textFormat = $"<style=Title>{score} JT</style>/ {targetScore} M";
        scoreText.text = textFormat;
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
