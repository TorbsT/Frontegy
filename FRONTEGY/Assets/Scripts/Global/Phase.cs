using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Phase
{
    private PhaseManager pm;

    public Phase(PhaseManager pm)
    {
        if (pm == null) Debug.LogError("IllegalArgumentException");
        this.pm = pm;
        // runs before the construction of any subclass!
    }

    private PhaseType type;

    public PhaseType getType() { return type; }
    protected void setType(PhaseType t) { type = t; }
    public bool bupdate()
    {
        // common phase update method goes here
        return bupdateAbstra();
    }
    protected abstract bool bupdateAbstra();
    public Grid getGrid() { return getPhaseManager().getGrid(); }
    public PhaseManager getPhaseManager() { if (pm == null) Debug.LogError("IllegalStateException"); return pm; }
}
