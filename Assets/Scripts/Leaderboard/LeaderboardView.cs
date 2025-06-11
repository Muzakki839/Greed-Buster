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

        foreach (LeaderboardData data in saveManager.leaderboard)
        {
            GameObject item = Instantiate(leaderboardListItemPrefab, transform);
            if (item.TryGetComponent<LeaderboardListItem>(out var listItem))
            {
                listItem.SetData(data);
                listItem.SetRank(saveManager.leaderboard.IndexOf(data) + 1);
            }
            else
            {
                Debug.LogError("LeaderboardListItem component not found on the prefab.");
            }
        }
    }
}
