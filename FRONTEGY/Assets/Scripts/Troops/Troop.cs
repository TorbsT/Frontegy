using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Troop : SelChy  // "Must" be class since SetStats() should be able to modify these values
{
    public static int nextId { get; private set; }
    public override Player owner { get => _state.owner; }
    public int id { get => _id; }
    public Paf paf { get => _state.paf; }
    public Djikstra djikstra { get => _state.djikstra; }
    public TroopState state { get => _state; set { _state = value; } }
    public Pos3 heightAdjustment { get => new Pos3(0f, getColliderBounds().extents.y, 0f); }

    
    [SerializeField] private int _id = -1;
    [SerializeReference] private TroopState _state;

    public Troop(TroopState state)
    {
        if (state == null) Debug.LogError("Hell fucking no");
        _state = state;
        _id = nextId;
        nextId++;
        _state.id = _id;

        Grid.Instance.allTroops.Add(this);
        stage();
        initMats();
        transive.scalep.set(new Scale(0.5f, 0.5f, 0.5f));
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
        if (!connected) return;
        Debug.Log("penIS");
        displayOnParent();
    }
    private void displayOnParent()
    {
        transive.setParent(state.parentTile.surfaceTranstatic, true);
        transive.pos3p.set(Pos3.identity, true);
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
    public void weiterViewStart(WeiterView wv)
    {
        if (!connected) return;
        int step = wv.step;
        TroopStepState stepState = _state.stepStates.getStepState(step);
        Debug.Log(stepState);
        Tile tile = stepState.currentBreadcrumb.tile;
        transive.setParent(tile.surfaceTranstatic);
        transive.pos3p.set(Pos3.identity);
        Debug.Log(step + " " + tile);

        if (stepState.dead && connected) TroopPool.Instance.ragdollify(this);
    }
    public void weiterViewUpdate(WeiterView wv)
    {
        if (!connected) return;
        int step = wv.step;
        Slid slid = wv.slid;

        TroopStepStates stepStates = _state.stepStates;
        TroopStepState nowState = stepStates.getStepState(step);
        TroopStepState nextState = stepStates.getStepState(step+1);

        if (nowState.dead) return;

        Tile from = nowState.currentBreadcrumb.tile;
        Tile to = nextState.currentBreadcrumb.tile;

        Pos3 diff = to.transive.pos3p.get(false) - from.transive.pos3p.get(false);
        diff *= slid;
        transive.pos3p.set(diff, true);
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
        setMat("selectable", getPlayerMatPlace());
    }
    protected override string getInitialSelMatPlace()
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
