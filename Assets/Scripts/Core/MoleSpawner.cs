using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Script that responsible to set hole spawn and spawnning mole
/// </summary>
[RequireComponent(typeof(MolePoolConfig))]
public class MoleSpawner : Singleton<MoleSpawner>
{
    // [SerializeField] private GameObject molePrefab;
    public float spawnInterval = 0.1f;
    [SerializeField] private int numMolesSpawnning = 1;
    [SerializeField] private List<SpawnPoints> spawnPoints = new();

    [HideInInspector] public MolePoolConfig molePoolConfig;
    private bool isSpawning = false;

    private void Start()
    {
        molePoolConfig = GetComponent<MolePoolConfig>();
    }

    private void Update()
    {
        // invoke spawnMole every spawnInterval
        if (Time.time % spawnInterval < Time.deltaTime && !IsAllSpawnPointsOccupied() && isSpawning)
        {
            SpawnMole(molePoolConfig.GetRandomMolePrefab());
        }
    }

    public void SpawnMole(GameObject mole)
    {
        // randomize unOccupied-spawn-slot
        var availableSlots = spawnPoints.Where(sp => !sp.isOccupied).ToList(); // Find all available spawn slots
        if (availableSlots.Count == 0) return; // If no available slots, return from method
        var selectedSlot = availableSlots[Random.Range(0, availableSlots.Count)];

        // spawn to the slot location
        Vector3 _spawnPoint = selectedSlot.spawnPointTransform.position - new Vector3(0, 1);
        GameObject _mole = Instantiate(mole, _spawnPoint, Quaternion.identity, selectedSlot.spawnPointTransform);

        // send active slot to Ardity
        int _randomSpawnPointID = spawnPoints.IndexOf(selectedSlot); // Get the ID of the selected spawn point
        SerialMessageHandler.Instance?.SendLedMessage(_randomSpawnPointID, true);

        // modify spawned mole
        molePoolConfig.MultiplyMoleWaitDuration(_mole.GetComponent<Mole>());

        // give spawnPointID info to the mole
        // (so it can set slot-occupied-status to false when it hides or hits)
        _mole.GetComponent<Mole>().spawnPointID = _randomSpawnPointID;

        // add spawned mole to the list
        SetSpawnPointMole(_randomSpawnPointID, _mole);
        // set slot-occupied-status to True
        SetSpawnPointOccupied(_randomSpawnPointID, true);
    }

    public void StartSpawn()
    {
        isSpawning = true;
    }

    public void StopSpawn()
    {
        isSpawning = false;
        // all mole in spawnPoint hide
        foreach (var sp in spawnPoints)
        {
            if (sp.spawnedMole != null)
            {
                sp.spawnedMole.GetComponent<Mole>().Hide();
            }
        }
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
