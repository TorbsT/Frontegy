using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRoster : Roster
{
    public CardRoster(Rooster rooster, int count) : base(rooster, count) { }  // MAYBUG what is this

    protected override Phy genPhy()
    {
        return new TroopPhy(this);
    }

    public static CardPhy sgetUnstagedPhy() { return GameMaster.sgetUnstagedCardPhy(); }
}
