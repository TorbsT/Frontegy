using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FloatLoc
{
    private Vector2 lw;

    // constructors
    public FloatLoc(TileLoc tl)
    {
        lw = tl.getLW();
    }
    public FloatLoc(BorderLoc bl)
    {
        Vector2 a = bl.getA().getLW();
        Vector2 b = bl.getB().getLW();
        lw = (a + b) / 2f;
    }

    // statics

    // normals
    public float getL() { return lw.x; }
    public float getW() { return lw.y; }
}
