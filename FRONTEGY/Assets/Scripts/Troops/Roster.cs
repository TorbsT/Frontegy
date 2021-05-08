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
    public void unstageAll()
    {
        foreach (Phy p in getPhys())
        {
            if (p.isStaged()) p.getChy().unstage();
        }
    }
    public Phy getUnstagedPhy()
    {
        int count = getPhyCount();  // used multiple times, and does not change throughout alg
        for (int i = 0; i < count; i++)
        {  // i is used solely to check whether all phys have been checked
            if (isUnstaged(looper))
            {
                Phy p = getPhy(looper);
                return p;
            }
            looper++;
            if (looper >= count) looper = 0;
        }
        Debug.LogError("Not enough Phys in "+GetType()+"!");
        return null;
    }
    private bool isUnstaged(int index) { return !getPhy(index).isStaged(); }
    private Phy getPhy(int index)
    {
        if (outOfBounds(index)) Debug.LogError("Tried getting Phy out of bounds, shoulneverhappen");
        Phy p = getPhys()[index];
        if (p == null) Debug.LogError("IllegalStateException");
        return p;
    }
    private bool outOfBounds(int index) { return index < 0 || index >= getPhyCount(); }
    public GameMaster getGM() { return getRooster().getGM(); }
    protected Rooster getRooster() { if (rooster == null) Debug.LogError("Roster should store a reference to the rooster"); return rooster; }
    private int getPhyCount() { return getPhys().Count; }
    private List<Phy> getPhys() { if (phys == null) Debug.LogError("Should never happen"); return phys; }
}
