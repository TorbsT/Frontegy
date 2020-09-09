using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitStats
{
    public int walkRange = 10;
    public int teamId;
    public float scale = 0.3f;
    [System.NonSerialized] public Tile parentTile;
    public List<Vector2Int> path;
}
