using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Loc
{  // MY FIRST EVER INTERFACE (in C# atleast, made in Java for TDT4100 before)
    public abstract Pos2 toPos();

    public abstract bool sameLoc(Loc loc);
}
