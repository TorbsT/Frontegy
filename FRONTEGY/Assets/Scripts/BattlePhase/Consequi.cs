using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consequi
{
    private List<Consequence> consequences;
    public Consequi()
    {
        consequences = new List<Consequence>();
    }

    private List<Consequence> getByTroop(Troop troop)
    {
        List<Consequence> cs = new List<Consequence>();
        foreach (Consequence c in getConsequences())
        {
            if (c.troopMatches(troop)) cs.Add(c);
        }
        return cs;
    }
    private List<Consequence> getByStep(int step)
    {
        List<Consequence> cs = new List<Consequence>();
        foreach (Consequence c in getConsequences())
        {
            if (c.stepMatches(step)) cs.Add(c);
        }
        return cs;
    }
    private Consequence getOne(int step, Troop troop)
    {
        foreach (Consequence c in getConsequences())
        {
            if (c.allMatches(step, troop)) return c;
        }
        return null;
    }
    public void merge(Consequi consequi)
    {
        foreach (Consequence c in consequi.getConsequences())
        {
            add(c);
        }
        // TODO maybe should empty consequi?
    }

    public bool deadBy(int step, Troop troop)
    {
        foreach (Consequence c in getByTroop(troop))
        {  // Faster this way rather than using diesAt
            if (c.getDies()) return true;
        }
        return false;
    }
    public bool diesAt(int step, Troop troop)
    {  // TODO getDies is used both here and in deadBy, maybe have a method for it here in Consequi
        return getOne(step, troop).getDies();
    }
    public void add(Consequence c) { getConsequences().Add(c); }
    public List<Consequence> getConsequences() { return consequences; }
}
