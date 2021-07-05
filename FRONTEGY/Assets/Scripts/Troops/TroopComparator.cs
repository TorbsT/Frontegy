using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopComparator : Comparer<Troop>
{
    public override int Compare(Troop x, Troop y)
    {
        return x.getUnit().getPOW() - y.getUnit().getPOW();
    }
}
