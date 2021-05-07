using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLoc : Loc
{
    [SerializeField] private Vector2Int lw;

    // constructors
    public TileLoc(Vector2Int lw) { this.lw = lw; }
    public TileLoc(int l, int w) { this.lw = new Vector2Int(l, w); }

    // virtuals
    public override bool sameLoc(Loc loc)
    {
        TileLoc comparable = loc as TileLoc;
        if (comparable == null) return false;
        return getLW() == comparable.getLW();
    }
    public override Pos2 toPos()
    {  // TODO URGENT
        Vector2 v = new Vector2(getL(), getW());
        v *= 1f;
        return new Pos2(v);
    }

    // statics


    // normals
    public int distance(TileLoc loc) { return difference(loc).getLength(); }
    public int getLength() { return Mathf.Abs(getL()) + Mathf.Abs(getW()); }
    public TileLoc difference(TileLoc loc) { return new TileLoc(lw - loc.getLW()); }
    public Tiile getNeigTiile()
    {
        TileLooc looc = getNeigLooc();
        return looc.toValidTiile();
    }
    public bool isNeigOfTileLoc(TileLoc tl)
    {
        return distance(tl) == 1;
        // return getNeigLooc().contains(tl); BAD
    }
    public TileLooc getNeigLooc()
    {
        TileLooc looc = new TileLooc();
        looc.add(north());
        looc.add(east());
        looc.add(south());
        looc.add(west());
        return looc;
    }
    public TileLoc north() { return new TileLoc(getL() + 1, getW()); }
    public TileLoc east() { return new TileLoc(getL(), getW() + 1); }
    public TileLoc south() { return new TileLoc(getL() - 1, getW()); }
    public TileLoc west() { return new TileLoc(getL(), getW() - 1); }
    public int getL() { return lw.x; }
    public int getW() { return lw.y; }
    public Vector2Int getLW() { return lw; }
    public Tile findTile()
    {
        return GameMaster.sfindTile(this);
    }
}
