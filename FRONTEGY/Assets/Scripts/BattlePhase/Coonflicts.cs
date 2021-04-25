using System.Collections.Generic;
using UnityEngine;

public class Coonflict
{  // Group of conflicts. maybe first double vowel occurence?
    private List<Conflict> conflicts;

    public Coonflict()
    {
        conflicts = new List<Conflict>();
    }


    public void mergeConflict(Conflict newConflict)
    {  // Tries to merge new conflict in this coonflict.
        for (int i = 0; i < getCount(); i++)
        {
            Conflict conflict = getConflict(i);
            if (conflict.canMerge(newConflict))
            {
                conflict.merge(newConflict);
                return;
            }
        }

        // No suitable conflict to merge into, create new
        getConflicts().Add(newConflict);
    }
    public void merge(Coonflict coonflict)
    {  // Merges two Coonflicts
        foreach (Conflict c in coonflict.getConflicts())
        {
            addConflict(c);
        }
    }
    private void addConflict(Conflict c) { getConflicts().Add(c); }  // Faster than mergeConflict(), but only use if you know what you're doing!
    public Consequi getConsequi()
    {
        Consequi allConsequi = new Consequi();
        foreach (Conflict conflict in getConflicts())
        {
            allConsequi.merge(conflict.makeConsequi());
        }
        return allConsequi;
    }
    private bool sameTonflict(Conflict a, Conflict b)
    {  // same type of conflict
        return (a.GetType() == b.GetType());
    }
    

    private Conflict getConflict(int index)
    {
        if (outOfBounds(index)) Debug.LogError("Should not happen");
        return getConflicts()[index];
    }
    private bool outOfBounds(int index)
    {  // maybe have Groop, Coonflict and all that as subclasses of some shitty class that has these methods already?
        return (index < 0 || index >= getCount());
    }
    public int getCount()
    {
        return getConflicts().Count;
    }
    public List<Conflict> getConflicts()
    {
        if (conflicts == null) Debug.LogError("Should DEFINITELY NEVER HAPPEN");
        return conflicts;
    }
    public Coonflict getStepCoonflict(int step)
    {
        Coonflict coonflict = new Coonflict();
        foreach (Conflict c in getConflicts())
        {
            if (c.isStep(step)) coonflict.addConflict(c);
        }
        return coonflict;
    }
}
