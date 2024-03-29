﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Groop
{  // group of troops
    // TODO make AllGroop
    private List<Troop> troops;

    public Groop()
    {
        troops = new List<Troop>();
    }
    public Groop(List<Troop> troops)
    {
        if (troops == null) Debug.LogError("IllegalArgumentException");
        this.troops = troops;
    }
    public List<Troop> filter(Predicate<Troop> predicate) => getTroops().FindAll(predicate);
    private bool troopOutOfBounds(int index) { return index < 0 || index >= getCount(); }
    private bool noTroops() { return getCount() == 0; }
    public int getCount()
    {
        if (troops == null) return 0;
        return troops.Count;
    }
    public Troop getTroop(int id) => getTroops().Find(troop => troop.id == id);
    private Troop getTroopAtIndex(int index)
    {
        if (troopOutOfBounds(index))
        {
            Debug.LogError("Should probably not happen");
            return null;
        }
        return troops[index];
    }
    public void add(Troop t) { getTroops().Add(t); }
    private List<Troop> getTroops()
    {
        if (troops == null) Debug.LogError("Should never happen");
        return troops;
    }
    public void tacticalStart()
    {
        foreach (Troop t in getTroops())
        {
            t.tacticalStart();
        }
    }
    public void weiterUpdate(WeiterView wv)
    {
        foreach (Troop t in getTroops())
        {
            t.weiterUpdate(wv);
        }
    }
}
