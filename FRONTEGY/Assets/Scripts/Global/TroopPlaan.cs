using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopPlaan
{
    private List<TroopPlan> plans;

    public TroopPlaan()
    {
        plans = new List<TroopPlan>();
    }
    public Tile lastTileInPaf(Troop troop) { return getPlan(troop).lastTileInPaf(); }
    public bool hasPafChy(Troop troop)
    {
        return getPafChy(troop) != null;
    }
    public PafChy getPafChy(Troop troop)
    {
        TroopPlan tp = getPlan(troop);
        if (tp == null) return null;
        return tp.getPafChy();
    }
    public bool hasPlanFor(Troop troop)
    {
        return getPlan(troop) != null;
    }
    public TroopPlan getPlan(Troop troop)
    {
        foreach (TroopPlan tp in getPlans())
        {
            if (tp.hasTroop(troop)) return tp;
        }
        return null;
    }
    public void plan(Troop troop, PafChy pafChy)
    {
        TroopPlan tp = getPlan(troop);
        if (tp != null)
        {
            tp.getPafChy().unstage();
            tp.setPafChy(pafChy);
        } else
        {
            tp = new TroopPlan(troop, pafChy);
            getPlans().Add(tp);
        }
    }
    public int getMaxSteps()
    {
        int maxSteps = 0;
        foreach (TroopPlan troopPlan in getPlans())
        {
            int steps = troopPlan.getSteps();
            if (maxSteps > steps) maxSteps = steps;
        }
        return maxSteps;
    }
    private List<TroopPlan> getPlans()
    {
        if (plans == null) Debug.LogError("IllegalStateException");
        return plans;
    }
}
