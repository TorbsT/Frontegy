using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopRoster : Roster
{
    public TroopRoster(Rooster rooster, int count) : base(rooster, count) { }  // MAYBUG what is this

    protected override Phy genPhy()
    {
        return new TroopPhy(this);
    }

    public static TroopPhy sgetUnstagedPhy() { return GameMaster.sgetUnstagedTroopPhy(); }
}
