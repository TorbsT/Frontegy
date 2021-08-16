using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopState
{
    public Player owner { get => _owner; set { _owner = value; _ownerId = value.id; } }
    public int ownerId { get => _ownerId; set { _ownerId = value; _owner = Playyer.Instance.getPlayer(value); } }
    public TileLoc parentTileLoc { get => _parentTileLoc; set { _parentTileLoc = value; _parentTile = AllTiile.Instance.find(value); } }
    public Tile parentTile { get => _parentTile; set { _parentTile = value; _parentTileLoc = value.loc; } }
    public bool dead { get => _dead; }
    public Role role { get => _role; set { _role = value; _roleId = value.id; } }
    public int roleId { get => _roleId; set { _roleId = value; _role = Roole.Instance.getRole(value); } }
    public int id { get => _id; set { _id = value; } }
    public int roundId { get => _roundId; set { _roundId = value; } }
    public Djikstra djikstra { get => _djikstra; set { _djikstra = value; } }
    public TroopStepStates stepStates { get => _stepStates; set { _stepStates = value; } }
    public Paf paf { get => _paf; set { _paf = value; } }

    private Player _owner;
    private int _ownerId;
    private TileLoc _parentTileLoc;
    private Tile _parentTile;
    private bool _dead;
    private Role _role;
    private int _roleId;
    private int _id;
    private int _roundId;
    private Djikstra _djikstra;
    private TroopStepStates _stepStates;
    private Paf _paf;
    //List<Mods> mods;

    public TroopState()
    {
    }
    public TroopState copy()
    {
        TroopState t = new TroopState();
        t.owner = _owner;
        t.parentTile = _parentTile;
        t._dead = _dead;
        t.role = _role;
        t._id = _id;
        t._roundId = _roundId;
        t._djikstra = null;
        t._stepStates = null;
        t._paf = null;
        return t;
    }

    public static int defaultTroopComparison(TroopState a, TroopState b)
    {
        if (a._role.trumps(b._role)) return 100;
        if (b.role.trumps(a._role)) return -100;
        return a.getPOW() - b.getPOW();
    }
    public int getPOW()
    {
        return _role.baseStats.getPOW()+0;
    }
    public int getRANGE()
    {
        return _role.baseStats.getRANGE();
    }
    public bool planPafTo(Tile tile)
    {
        if (!djikstra.tileIsInRange(tile)) return false;
        if (!paf.isValidNext(tile)) return false;
        paf.add(tile);
        return true;
    }
    public void prepareStepState(int step)
    {
        if (_stepStates == null) _stepStates = new TroopStepStates(this);
        _stepStates.prepareStep(step);
    }
}
