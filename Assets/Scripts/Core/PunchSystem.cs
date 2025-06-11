using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PunchSystem : MonoBehaviour
{
    [SerializeField] private GameObject hammerPrefab;

    private GameObject spawnedHammer;
    private MoleSpawner moleSpawner;

    private AudioSource punchAudioSource;

    public void Start()
    {
        moleSpawner = MoleSpawner.Instance;
        punchAudioSource = GetComponent<AudioSource>();
    }

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
            DestroyImmediate(spawnedHammer);
        }

        spawnedHammer = Instantiate(
            hammerPrefab,
            moleSpawner.GetSpawnPositionByID(id),
            Quaternion.identity,
            moleSpawner.GetSpawnPointTransformByID(id)
        );

        punchAudioSource.Play();
        Destroy(spawnedHammer, 0.15f);
    }
}
