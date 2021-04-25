using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roster
{  // Provides a way to reuse GameObjects, and by extension MonoBehaviour scripts.
    private Rooster rooster;
    private List<Phy> phys;
    private int looper;
    
    public Roster(Rooster rooster, int phyCount)
    {
        this.rooster = rooster;
        phys = new List<Phy>();
        genPhys(phyCount);
    }
    
    private void genPhys(int phyCount)
    {
        for (int i = 0; i < phyCount; i++)
        {
            phys.Add(genPhy());
        }
    }
    protected virtual Phy genPhy()
    {
        Debug.LogError("Should never happen, did you forget to override this function?");
        return null;
    }
    public Phy getUnstagedPhy()
    {
        int count = getPhyCount();  // used multiple times, and does not change throughout alg
        for (int i = 0; i < count; i++)
        {  // i is used solely to check whether all phys have been checked
            if (isUnstaged(looper)) return getPhy(looper);
            looper++;
            if (looper >= count) looper = 0;
        }
        Debug.LogWarning("Not enough Phys in "+GetType()+"!");
        return null;
    }
    private bool isUnstaged(int index) { return getPhy(index) != null; }
    private Phy getPhy(int index) { if (outOfBounds(index)) Debug.LogError("Tried getting Phy out of bounds, shoulneverhappen"); return getPhys()[index]; }
    private bool outOfBounds(int index) { return index < 0 || index >= getPhyCount(); }
    public GameMaster getGM() { return getRooster().getGM(); }
    private Rooster getRooster() { if (rooster == null) Debug.LogError("Roster should store a reference to the rooster"); return rooster; }
    private int getPhyCount() { return getPhys().Count; }
    private List<Phy> getPhys() { if (phys == null) Debug.LogError("Should never happen"); return phys; }
}
