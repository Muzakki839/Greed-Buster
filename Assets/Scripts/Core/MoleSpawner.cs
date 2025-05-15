using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoleSpawner : Singleton<MoleSpawner>
{
    [SerializeField] private GameObject molePrefab;
    [SerializeField] private float spawnInterval = 0.1f;

    [SerializeField] private List<SpawnPoints> spawnPoints = new();

    private void Update()
    {
        // invoke spawnMole every spawnInterval
        if (Time.time % spawnInterval < Time.deltaTime && !IsAllSpawnPointsOccupied())
        {
            SpawnMole();
        }
    }

    private void SpawnMole()
    {
        // randomize unOccupied-spawn-slot
        var availableSlots = spawnPoints.Where(sp => !sp.isOccupied).ToList(); // Find all available spawn slots

        if (availableSlots.Count == 0) return; // Jika tidak ada slot kosong, keluar dari fungsi

        // spawn to the slot location
        var selectedSlot = availableSlots[Random.Range(0, availableSlots.Count)];
        Vector3 _spawnPoint = selectedSlot.spawnPointTransform.position - new Vector3(0, 1);
        GameObject _mole = Instantiate(molePrefab, _spawnPoint, Quaternion.identity);

        // send active slot to Ardity
        int _randomSpawnPointID = spawnPoints.IndexOf(selectedSlot); // Get the ID of the selected spawn point
        SerialMessageHandler.Instance.SendLedMessage(_randomSpawnPointID, true);

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
        return Random.Range(0, spawnPoints.Count);
    }

    public Vector3 GetSpawnPositionByID(int id)
    {
        return spawnPoints[id].spawnPointTransform.position;
    }

    public bool IsSpawnPointOccupied(int id)
    {
        return spawnPoints[id].isOccupied;
    }

    public bool IsAllSpawnPointsOccupied()
    {
        return spawnPoints.All(sp => sp.isOccupied);
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
