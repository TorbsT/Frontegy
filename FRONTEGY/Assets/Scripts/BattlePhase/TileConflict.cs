﻿using System.Collections.Generic;
using UnityEngine;

public class TileConflict : Conflict
{
    public TileLoc loc { get => _loc; }
    private TileLoc _loc;
    public TileConflict(TileLoc tloc, int roundId, int stepId, List<int> involvedTroops) : base(roundId, stepId, involvedTroops) { _loc = tloc; }

    public override bool sameLoc(Conflict c)
    {
        TileConflict comparable = c as TileConflict;
        if (comparable == null) return false;
        return _loc == comparable._loc;
    }
}
