using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile : Chy
{

    private TilePhy tilePhy;
    private GameMaster gm;
    [SerializeReference] private TileLoc loc;
    [SerializeField] private bool active;
    private Reservoir reservoir;
    [SerializeReference] private Player player;

    public Tile(Grid grid, bool instantiate, TileLoc loc) : base(grid)
    {
        this.loc = loc;
        gm = GameMaster.GetGM();
        if (instantiate)
        {
            stage();
            activate();
        }
    }

    public void updateVisual()
    {
        getTilePhy().setPos2(getPos());
        getTilePhy().updateVisual();
    }
    public Pos2 getPos()
    {
        return loc.toPos();
    }
    public Reservoir getReservoir() { return reservoir; }
    public bool isActive() { return active; }
    private void activate() { active = true; }
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

    public void setPlayer(Player player)
    {
        this.player = player;
    }
    public Player getPlayer()
    {
        return player;
    }
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
        tilePhy = getGrid().getUnstagedTilePhy();
        if (tilePhy == null) Debug.LogError("IllegalStateException: Tile.connect() failed to set a tilePhy");
    }

    protected override void disconnect()
    {
        tilePhy = null;
    }
}
