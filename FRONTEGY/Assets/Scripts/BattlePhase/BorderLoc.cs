using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLoc
{
    private TileLoc a;  // TODO maybe some fancy shit to make it one variable
    private TileLoc b;

    // constructors
    public BorderLoc(TileLoc a, TileLoc b)
    {
        this.a = a;
        this.b = b;
    }

    public Pos3 toPos3()
    {
        // could convert to FloatLoc first, but that would instantiate a new object every frame. bad
        Pos3 pa = a.toPos3();
        Pos3 pb = b.toPos3();

        return Pos3.halfPoint(pa, pb);
    }


    // statics

    // normals
    public bool sameBorderLoc(BorderLoc bl)
    {  // Ignores direction
        TileLoc c = bl.getA();
        TileLoc d = bl.getB();

        bool ac = a == c;
        bool ad = a == d;
        bool bc = b == c;
        bool bd = b == d;

        bool same = (ac && bd) || (ad && bc);
        return same;
    }
    public TileLoc getA() { return a; }
    public TileLoc getB() { return b; }
}
