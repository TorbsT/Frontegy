using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile : SelChy
{
    private GameMaster gm;
    [SerializeReference] private TileLoc loc;
    [SerializeField] private bool active;
    private Reservoir reservoir;

    public Tile(Grid grid, bool instantiate, TileLoc loc, Player owner) : base(grid)
    {
        this.loc = loc;
        this.owner = owner;
        gm = GameMaster.GetGM();
        if (instantiate)
        {
            stage();
        }
        initMats();
    }

    public void updateVisual()
    {
        trans.pos3 = getPos3();
        showTrans();
    }
    public Pos3 getPos3()
    {
        return loc.toPos3();
    }
    public Reservoir getReservoir() { return reservoir; }
    public bool isActive() { return active; }
    private void activate() { active = true; }
    public TileLoc getLoc() { return loc; }
    private GameMaster getGM() { if (gm == null) Debug.LogError("Should never happen"); return gm; }
    public Tiile getNeigTiile()
    {
        return getLoc().getValidNeigTiile();
    }
    public bool isNeigOfTile(Tile t) { return isNeigOfTileLoc(t.getLoc()); }
    public bool isNeigOfTileLoc(TileLoc tl) { return TileLoc.areNeigs(getLoc(), tl); }
    public void showMark(Breadcrumb bc)
    {
        float timeOffset = (float)bc.stepsRemaining * 0.1f;
        setFloat(RendPlace.top, "TimeOffset", timeOffset);  // REMEMBER TO ALSO CHANGE MAT BEFORE THIS, IF IT DOESN'T WORK
    }
    public void hideMark() { setMat(owner.getMatPlace(), RendPlace.top); }//hehehehe

    public TilePhy getHost()
    {
        return TilePool.Instance.getHost(this);
    }





    protected override Phy getPhy()
    {
        return getHost();
    }
    public override void stage()
    {
        TilePool.Instance.stage(this);
    }
    public override void unstage()
    {
        TilePool.Instance.unstage(this);
    }
    protected override MatPlace getInitialSelMat()
    {
        return getPlayerMatPlace();
    }
    public override void initMats()
    {
        setMat(getPlayerMatPlace(), RendPlace.top);
        setMat(getInitialSelMat(), RendPlace.selectable);
    }


    public override bool Equals(object obj)
    {
        if (!(obj is Tile)) return false;
        Tile t = (Tile)obj;
        return t.loc == loc;
    }
    public override int GetHashCode()
    {
        return loc.GetHashCode();
    }
}
