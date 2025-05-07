using System.Collections.Generic;
using UnityEngine;

public class MoleSpawner : Singleton<MoleSpawner>
{
    [SerializeField] private GameObject molePrefab;
    [SerializeField] private float spawnInterval = 0.1f;
    // [SerializeField] private int maxMoles = 3;

    [SerializeField] private List<SpawnPoints> spawnPoints = new();

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

        // send active slot to Ardity
        SerialMessageHandler.Instance.serialController.SendSerialMessage("l" + _randomSpawnPointID + "_on\n");

        // give spawnPointID info to the mole
        // (so it can set slot-occupied-status to false when it hides or hits)
        _mole.GetComponent<Mole>().spawnPointID = _randomSpawnPointID;
        // add spawned mole to the list
        SetSpawnPointMole(_randomSpawnPointID, _mole);
        // set slot-occupied-status to True
        SetSpawnPointOccupied(_randomSpawnPointID, true);
    }

    // ---------- Spawn Point Controller ----------
    private int RandomizeSpawnPointID()
    {
        return Random.Range(0, spawnPoints.Count - 1);
    }

    public Vector3 GetSpawnPositionByID(int id)
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
    public void SetSpawnPointMole(int id, GameObject mole)
    {
        spawnPoints[id].spawnedMole = mole;
    }
    public GameObject GetSpawnedMoleByID(int id)
    {
        return spawnPoints[id].spawnedMole;
    }
}

[System.Serializable]
public class SpawnPoints
{
    public Transform spawnPointTransform;
    public bool isOccupied;
    public GameObject spawnedMole;
}
