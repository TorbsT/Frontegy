using UnityEngine;

[System.Serializable]
public struct Pos2
{  // having this as struct allows rapid instantiating without performance issues, apparently
    public float x { get { return _v2.x; } }
    public float z { get { return _v2.y; } }

    [SerializeField] private Vector2 _v2;

    public Pos2(Vector2 v2)
    {
        this._v2 = v2;
    }
    public Pos2(float x, float z)
    {
        _v2 = new Vector2(x, z);
    }
    public static Pos2 halfPoint(Pos2 a, Pos2 b)
    {
        return lerp(a, b, new Slid(0.5f));
    }
    public static Pos2 lerp(Pos2 a, Pos2 b, Slid slid)
    {
        return a + (b - a) * slid;
    }

    public static Pos2 operator *(Pos2 p2, float f) => new Pos2(p2._v2 * f);
    public static Pos2 operator *(float f, Pos2 p2) => new Pos2(p2._v2 * f);
    public static Pos2 operator *(Pos2 p2, Slid s) => new Pos2(p2._v2 * s.f);
    public static Pos2 operator *(Slid s, Pos2 p2) => new Pos2(p2._v2 * s.f);
    public static Pos2 operator /(Pos2 p2, float f) => new Pos2(p2._v2 / f);
    public static Pos2 operator +(Pos2 a, Pos2 b) => new Pos2(a._v2 + b._v2);
    public static Pos2 operator -(Pos2 a, Pos2 b) => new Pos2(a._v2 - b._v2);
}
