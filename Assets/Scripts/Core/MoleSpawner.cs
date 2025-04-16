using UnityEngine;

public class MoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject molePrefab;
    [SerializeField] private float spawnInterval = 0.1f;
    [SerializeField] private int maxMoles = 3;

    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMole), 0f, spawnInterval);
    }

    private void SpawnMole()
    {
        Vector3 _spawnPoint = RandomizeSpawnPoint() - new Vector3(0, 1);
        Instantiate(molePrefab, _spawnPoint, Quaternion.identity);
    }

    private Vector3 RandomizeSpawnPoint()
    {
        int _randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[_randomIndex].position;
    }
}
