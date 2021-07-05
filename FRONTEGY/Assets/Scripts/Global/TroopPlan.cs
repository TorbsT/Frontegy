using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopPlan
{
    // Contains all user input for a tactical phase
    private Troop troop;
    private PafChy pafChy;

    public TroopPlan(Troop troop, PafChy pafChy)
    {
        if (troop == null) Debug.LogError("IllegalArgumentException");
        this.troop = troop;
        setPafChy(pafChy);
    }

    public Tile lastTileInPaf() { return getPafChy().lastTile(); }
    public bool hasTroop(Troop t)
    {
        return (getTroop().Equals(t));
    }
    public void setPafChy(PafChy pafChy)
    {
        if (pafChy == null) Debug.LogError("IllegalArgumentException");
        this.pafChy = pafChy;
    }
    public Troop getTroop()
    {
        if (troop == null) Debug.LogError("IllegalStateException");
        return troop;
    }
    public FromTo getFromTo(int step) { return getPafChy().getFromTo(step); }
    public PafChy getPafChy()
    {
        if (pafChy == null) Debug.LogError("IllegalStateException");
        return pafChy;
    }
    public int getSteps()
    {
        return getPafChy().getSteps();
    }
}
  