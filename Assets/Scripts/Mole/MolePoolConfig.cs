using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pooling available moles for config and pick to spawn
/// </summary>
public class MolePoolConfig : MonoBehaviour
{
    public int pointMultiplier = 1;
    public float waitDurationMultiplier = 1;
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

    // --------- Modifier ---------
    public void MultiplyMolePoint(Mole mole)
    {
        mole.Point *= pointMultiplier;
    }

    public void MultiplyMoleWaitDuration(Mole mole)
    {
        mole.WaitDuration *= waitDurationMultiplier;
    }

    public void SetMolePoolOdd(int index, float odd)
    {
        if (index >= 0 && index < molePools.Count)
        {
            molePools[index].odd = odd;
        }
        else
        {
            Debug.LogError("Index out of range");
        }
    }
}

[System.Serializable]
public class MolePool
{
    public GameObject molePrefab;
    public float odd;
}
