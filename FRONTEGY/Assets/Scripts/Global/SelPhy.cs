using System.Collections.Generic;
using UnityEngine;

public abstract class SelPhy : Phy, IPlayerOwned
{
    public Player owner { get { return getSelChy().owner; } }
    public int ownerId { get => owner.id; }

    protected override Chy getChy() { return getSelChy(); }
    public abstract SelChy getSelChy();
}
