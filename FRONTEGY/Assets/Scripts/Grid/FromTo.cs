using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FromTo
{
    public TileLoc from { get; private set; }
    public TileLoc to { get; private set; }
    public FromTo(Breadcrumb from, Breadcrumb to)
    {
        this.from = from.tile.loc;
        this.to = to.tile.loc;
    }
    public FromTo(TileLoc from, TileLoc to)
    {
        this.from = from;
        this.to = to;
    }
    public FromTo(Tile from, Tile to)
    {
        if (from == null) Debug.LogError("IllegalArgumentException");
        if (to == null) Debug.LogError("IllegalArgumentException");
        this.from = from.loc;
        this.to = to.loc;
    }

    public static bool meet(FromTo a, FromTo b)
    {
        return a.to == b.to;
    }
    public static bool pass(FromTo a, FromTo b)
    {
        return a.to == b.from && a.from == b.to;
    }
}
