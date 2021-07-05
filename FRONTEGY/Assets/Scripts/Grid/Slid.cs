using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Slid
{
    public float f { get { return _f; } }
    private float _f;

    public Slid(float f)
    {
        _f = Mathf.Clamp(f, 0f, 1f);
    }
    
    public bool isDone()
    {
        return _f == 1f;
    }

    public static Slid operator +(Slid s, float f) => new Slid(s._f + f);
    public static Slid operator +(Slid a, Slid b) => new Slid(a._f + b._f);
    public static Slid operator -(Slid s, float f) => new Slid(s._f - f);
    public static Slid operator -(Slid a, Slid b) => new Slid(a._f - b._f);
}
