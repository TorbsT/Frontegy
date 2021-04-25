using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Troop : Chy  // "Must" be class since SetStats() should be able to modify these values
{
    public Troop(bool instantiate, int _playerId, List<Unit> _units)
    {
        playerId = _playerId;
        units = _units;
        if (instantiate) troopPhy = TroopRoster.sgetUnstagedPhy();
    }
    private int id;
    public int playerId;
    public float scale = 0.5f;
    public Tile parentTile;
    private TroopPhy troopPhy;
    public List<Unit> units;
    private Djikstra djikstra;
    private PafChy pafChy;

    public Player getPlayer()
    {
        return Player.getById(playerId);
    }
    public int GetWalkRange()
    {
        return GetMaxRange();
    }
    public int GetMaxRange()
    {
        int range = 0;
        foreach (Unit unit in units)
        {
            int myRange = unit.myRole.stats.RANGE;
            if (myRange > range) range = myRange;
        }
        return range;
    }
    public FromTo getFromTo(int step) { return getPaf().getFromTo(step); }
    public Paf getPaf() { return getPafChy().getPaf(); }
    public PafChy getPafChy() { return pafChy; }
    public bool noPaf() { return !hasPaf(); }
    public bool hasPaf()
    {
        if (getPafChy() == null) return false;
        if (getPaf() == null) return false;
        return true;
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

        return a.meets(b);
    }
    private bool passOnStep(int step, Troop t)
    {
        FromTo a = getFromTo(step);
        FromTo b = t.getFromTo(step);

        return a.passes(b);
    }
    public void weiterUpdate(WeiterView wv)
    {
        int step = wv.getStep();
        Slid slid = wv.getSlid();

        FromTo ft = getFromTo(step);
    }
    public Tile getParentTile() { if (parentTile == null) Debug.LogError("Should probably not happen"); return parentTile; }
    public void planPafTo(Tile t)
    {
        if (hasPaf()) getPafChy().unstage();
        pafChy = djikstra.getPafTo(t);
        pafChy.stage();
    }
    public void select()
    {
        getDjikstra().showMarks();
    }
    public void unselect()
    {
        getDjikstra().hideMarks();
    }
    public void resetParentTile()
    {
        parentTile = getPaf().lastTile();
        resetDjikstra();
    }
    private void resetDjikstra()
    {
        djikstra = null;
        pafChy = null;
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

    protected override Phy getPhy()
    {
        return troopPhy;
    }
    protected override void connect()
    {
        troopPhy = TroopRoster.sgetUnstagedPhy();
    }
    protected override void disconnect()
    {
        troopPhy = null;
    }
}
