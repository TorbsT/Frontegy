using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TileLoc
{
    public int x { get { return _x; } }
    public int y { get { return _y; } }
    public int z { get { return _z; } }

    private int _x { get { return _xyz.x; } }
    private int _y { get { return _xyz.y; } }
    private int _z { get { return _xyz.z; } }
    [SerializeField] private Vector3Int _xyz;

    // constructors
    public TileLoc(Vector2Int xz) { _xyz = new Vector3Int(xz.x, 0, xz.y); }
    public TileLoc(int x, int z) { _xyz = new Vector3Int(x, 0, z); }
    public TileLoc(int x, int y, int z) { _xyz = new Vector3Int(x, y, z); }
    public TileLoc(Vector3Int xyz) { _xyz = xyz; }

    public Pos3 toPos3() => new Pos3(_xyz)*1f;
    public static int stepsBetween(TileLoc a, TileLoc b) { return (a-b).getLength(); }
    public int getLength() { return Mathf.Abs(x) + Mathf.Abs(z); }
    public Tiile getValidNeigTiile()
    {
        TileLooc looc = getNeigLooc();
        return looc.getValidTiile();
    }
    public static bool areNeigs(TileLoc a, TileLoc b)
    {
        return stepsBetween(a, b) == 1;
        // return getNeigLooc().contains(tl); BAD
    }
    public TileLooc getNeigLooc()
    {
        // don't cache this - tiles may be disabled throughout run
        List<TileLoc> locs = new List<TileLoc>();
        locs.Add(north());
        locs.Add(east());
        locs.Add(south());
        locs.Add(west());
        TileLooc looc = new TileLooc(locs);
        return looc;
    }
    public TileLoc north() { return new TileLoc(x+1, z); }
    public TileLoc east() { return new TileLoc(x, z+1); }
    public TileLoc south() { return new TileLoc(x-1, z); }
    public TileLoc west() { return new TileLoc(x, z-1); }
    public Tile findTile()
    {
        return GameMaster.sfindTile(this);
    }

    public static bool operator ==(TileLoc a, TileLoc b) => a._xyz == b._xyz;
    public static bool operator !=(TileLoc a, TileLoc b) => a._xyz != b._xyz;
    public static TileLoc operator +(TileLoc a, TileLoc b) => new TileLoc(a._xyz + b._xyz);
    public static TileLoc operator -(TileLoc a, TileLoc b) => new TileLoc(a._xyz - b._xyz);

    public override bool Equals(object obj)
    {
        if (!(obj is TileLoc)) return false;
        TileLoc tl = (TileLoc)obj;
        return _xyz == tl._xyz;
    }
    public override int GetHashCode()
    {
        return _xyz.GetHashCode();
    }

    public static Pos3 toPos3(Vector3 v3) { return toPos3(v3.x, v3.y, v3.z); }
    public static Pos3 toPos3(float x, float y, float z) { return new Pos3(x, y, z); }
}
