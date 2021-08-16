using System.Collections.Generic;
using UnityEngine;

public class BorderConflict : Conflict
{
    public BorderLoc borderLoc { get => _borderLoc; }
    private BorderLoc _borderLoc;
    public BorderConflict(int roundId, int stepId, List<int> involvedTroops) : base(roundId, stepId, involvedTroops) { }

    public override bool sameLoc(Conflict compare)
    {
        if (!(compare is BorderConflict comparable)) return false;
        return _borderLoc.sameBorderLoc(comparable._borderLoc);
    }
}
