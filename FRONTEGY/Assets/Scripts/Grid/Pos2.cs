using UnityEngine;

[System.Serializable]
public struct Pos2
{  // having this as struct allows rapid instantiating without performance issues, apparently
    private static Vector2 unstagedV2 = new Vector2(0f, -10f);
    private bool staged;
    [SerializeField] private Vector2 v2;

    public Pos2(Vector2 v2)
    {
        this.v2 = v2;
        staged = true;
    }
    public Pos2(float x, float z)
    {
        v2 = new Vector2(x, z);
        staged = true;
    }

    public void mod(Vector2 m) { set(v2+m); }
    public void setF(float x, float z) { set(new Vector2(x, z)); }
    public void set(Vector2 v) { v2 = v; stage(); }
    public Vector2 getV2() { return v2; }
    public float getX() { return v2.x; }
    public float getZ() { return v2.y; }
    private void stage() { staged = true; }
    public void unstage() { staged = false; v2 = unstagedV2; }
    public bool isStaged() { return staged; }
    public static Pos2 halfPoint(Pos2 from, Pos2 to)
    {
        return Pos2.lerp(from, to, new Slid(0.5f));
    }
    public static Pos2 lerp(Pos2 from, Pos2 to, Slid slid)
    {
        float s = slid.get();
        Vector2 f = from.getV2();
        Vector2 t = to.getV2();
        return new Pos2(f + s*(t-f));
    }
}
