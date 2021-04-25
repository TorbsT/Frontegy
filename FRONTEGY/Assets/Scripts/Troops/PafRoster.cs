using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PafRoster : Roster
{
    public PafRoster(Rooster rooster, int phyCount) : base(rooster, phyCount) { }

    protected override Phy genPhy()
    {
        return new PafPhy(this);
    }

    public static PafPhy sgetUnstagedPhy() { return GameMaster.sgetUnstagedPafPhy(); }
}
