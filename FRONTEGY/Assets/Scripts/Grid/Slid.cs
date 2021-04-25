using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slid
{
    private float slid;

    public Slid()
    {
        set(0f);
    }
    public Slid(float f)
    {
        set(f);
    }
    
    public bool isDone()
    {
        return get() == 1f;
    }
    public float get()
    {
        return slid;
    }
    public void add(float f)
    {
        set(slid + f);
    }
    public void set(float f)
    {
        slid = Mathf.Clamp(f, 0f, 1f);
    }
}
