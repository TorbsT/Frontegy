using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TroopStats  // "Must" be class since SetStats() can modify these values
{
    public int id;
    public int walkRange;
    public int playerId;
    public float scale = 0.5f;
    public int parentTileId;
    public List<Breadcrumb> path;
    public List<Unit> units;
}
