using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    private ScoreManager scoreManager;
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreManager = ScoreManager.Instance;
        scoreText = GetComponent<TextMeshProUGUI>();

        scoreText.text = NumberFormatter.FormatNumber(scoreManager.GetScore());
    }
}
