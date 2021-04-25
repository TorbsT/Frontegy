using UnityEngine;

public class BorderConflict : Conflict
{
    public BorderConflict(int step, Troop a, Troop b) : base(step, a, b) { }

    private BorderLoc borderLoc;

    public BorderLoc getBLoc()
    {
        return borderLoc;
    }
    public override bool sameLoc(Conflict compare)
    {
        BorderConflict comparable = compare as BorderConflict;
        if (comparable == null) return false;
        BorderLoc a = getBLoc();
        BorderLoc b = comparable.getBLoc();
        return a.sameBorderLoc(b);
    }
}
