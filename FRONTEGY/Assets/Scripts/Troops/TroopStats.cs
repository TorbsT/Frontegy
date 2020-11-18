using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TroopStats  // "Must" be class since SetStats() should be able to modify these values
{
    public TroopStats(int _playerId, List<Unit> _units)
    {
        playerId = _playerId;
        units = _units;
    }
    public int id;
    public int playerId;
    public float scale = 0.5f;
    public int parentTileId;
    public List<Breadcrumb> path;
    public List<Unit> units;

    public int GetWalkRange()
    {
        return GetMaxRange();
    }
    public int GetMaxRange()
    {
        int range = 0;
        foreach (Unit unit in units)
        {
            int myRange = unit.myRole.stats.RANGE;
            if (myRange > range) range = myRange;
        }
        return range;
    }
}
