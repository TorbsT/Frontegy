using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Pos3 : ITransPropertyField<Pos3>
{
    public float x { get { return _p2.x; } }
    public float y { get { return _y; } }
    public float z { get { return _p2.z; } }
    public Vector3 v3 { get { return new Vector3(_p2.x, _y, _p2.z); } }

    [SerializeField] private Pos2 _p2;
    [SerializeField] private float _y;

    public Pos3(Pos2 p2)
    {
        _p2 = p2;
        _y = 1f;  // TODO
    }
    public Pos3(Vector3 v3)
    {
        _p2 = new Pos2(v3.x, v3.z);
        _y = v3.y;
    }
    public Pos3(Pos2 p2, float y)
    {
        _p2 = p2;
        _y = y;
    }
    public Pos3(float x, float y, float z)
    {
        _p2 = new Pos2(x, z);
        _y = y;
    }
    public static Pos3 halfPoint(Pos3 a, Pos3 b)
    {
        return lerp(a, b, new Slid(0.5f));
    }
    public static Pos3 lerp(Pos3 a, Pos3 b, Slid slid)
    {
        return a + (b - a) * slid;
    }
    public static Pos3 operator +(Pos3 a, Pos3 b) => new Pos3(a.v3 + b.v3);
    public static Pos3 operator -(Pos3 a, Pos3 b) => new Pos3(a.v3 - b.v3);
    public static Pos3 operator *(Pos3 p3, float f) => new Pos3(p3.v3 * f);
    public static Pos3 operator *(float f, Pos3 p3) => new Pos3(p3.v3 * f);
    public static Pos3 operator *(Pos3 p3, Slid s) => new Pos3(p3.v3 * s.f);
    public static Pos3 operator *(Slid s, Pos3 p3) => new Pos3(p3.v3 * s.f);
    public static Pos3 operator /(Pos3 p3, float f) => new Pos3(p3.v3 / f);
    public static bool operator ==(Pos3 a, Pos3 b) => a.v3 == b.v3;
    public static bool operator !=(Pos3 a, Pos3 b) => a.v3 != b.v3;
    public override bool Equals(object obj)
    {
        if (!(obj is Pos3)) return false;
        return (Pos3)obj == this;
    }
    public override int GetHashCode()
    {
        return v3.GetHashCode();
    }

    public Pos3 computeWorld(Transform parent) => new Pos3(parent.TransformPoint(v3));
    public Pos3 computeLocal(Transform parent) => new Pos3(parent.InverseTransformPoint(v3));
    public Pos3 transformToProperty(Transform transform) => new Pos3(transform.localPosition);
    public void update(Transform transform) { transform.localPosition = v3; }

    public override string ToString() => v3.ToString();
}
