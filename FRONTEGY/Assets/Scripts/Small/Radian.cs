using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Radian
{
    [SerializeField] private float x;

    public Radian(float x)
    {
        set(x);
    }
    public Radian(Degree d)
    {
        assimilateFrom(d);
    }
    public void set(float x)
    {
        this.x = clamped(x);
    }
    public float get()
    {
        return x;
    }

    public static float clamped(float x)
    {
        float r = oneRot();
        while (x < 0f) x += r;
        while (x >= r) x -= r;
        return x;
    }
    public void assimilateFrom(Degree d)
    {
        set(Degree.floatDegToFloatRadian(d.get()));
    }
    public static float oneRot() { return 2 * Mathf.PI; }
    // don't add a method for converting to Degrees.
    // application should probably just use degrees as standard.
}
