using System.Collections.Generic;
using UnityEngine;

public class Conflict
{
    public List<TroopStats> originalTroops;
    public bool isBorderBattle;

    public Conflict(List<TroopStats> _originalTroops, bool _isBorderBattle)
    {
        originalTroops = _originalTroops;
        isBorderBattle = _isBorderBattle;
    }
    public void ManualUpdate()
    {

    }
    public void DebugTroops(List<TroopStats> troops)
    {
        foreach (TroopStats troop in troops)
        {
            Debug.Log("The following troop is from team "+troop.playerId);
            foreach (Unit unit in troop.units)
            {
                Debug.Log(unit.role.displayName);
            }
        }
    }
    public List<TroopStats> GetRankedTroops(List<TroopStats> troopsToRank)
    {
        List<TroopStats> troopsInRank = new List<TroopStats>();

        foreach (TroopStats troopToRank in troopsToRank)
        {
            TroopStats rankedTroop = GetRankedTroop(troopToRank);
            troopsInRank.Add(rankedTroop);
        }
        return troopsInRank;
    }
    TroopStats GetRankedTroop(TroopStats troopToRank)  // "In rank" means sorted so:
    {
        // Spy, King, Gen., Col., Capt., Major, Ltn., Pvt.
        TroopStats rankedTroop = new TroopStats();

        foreach (Unit unitToRank in troopToRank.units)  // INSERTION SORT!
        {
            int insertPriority = unitToRank.role.priority;
            int idToInsertInto = rankedTroop.units.Count;
            for (int i = 0; i < rankedTroop.units.Count; i++)
            {
                Unit thisUnit = rankedTroop.units[i];
                int thisPriority = thisUnit.role.priority;
                if (thisUnit.isDead)
                if (insertPriority >= thisPriority)
                {
                    idToInsertInto = i;
                    break;  // we got him boys
                }
                // Worse
            }

            if (idToInsertInto == rankedTroop.units.Count)  // If it's the worst so far!
                rankedTroop.units.Add(unitToRank);
            else
                rankedTroop.units.Insert(idToInsertInto, unitToRank);
        }
        return rankedTroop;
    }
}
