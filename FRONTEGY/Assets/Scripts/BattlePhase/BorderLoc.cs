using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLoc : Loc
{
    private TileLoc a;  // TODO maybe some fancy shit to make it one variable
    private TileLoc b;

    // constructors
    public BorderLoc(TileLoc a, TileLoc b)
    {
        this.a = a;
        this.b = b;
    }

    // virtuals
    public override bool sameLoc(Loc loc)
    {
        BorderLoc comparable = loc as BorderLoc;
        if (comparable == null) return false;
        return sameBorderLoc(comparable);
    }
    public override Pos2 getPos()
    {
        // could convert to FloatLoc first, but that would instantiate a new object every frame. bad
        Pos2 pa = a.getPos();
        Pos2 pb = b.getPos();

        return Pos2.halfPoint(pa, pb);
    }


    // statics

    // normals
    public bool sameBorderLoc(BorderLoc bl)
    {
        TileLoc c = bl.getA();
        TileLoc d = bl.getB();

        bool ac = a.sameLoc(c);
        bool ad = a.sameLoc(d);
        bool bc = b.sameLoc(c);
        bool bd = b.sameLoc(d);

        bool same = (ac && bd) || (ad && bc);
        return same;
    }
    public TileLoc getA() { return a; }
    public TileLoc getB() { return b; }
}
