using System.Collections.Generic;
using UnityEngine;

public class MolePoolConfig : MonoBehaviour
{
    public List<MolePool> molePools = new();
}

[System.Serializable]
public class MolePool
{
    public GameObject molePrefab;
    public float odd;
}
