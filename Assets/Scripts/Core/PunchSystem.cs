using UnityEngine;

public class PunchSystem : MonoBehaviour
{
    public void PunchHoleID(int id)
    {
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
}
