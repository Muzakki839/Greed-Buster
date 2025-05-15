using System.Collections.Generic;
using UnityEngine;

public class MolePoolConfig : MonoBehaviour
{
    public List<MolePool> molePools = new();

    public GameObject GetRandomMolePrefab()
    {
        float totalOdd = 0;
        foreach (var mole in molePools)
        {
            totalOdd += mole.odd;
        }

        float randomValue = Random.Range(0, totalOdd);
        float cumulativeOdd = 0;

        foreach (var mole in molePools)
        {
            cumulativeOdd += mole.odd;
            if (randomValue <= cumulativeOdd)
            {
                return mole.molePrefab;
            }
        }

        return null; // Fallback in case no prefab is found
    }
}

[System.Serializable]
public class MolePool
{
    public GameObject molePrefab;
    public float odd;
}
