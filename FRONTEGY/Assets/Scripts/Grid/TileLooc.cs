using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLooc
{
    private List<TileLoc> locs;

    public TileLooc()
    {
        locs = new List<TileLoc>();
    }
    public TileLooc(List<TileLoc> locs)
    {
        if (locs == null) Debug.LogError("Should never happen");
        this.locs = locs;
    }

    public Tiile toValidTiile()
    {
        Tiile tiile = new Tiile();
        foreach (TileLoc loc in getLocs())
        {
            Tile t = loc.findTile();
            if (t != null) tiile.add(t);
        }
        return tiile;
    }
    public void add(TileLoc loc)
    {
        getLocs().Add(loc);
    }
    public bool contains(TileLoc find)
    {
        foreach (TileLoc tl in getLocs())
        {
            if (tl.sameLoc(find)) return true;
        }
        return false;
    }
    public List<TileLoc> getLocs() { return locs; }
}
