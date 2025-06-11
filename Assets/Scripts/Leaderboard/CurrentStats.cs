using UnityEngine;

public class CurrentStats : MonoBehaviour
{
    public string currentName;
    public int currentScore;

    public void SetScore()
    {
        currentScore = ScoreManager.Instance.GetScore();
    }

    public void ResetCurrentData()
    {
        currentName = string.Empty;
        currentScore = 0;
    }
}