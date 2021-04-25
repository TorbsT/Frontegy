using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Pos3
{
    private Pos2 pos2;
    private float y;

    public Pos3(Pos2 pos2)
    {
        this.pos2 = pos2;
        y = 1f;  // TODO
    }
    public Pos3(Vector3 v3)
    {
        pos2 = new Pos2(v3.x, v3.z);
        y = v3.y;
    }
    public Pos3(Pos2 pos2, float y)
    {
        this.pos2 = pos2;
        this.y = y;
    }
    public Pos3(float x, float y, float z)
    {
        pos2 = new Pos2(x, z);
        this.y = y;
    }

    public Vector3 getV3()
    {
        return new Vector3(getX(), y, getZ());
    }
    public static Pos3 sum(Pos3 a, Pos3 b)
    {
        return new Pos3(a.getV3() + b.getV3());
    }
    public float getX() { return pos2.getX(); }
    public float getY() { return y; }
    public float getZ() { return pos2.getZ(); }
    public Vector2 getV2() { return pos2.getV2(); }
    public Pos2 getPos2() { return pos2; }
}
