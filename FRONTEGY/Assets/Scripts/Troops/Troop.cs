using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Troop : SelChy  // "Must" be class since SetStats() should be able to modify these values
{
    public override Player owner { get => _state.owner; }
    public int id { get => _id; }
    public Paf paf { get => _state.paf; }
    public Djikstra djikstra { get => _state.djikstra; }
    public TroopState state { get => _state; set { _state = value; } }

    
    [SerializeField] private int _id;
    [SerializeReference] private TroopState _state;

    public Troop(TroopState state)
    {
        _state = state;
        stage();
        initMats();
        displayOnParent();
    }
    public Player getOwner()
    {
        return state.owner;
    }
    public int GetRange()
    {
        return state.getRANGE();
    }
    public int getPOW()
    {
        return state.getPOW();
    }
    
    public void tacticalStart()
    {
        Debug.Log("penIS");
        displayOnParent();

    }
    private void displayOnParent()
    {
        

        Pos3 p3 = new Pos3(0f, getColliderBounds().extents.y, 0f);

        transive.setParent(state.parentTile.surfaceTranstatic, true);
        transive.pos3p.set(p3, true);
    }
    public bool isThisTroop(Troop compareAgainst)
    {
        if (compareAgainst == null) return false;  // not possible to compare to nulls... i think
        return (this.id == compareAgainst._id);
    }
    /*
    public Conflict findConflictByStepAndTroop(int step, Troop b)
    {  // MAYBUG doesn't take any consequi into account
        bool meet = meetOnStep(step, b);
        bool pass = passOnStep(step, b);

        if (meet && pass) Debug.LogError("This is a logical error. what the actual fuck?");
        if (meet) return new TileConflict(step, this, b);
        if (pass) return new BorderConflict(step, this, b);
        return null;
    }
    */
    public void weiterUpdate(WeiterView wv)
    {
        /*
        int step = wv.getStep();
        Slid slid = wv.getSlid();

        FromTo ft = getFromTo(step);
        */
    }
    public override void primarySelect()
    {
        base.primarySelect();
        Debug.Log("penus");
        djikstra.showMarks();
        paf.showMarks();
    }
    public override void unselect()
    {
        base.unselect();
        paf.hideMarks();
        djikstra.hidePrimaryMarks();
    }
    public override bool canSecondarySelectOn(SelChy selChy)
    {
        if (selChy is Tile t) return paf.isValidNext(t);
        return false;
    }
    public override void secondarySelectOn(SelChy selChy)
    {
        if (selChy is Tile t)
        {
            paf.add(t);
            paf.showMarks();
        }
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
    public override Phy getPhy()
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
