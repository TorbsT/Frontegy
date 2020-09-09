using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStats
{
    public int walkRange = 2;
    public int teamId;
    public float scale = 0.3f;
    [System.NonSerialized] public int parentTileId;
    public List<int> path;
}
