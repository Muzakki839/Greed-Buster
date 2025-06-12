using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private string nextScene = "TapCard";

    private string SaveFilePath => Path.Combine(Application.dataPath, "SaveData", "leaderboard.json");
    public List<LeaderboardData> leaderboard = new();

    private void Start()
    {
        LoadData();
    }

    // Create save file json if doesn't exist, and ensure folder exists
    public void LoadData()
    {
        if (File.Exists(SaveFilePath))
        {
            string json = File.ReadAllText(SaveFilePath);
            leaderboard = JsonUtility.FromJson<LeaderboardList>(json)?.list ?? new List<LeaderboardData>();
        }
        else
        {
            leaderboard = new List<LeaderboardData>();
            SaveData();
        }
    }

    private void SaveData()
    {
        // Make sure save folder exists
        string folder = Path.GetDirectoryName(SaveFilePath);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        LeaderboardList wrapper = new() { list = leaderboard };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(SaveFilePath, json);
    }

    // Add new player data to save file 
    public void AddPlayerScore(TextMeshProUGUI nameText)
    {
        string currentName = nameText.text.Trim();
        int currentScore = FindFirstObjectByType<CurrentStats>().currentScore;

        // duplicate / empty name not allowed
        // if (leaderboard.Exists(x => x.playerName == currentName) || string.IsNullOrEmpty(currentName))
        //     return;

        // duplicate allowed, empty name not allowed
        if (string.IsNullOrEmpty(currentName))
            return;

        leaderboard.Add(new LeaderboardData { playerName = currentName, score = currentScore });
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));
        SaveData();
        SceneManager.LoadScene(nextScene);
        return;
    }

    // Load Top N score from save file
    public List<LeaderboardData> GetTopScores(int top = 5)
    {
        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));
        return leaderboard.GetRange(0, Mathf.Min(top, leaderboard.Count));
    }

    public int GetHighestScore()
    {
        if (leaderboard.Count == 0)
            return 0;

        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));
        return leaderboard[0].score;
    }
}

[Serializable]
public class LeaderboardData
{
    public string playerName;
    public int score;
}

[Serializable]
public class LeaderboardList
{
    public List<LeaderboardData> list = new();
}