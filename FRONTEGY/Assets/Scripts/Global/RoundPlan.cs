using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundPlan
{
    //private Tiile tiile;
    private TroopPlaan troopPlaan;
    public RoundPlan()
    {
        troopPlaan = new TroopPlaan();
    }
    public Tile lastTileInPaf(Troop troop) { return getTroopPlaan().lastTileInPaf(troop); }
    public PafChy getTroopPafChy(Troop troop)
    {
        return getTroopPlaan().getPafChy(troop);
    }
    public TroopPlan getTroopPlan(Troop troop)
    {
        return getTroopPlaan().getPlan(troop);
    }
    public void planTroop(Troop t, PafChy pafChy)
    {
        getTroopPlaan().plan(t, pafChy);
    }
    public int getMaxSteps()
    {
        return getTroopPlaan().getMaxSteps();
    }
    public TroopPlaan getTroopPlaan()
    {
        if (troopPlaan == null) Debug.LogError("IllegalStateException");
        return troopPlaan;
    }

}
