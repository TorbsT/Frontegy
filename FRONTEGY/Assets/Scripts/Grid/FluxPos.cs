using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluxPos
{  // Class created solely to avoid instantiating new Pos objects every frame for each tile.
    /* UNUSED
    private Loc loc;
    private Pos2 pos;

    public FluxPos(Loc loc)
    {
        setLoc(loc);
    }
    public Pos2 getPos()
    {
        update();
        return pos;
    }
    public Loc getLoc()
    {
        return loc;
    }
    public void setLoc(Loc newLoc)
    {
        if (newLoc == null) Debug.LogError("loc can't be null");
        loc = newLoc;
    }
    private void update()
    {
        Vector3 v = loc.getV3(); 
        pos.set(v);
    }
    */
}
