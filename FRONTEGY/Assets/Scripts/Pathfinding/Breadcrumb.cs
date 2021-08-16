using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]  // for objectcopiers sake
public struct Breadcrumb
{  // For scalability reasons class is better, but maybe struct is performance-wise better so idk
    public Tile tile { get; private set; }
    public int stepsRemaining { get; private set; }  // affected by portals
    //private int step;  // not affected by portals, unused


    public Breadcrumb(Tile tile, int stepsRemaining)
    {
        this.tile = tile;
        this.stepsRemaining = stepsRemaining;
    }
    public bool isNeigOfBC(Breadcrumb b) { return isNeigOfTile(b.getTile()); }
    public bool isNeigOfTile(Tile t) { return getTile().isNeigOfTile(t); }
    public bool sameTileAs(Breadcrumb b) { return isTile(b.getTile()); }
    public bool isTile(Tile t) { return getTile() == t; }
    public Tiile getValidNeigTiile() { return getTile().loc.getValidNeigTiile(); }
    public Tile getTile() { if (isInvalid()) Debug.LogError("TRIED USING INVALID BREADCRUMB"); return tile; }
    public bool isInvalid() { return !isValid(); }  
    public bool isValid() { return tile != null; } // don't use getTile() here
    public void showMark() { getTile().showMark(this); }
    public void hideMark() { getTile().hideMark(); }

    public static Breadcrumb makeInvalid()
    {
        return new Breadcrumb(null, -1);
    }
    public static Breadcrumb makeStarter(Tile t, int stepsRemaining)
    {
        return new Breadcrumb(t, stepsRemaining);
    }
    public static Breadcrumb makeNeig(Tile t, int stepsRemaining)
    {
        return new Breadcrumb(t, stepsRemaining - 1);
    }

    public List<Breadcrumb> getValidNeigBreadcrumbs()
    {
        List<Breadcrumb> bcs = new List<Breadcrumb>();
        if (stepsRemaining == 0) return bcs;
        foreach (Tile tile in getValidNeigTiile().getTiles())
        {
            Breadcrumb bc = new Breadcrumb(tile, stepsRemaining - 1);
            bcs.Add(bc);
        }
        return bcs;
    }

    public static bool sameTile(Breadcrumb a, Breadcrumb b) => a.tile == b.tile;

    public static bool operator <(Breadcrumb a, Breadcrumb b) => sameTile(a, b) && a.stepsRemaining < b.stepsRemaining;
    public static bool operator >(Breadcrumb a, Breadcrumb b) => sameTile(a, b) && a.stepsRemaining > b.stepsRemaining;
    public static bool operator <=(Breadcrumb a, Breadcrumb b) => sameTile(a, b) && a.stepsRemaining <= b.stepsRemaining;
    public static bool operator >=(Breadcrumb a, Breadcrumb b) => sameTile(a, b) && a.stepsRemaining >= b.stepsRemaining;
    public static bool operator ==(Breadcrumb a, Breadcrumb b) => sameTile(a, b) && a.stepsRemaining == b.stepsRemaining;
    public static bool operator !=(Breadcrumb a, Breadcrumb b) => !sameTile(a, b) || a.stepsRemaining != b.stepsRemaining;
    public override bool Equals(object obj)
    {
        if (!(obj is Breadcrumb)) return false;
        Breadcrumb b = (Breadcrumb)obj;
        return b == this;
    }
    public override int GetHashCode()
    {
        return tile.GetHashCode();
    }
    public override string ToString()
    {
        return "(" + tile + " " + stepsRemaining + ")";
    }

}
