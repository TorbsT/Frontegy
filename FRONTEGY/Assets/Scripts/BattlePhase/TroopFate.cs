using System.Collections.Generic;
using UnityEngine;

public class TroopFate
{  // What will happen to a troop throughout a battlephase?
    // UNUSED: see Consequence
    private Troop belongsToTroop; 
    List<Consequence> events;

    public bool isFateOfTroop(Troop compare)
    {
        bool same = belongsToTroop.isThisTroop(compare);
        return same;
    }
    public void setDeath(int stepId)
    {
        getEventAt(stepId).setDies();
    }
    public bool deadBy(int stepId)
    {
        for (int i = 0; i < stepId; i++)
        {
            if (diesAt(i)) return true;
        }
        return false;
    }
    public bool diesAt(int stepId)
    {
        return getEventAt(stepId).getDies();
    }
    private Consequence getEventAt(int index)
    {
        if (eventOutOfRange(index))
        {
            Debug.LogError("Tried getting fate out of range");
            return null;
        }
        return events[index];
    }
    private bool eventOutOfRange(int index)
    {
        return (index < 0 || index >= getEventCount());
    }
    private int getEventCount()
    {
        if (events == null)
        {
            Debug.LogError("FateEvents should not be null");
            return 0;
        }
        return events.Count;
    }
}
