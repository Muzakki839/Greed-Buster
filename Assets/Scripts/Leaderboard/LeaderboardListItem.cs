using TMPro;
using UnityEngine;

public class LeaderboardListItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI palyerNameText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI rankText;

    public void SetData(LeaderboardData data)
    {
        if (palyerNameText == null || scoreText == null || rankText == null)
        {
            Debug.LogError("One or more TextMeshProUGUI components are not assigned in LeaderboardListItem.");
            return;
        }

        palyerNameText.text = data.playerName;
        scoreText.text = NumberFormatter.FormatNumber(data.score);
    }

    public void SetRank(int rank)
    {
        if (rankText == null)
        {
            Debug.LogError("Rank TextMeshProUGUI is not assigned in LeaderboardListItem.");
            return;
        }

        rankText.text = "#" + rank.ToString();
    }
}
