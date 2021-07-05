using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Troop : SelChy  // "Must" be class since SetStats() should be able to modify these values
{
    public Troop(Grid grid, bool instantiate, Player owner, Unit unit, Tile parentTile) : base(grid)
    {
        this.parentTile = parentTile;
        this.owner = owner;
        this.unit = unit;
        stage();
        initMats();
        displayOnParent();
    }
    private int id;
    public float scale = 0.5f;
    public int tiss = 123;
    [SerializeReference] private Tile parentTile;
    private Unit unit;
    [SerializeReference] private Djikstra djikstra;

    public Player getPlayer()
    {
        return getUnit().getPlayer();
    }
    public int GetRange()
    {
        return getUnit().getRANGE();
    }
    public Unit getUnit()
    {
        return unit;
    }
    
    public void tacticalStart()
    {
        Debug.Log("penIS");
        displayOnParent();

    }
    private void displayOnParent()
    {
        Pos3 p3 = parentTile.getPos3();
        Pos3 add = new Pos3(0f, parentTile.getColliderBounds().extents.y + getColliderBounds().extents.y, 0f);
        p3 += add;
        trans.pos3 = p3;
        showTrans();
    }

    public int getId() { return id; }
    public bool isThisTroop(Troop compareAgainst)
    {
        if (compareAgainst == null) return false;  // not possible to compare to nulls... i think
        return (this.id == compareAgainst.getId());
    }
    public Conflict findConflictByStepAndTroop(int step, Troop b)
    {  // MAYBUG doesn't take any consequi into account
        bool meet = meetOnStep(step, b);
        bool pass = passOnStep(step, b);

        if (meet && pass) Debug.LogError("This is a logical error. what the actual fuck?");
        if (meet) return new TileConflict(step, this, b);
        if (pass) return new BorderConflict(step, this, b);
        return null;
    }
    private bool meetOnStep(int step, Troop t)
    {
        FromTo a = getFromTo(step);
        FromTo b = t.getFromTo(step);

        return FromTo.meet(a, b);
    }
    private bool passOnStep(int step, Troop t)
    {
        FromTo a = getFromTo(step);
        FromTo b = t.getFromTo(step);

        return FromTo.pass(a, b);
    }
    public void weiterUpdate(WeiterView wv)
    {
        int step = wv.getStep();
        Slid slid = wv.getSlid();

        FromTo ft = getFromTo(step);
    }
    private FromTo getFromTo(int step)
    {
        return getTroopPlan().getFromTo(step);
    }
    private TroopPlan getTroopPlan()
    {
        return getGrid().getTroopPlan(this);
    }
    public Tile getParentTile() { if (parentTile == null) Debug.LogError("Should probably not happen"); return parentTile; }
    public bool planPafTo(Tile t)
    {
        if (t.Equals(getParentTile()))
        {
            getGrid().removePaf(this);
            return true;
        }

        PafChy pafChy = djikstra.getPafTo(t);
        if (pafChy == null) return false;  // Out of range
        getGrid().planPaf(this, pafChy);  // should always succeed
        return true;
    }
    public override void primarySelect()
    {
        base.primarySelect();
        Debug.Log("penus");
        getDjikstra().showMarks();
    }
    public override void unselect()
    {
        base.unselect();
        getDjikstra().hideMarks();
    }
    public void newRound(Results results)
    {
        // get results.consequence, roundplan.paf
        // if dead: dead = true, unstage, return (not sure what to do with dead troopsyet)
        if (results == null) Debug.LogError("IllegalArgumentException");
        parentTile = results.lastTileInPaf(this);
        resetDjikstra();
    }
    private void resetDjikstra()
    {
        djikstra = null;
    }
    public Djikstra getDjikstra()
    {  // If no exists, generate. use as often as you like
        if (djikstra == null) computeDjikstra();
        if (djikstra == null) Debug.LogError("you messed up you big stupid piece of feces");
        return djikstra;
    }
    public bool tileIsInRange(Tile t)
    {
        return getDjikstra().tileIsInRange(t);
    }



    private void computeDjikstra()
    {
        djikstra = new Djikstra(this);
    }
    public TroopPhy getTroopPhy() { return TroopPool.Instance.getHost(this); }
    public override void initMats()
    {
        setMat(getPlayerMatPlace(), RendPlace.selectable);
    }
    protected override MatPlace getInitialSelMat()
    {
        return getPlayerMatPlace();
    }
    protected override Phy getPhy()
    {
        return getTroopPhy();
    }
    public override void stage()
    {
        TroopPool.Instance.stage(this);
    }
    public override void unstage()
    {
        TroopPool.Instance.unstage(this);
    }
}
