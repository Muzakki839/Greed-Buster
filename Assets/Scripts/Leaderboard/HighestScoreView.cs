using System;
using TMPro;
using UnityEngine;

public class HighestScoreView : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private TextMeshProUGUI highestScoreText;

    private void Start()
    {
        if (saveManager == null)
        {
            Debug.LogError("SaveManager is not assigned in HighestScoreView.");
            return;
        }

        if (highestScoreText == null)
        {
            Debug.LogError("Highest Score TextMeshProUGUI is not assigned in HighestScoreView.");
            return;
        }

        DisplayHighestScore();
    }

    private void DisplayHighestScore()
    {
        if (saveManager.leaderboard == null || saveManager.leaderboard.Count == 0)
        {
            highestScoreText.text = "0";
            return;
        }

        // Find the highest score
        int highestScore = int.MinValue;
        foreach (var data in saveManager.leaderboard)
        {
            if (data.score > highestScore)
            {
                highestScore = data.score;
            }
        }

        // Display the highest score
        highestScoreText.text = NumberFormatter.FormatNumber(highestScore);
    }
}
