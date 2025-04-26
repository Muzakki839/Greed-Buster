using UnityEngine;

public class PunchSystem : MonoBehaviour
{
    [SerializeField] private GameObject hammerPrefab;

    private GameObject spawnedHammer;

    public void PunchHoleID(int id)
    {
        PunchEffect(id);

        // Debug.Log($"Punched hole ID: {id}");
        if (MoleSpawner.Instance.IsSpawnPointOccupied(id))
        {
            GameObject mole = MoleSpawner.Instance.GetSpawnedMoleByID(id);
            if (mole != null)
            {
                mole.GetComponent<Mole>().Hit();
            }
        }
        else
        {
            Debug.LogWarning($"Miss at hole: {id}");
        }
    }

    private void PunchEffect(int id)
    {
        if (spawnedHammer != null)
        {
            Destroy(spawnedHammer);
        }

        spawnedHammer = Instantiate(hammerPrefab, MoleSpawner.Instance.GetSpawnPositionByID(id), Quaternion.identity);
        Destroy(spawnedHammer, 0.1f);
    }
}
