using System.Collections.Generic;
using UnityEngine;

public class Conflict
{
    public Conflict(Troop _a, Troop _b)
    {
        a = _a;
        b = _b;
    }
    //public List<TroopStats> originalTroops;
    // Use an easier structure, not working at NASA here:
    public Troop a;
    public Troop b;
    public bool isBorderBattle;

    public void ManualUpdate()
    {

    }
    public void AutoResolve()
    {
        int winnerPlayerId;

        float aPower = a.stats.units[0].myRole.stats.ATK;
        float bPower = b.stats.units[0].myRole.stats.ATK;
        if (aPower > bPower)
        {
            winnerPlayerId = a.stats.playerId;
        }
        else winnerPlayerId = b.stats.playerId;
        Debug.Log("Winner: " + winnerPlayerId);
    }
    public void DebugTroops(List<TroopStats> troops)
    {
        foreach (TroopStats troop in troops)
        {
            Debug.Log("The following troop is from team "+troop.playerId);
            foreach (Unit unit in troop.units)
            {
                Debug.Log(unit.myRole.displayName);
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
        TroopStats rankedTroop = new TroopStats(0, new List<Unit>()); // BAD BAD BAD BAD BAD BAD BAD BAD BAD

        foreach (Unit unitToRank in troopToRank.units)  // INSERTION SORT!
        {
            int insertPriority = unitToRank.myRole.priority;
            int idToInsertInto = rankedTroop.units.Count;
            for (int i = 0; i < rankedTroop.units.Count; i++)
            {
                Unit thisUnit = rankedTroop.units[i];
                int thisPriority = thisUnit.myRole.priority;
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
