using UnityEngine;

public class TileConflict : Conflict
{
    public TileConflict(int step, Troop a, Troop b) : base(step, a, b) { }

    private Loc loc;

    public Loc getLoc()
    {
        return loc;
    }
    public override bool sameLoc(Conflict c)
    {
        TileConflict comparable = c as TileConflict;
        if (comparable == null) return false;
        return getLoc() == comparable.getLoc();
    }
}
