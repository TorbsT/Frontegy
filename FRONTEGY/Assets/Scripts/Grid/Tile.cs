using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : Chy
{

    private TilePhy tilePhy;
    private GameMaster gm;
    private TileLoc loc;
    private bool active;
    public float height;
    public Reservoir reservoir;
    public int playerId;

    public Tile(bool instantiate, TileLoc loc)
    {
        this.loc = loc;
        gm = GameMaster.GetGM();
        if (instantiate) tilePhy = TileRoster.sgetUnstagedPhy();
    }


    public Pos2 getPos()
    {
        float x = getLoc().getL();
        float z = getLoc().getW();
        return new Pos2(x, z);
    }
    public bool isActive() { return active; }
    public TileLoc getLoc() { return loc; }
    public bool sameTile(Tile t) { return getLoc().sameLoc(t.getLoc()); }
    private GameMaster getGM() { if (gm == null) Debug.LogError("Should never happen"); return gm; }
    public Tiile getNeigTiile()
    {
        return getLoc().getNeigTiile();
    }
    public bool isNeigOfTile(Tile t) { return isNeigOfTileLoc(t.getLoc()); }
    public bool isNeigOfTileLoc(TileLoc tl) { return getLoc().isNeigOfTileLoc(tl); }
    public void showMark(Breadcrumb bc) { getTilePhy().showMark(bc); }  // OH NO
    public void hideMark() { getTilePhy().hideMark(); }


    private TilePhy getTilePhy()
    {
        if (tilePhy == null) tilePhy = (TilePhy)getPhy();  // OH LORD THIS IS BAD TODO
        return tilePhy;
    }

    protected override Phy getPhy()
    {
        return tilePhy;
    }

    protected override void connect()
    {
        tilePhy = TileRoster.sgetUnstagedPhy();
    }

    protected override void disconnect()
    {
        tilePhy = null;
    }
}
