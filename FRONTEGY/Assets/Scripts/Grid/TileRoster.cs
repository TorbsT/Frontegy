using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileRoster : Roster
{
    public TileRoster(Rooster rooster, int count) : base(rooster, count) { }  // MAYBUG what is this

    protected override Phy genPhy()
    {
        return new TilePhy(this);
    }
}
