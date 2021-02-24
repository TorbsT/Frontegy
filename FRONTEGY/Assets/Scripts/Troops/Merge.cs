using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge
{
    public Merge(Troop _a, Troop _b)
    {
        a = _a;
        b = _b;
    }
    public Troop a;
    public Troop b;

    public void DoMerge()
    {
        foreach (Unit unit in b.stats.units)
        {
            a.stats.units.Add(unit);
        }
        a.gameMaster.grid.data.GetTroops().Remove(b);
        GameObject.Destroy(b.selGO);
    }
}
