using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loc
{  // MY FIRST EVER INTERFACE (in C# atleast, made in Java for TDT4100 before)
    public abstract Pos2 getPos();

    public abstract bool sameLoc(Loc loc);
}
