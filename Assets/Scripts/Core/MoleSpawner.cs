using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : Singleton<MoleSpawner>
{
    [SerializeField] private GameObject molePrefab;
    [SerializeField] private float spawnInterval = 0.1f;
    // [SerializeField] private int maxMoles = 3;

    [SerializeField] private List<SpawnPoints> spawnPoints = new List<SpawnPoints>();

    private void Start()
    {
        InvokeRepeating(nameof(SpawnMole), 0f, spawnInterval);
    }

    private void SpawnMole()
    {
        // randomize unoccupied-spawn-slot
        int _randomSpawnPointID = RandomizeSpawnPointID();
        while (IsSpawnPointOccupied(_randomSpawnPointID))
        {
            _randomSpawnPointID = RandomizeSpawnPointID();
        }

        // spawn to the slot location
        Vector3 _spawnPoint = GetSpawnPositionByID(_randomSpawnPointID) - new Vector3(0, 1);
        GameObject _mole = Instantiate(molePrefab, _spawnPoint, Quaternion.identity);

        // give spawnPointID info to the mole
        // (so it can set slot-occupied-status to false when it hides or hits)
        _mole.GetComponent<Mole>().spawnPointID = _randomSpawnPointID;
        // set slot-occupied-status to True
        SetSpawnPointOccupied(_randomSpawnPointID, true);
    }

    // ---------- Spawn Point Controller ----------
    private int RandomizeSpawnPointID()
    {
        return Random.Range(0, spawnPoints.Count - 1);
    }

    private Vector3 GetSpawnPositionByID(int id)
    {
        return spawnPoints[id].spawnPointTransform.position;
    }

    public bool IsSpawnPointOccupied(int id)
    {
        return spawnPoints[id].isOccupied;
    }
    public void SetSpawnPointOccupied(int id, bool isOccupied)
    {
        spawnPoints[id].isOccupied = isOccupied;
    }
}

[System.Serializable]
public class SpawnPoints
{
    public Transform spawnPointTransform;
    public bool isOccupied;
}
