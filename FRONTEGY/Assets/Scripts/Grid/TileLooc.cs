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
        validTiile = new Tiile(tiles);
        foreach (TileLoc loc in locs)
        {
            Tile t = loc.findTile();
            if (t != null) validTiile.add(t);
        }
        validTiile = new Tiile(tiles);
    }
    public Tiile getValidTiile()
    {
        return validTiile;
    }
}
