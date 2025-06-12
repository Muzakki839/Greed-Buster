using System;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameObject leaderboardListItemPrefab;

    private void Start()
    {
        if (saveManager == null)
        {
            Debug.LogError("SaveManager is not assigned in LeaderboardView.");
            return;
        }

        PopulateLeaderboard();
    }

    private void PopulateLeaderboard()
    {
        if (leaderboardListItemPrefab == null)
        {
            Debug.LogError("Leaderboard List Item Prefab is not assigned in LeaderboardView.");
            return;
        }

        saveManager.LoadData();

        int count = Mathf.Min(5, saveManager.leaderboard.Count);
        for (int i = 0; i < count; i++)
        {
            LeaderboardData data = saveManager.leaderboard[i];
            GameObject item = Instantiate(leaderboardListItemPrefab, transform);
            if (item.TryGetComponent<LeaderboardListItem>(out var listItem))
            {
                listItem.SetData(data);
                listItem.SetRank(i + 1);
            }
            else
            {
                Debug.LogError("LeaderboardListItem component not found on the prefab.");
            }
        }
    }
}
