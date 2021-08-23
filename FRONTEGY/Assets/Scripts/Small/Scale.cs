using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Scale
{
    public static Scale identity => new Scale(Vector3.one);
    public float x { get => _v3.x; set { _v3.x = value; } }
    public float y { get => _v3.y; set { _v3.y = value; } }
    public float z { get => _v3.z; set { _v3.z = value; } }
    public Vector3 v3 { get => _v3; set { _v3 = value; } }

    [SerializeField] private Vector3 _v3;

    public Scale(Vector3 v3)
    {
        _v3 = v3;
    }
    public Scale(float s)
    {
        _v3 = new Vector3(s, s, s);
    }
    public Scale(float x, float y, float z)
    {
        _v3 = new Vector3(x, y, z);
    }

    public static Scale operator *(Scale s, float f) => new Scale(s._v3 * f);
    public static Scale operator *(Scale a, Scale b) => a * b._v3;
    public static Scale operator *(Scale a, Vector3 v) => new Scale(a.x*v.x, a.y*v.y, a.z*v.z);
    public static Scale operator /(Scale s, float f)
    {
        if (f == 0f) throw new System.DivideByZeroException("Tried dividing " + s + " by " + f);
        return new Scale(s._v3 / f);
    }
    public static Scale operator /(Scale a, Scale b) => a / b._v3;
    public static Scale operator /(Scale a, Vector3 v)
    {
        if (v.x == 0 || v.y == 0 || v.z == 0) throw new System.DivideByZeroException("Tried dividing " + a + " by " + v);
        return new Scale(a.x / v.x, a.y / v.y, a.z / v.z);
    }
    public override string ToString() => "Scale"+_v3.ToString();
}
