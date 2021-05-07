using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Phase
{
    public Phase()
    {
        // runs before the construction of any subclass!
    }

    private PhaseType type;

    public PhaseType getType() { return type; }
    protected void setType(PhaseType t) { type = t; }
    public bool bupdate()
    {
        // common phase update method goes here
        return bupdateAbs();
    }
    protected abstract bool bupdateAbs();
}
