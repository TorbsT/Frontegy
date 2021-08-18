using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Pos3 : ITransPropertyField<Pos3>
{
    public float x { get { return _v3.x; } }
    public float y { get { return _v3.y; } }
    public float z { get { return _v3.z; } }
    public Vector3 v3 { get => _v3; }
    public Pos2 pos2 { get => new Pos2(_v3.x, _v3.z); }

    [SerializeField] private Vector3 _v3;

    public Pos3(Pos2 p2)
    {
        _v3 = new Vector3(p2.x, 0f, p2.z);
    }
    public Pos3(Vector3 v3)
    {
        _v3 = v3;
    }
    public Pos3(Pos2 p2, float y)
    {
        _v3 = new Vector3(p2.x, y, p2.z);
    }
    public Pos3(float x, float y, float z)
    {
        _v3 = new Vector3(x, y, z);
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
    /*
    public Pos3 computeWorld(Transform parent) => new Pos3(parent.TransformPoint(v3));
    public Pos3 computeLocal(Transform parent) => new Pos3(parent.InverseTransformPoint(v3));
    */
    public Pos3 computeWorld(Pos3 parentPos) => this + parentPos;
    public Pos3 computeLocal(Pos3 parentPos) => this - parentPos;
    public Pos3 transformToProperty(Transform transform) => new Pos3(transform.localPosition);
    public void update(Transform transform) { transform.localPosition = v3; }

    public override string ToString() => v3.ToString();
}
