using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Degree
{
    [SerializeField] private float x;
    private Vector2 limits;
    private static Vector2 defLimits = new Vector2(0f, oneRot());

    public Degree(float x)
    {
        limits = defLimits;
        set(x);
    }
    public Degree(float x, Vector2 limits)
    {
        if (limits[0] > limits[1]) Debug.LogError("IllegalArgumentException");
        this.limits = limits;
        set(x);
    }

    public void add(float x)
    {
        set(this.x + x);
    }
    public void set(float x)
    {
        x = clamped(x);
        this.x = limited(x);
    }
    public float get()
    {
        return x;
    }
    private float limited(float x)
    {
        return Mathf.Clamp(x, limits[0], limits[1]);
    }
    public static float clamped(float x)
    {
        float r = oneRot();
        while (x < 0f) x += r;
        while (x >= r) x -= r;
        return x;
    }
    public static float floatDegToFloatRadian(float x)
    {
        return x * Mathf.PI / 180f;
    }
    public static float oneRot() { return 360f; }
}
