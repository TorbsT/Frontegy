using System.Collections.Generic;
using UnityEngine;

public class Groop
{  // group of troops
    // TODO make AllGroop
    private List<Troop> troops;

    public Groop()
    {
        troops = new List<Troop>();
    }
    public Groop(List<Troop> troops)
    {
        if (troops == null) Debug.LogError("IllegalArgumentException");
        this.troops = troops;
    }
    private bool troopOutOfBounds(int index) { return index < 0 || index >= getCount(); }
    private bool noTroops() { return getCount() == 0; }
    public int getCount()
    {
        if (troops == null) return 0;
        return troops.Count;
    }
    public void newRound(Results results)
    {
        if (results == null) Debug.LogError("IllegalArgumentException");
        foreach (Troop troop in getTroops())
        {
            troop.newRound(results);
        }
    }
    private Troop getTroop(int index)
    {
        if (troopOutOfBounds(index))
        {
            Debug.LogError("Should probably not happen");
            return null;
        }
        return troops[index];
    }
    public void add(Troop t) { getTroops().Add(t); }
    private List<Troop> getTroops()
    {
        if (troops == null) Debug.LogError("Should never happen");
        return troops;
    }
    public void tacticalStart()
    {
        foreach (Troop t in getTroops())
        {
            t.tacticalStart();
        }
    }
    
    public Coonflict getCoonflict(int step, Consequi consequi)
    {  // coonflict not stored in state, rather computed
        Coonflict coonflict = new Coonflict();

        int troopCount = getCount();  // doesn't change throughout this algorithm
        if (troopCount <= 0) Debug.LogError("This should never happen");
        for (int a = 0; a < troopCount; a++)
        {
            Troop at = getTroop(a);


            if (consequi.deadBy(step, at)) continue;


            for (int b = a+1; b < troopCount; b++)
            {
                Troop bt = getTroop(b);


                if (consequi.deadBy(step, bt)) continue;


                Conflict newConflict = at.findConflictByStepAndTroop(step, bt);
                if (newConflict == null) continue;  // no suitable conflict found


                coonflict.mergeConflict(newConflict);  // should merge appropriately
            }
        }
        return coonflict;
    }
    public void weiterUpdate(WeiterView wv)
    {
        foreach (Troop t in getTroops())
        {
            t.weiterUpdate(wv);
        }
    }
}
