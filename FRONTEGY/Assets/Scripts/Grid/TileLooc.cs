using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLooc
{
    private List<TileLoc> locs;
    private Tiile validTiile;

    public TileLooc(List<TileLoc> locs)
    {
        if (locs == null) Debug.LogError("Should never happen");
        this.locs = locs;

        
        List<Tile> tiles = new List<Tile>();
        foreach (TileLoc loc in locs)
        {
            Tile t = loc.findTile();
            if (t != null) tiles.Add(t);
        }
        validTiile = new Tiile(tiles);
    }
    public Tiile getValidTiile()
    {
        return validTiile;
    }
    public override string ToString()
    {
        string txt = "TileLooc [ ";
        foreach (TileLoc loc in locs)
        {
            txt += loc;
        }
        txt += "]";
        return txt;
    }
}
