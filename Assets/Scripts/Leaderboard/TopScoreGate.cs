using UnityEngine;
using UnityEngine.Events;

public class TopScoreGate : Singleton<TopScoreGate>
{
    [SerializeField] private UnityEvent onEnterTopScore;
    [SerializeField] private UnityEvent onNotEnterTopScore;

    public void CheckEnterTopScore(SaveManager saveManager)
    {
        var leaderboard = saveManager.GetTopScores(5);
        int currentScore = FindFirstObjectByType<CurrentStats>().currentScore = ScoreManager.Instance.GetScore();

        if (leaderboard.Count < 5 || currentScore > leaderboard[leaderboard.Count - 1].score)
        {
            onEnterTopScore?.Invoke();
        }
        else
        {
            onNotEnterTopScore?.Invoke();
        }
    }

    public bool IsEnterTopScore(SaveManager saveManager)
    {
        var leaderboard = saveManager.GetTopScores(5);
        int currentScore = FindFirstObjectByType<CurrentStats>().currentScore = ScoreManager.Instance.GetScore();

        if (leaderboard.Count < 5 || currentScore > leaderboard[leaderboard.Count - 1].score)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}