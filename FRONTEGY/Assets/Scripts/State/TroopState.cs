﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TroopState
{
    public Player owner { get => _owner; set { _owner = value; _ownerId = value.id; } }
    public int ownerId { get => _ownerId; set { _ownerId = value; _owner = Playyer.Instance.getPlayer(value); } }
    public TileLoc parentTileLoc { get => _parentTileLoc; set { _parentTileLoc = value; _parentTile = AllTiile.Instance.find(value); } }
    public Tile parentTile { get => _parentTile; set { _parentTile = value; _parentTileLoc = value.loc; } }
    public bool dead { get => _dead; set { _dead = value; } }
    public Role role { get => _role; set { _role = value; _roleId = value.id; } }
    public int roleId { get => _roleId; set { _roleId = value; _role = Roole.Instance.getRole(value); } }
    public int id { get => _id; set { _id = value; } }
    public int roundId { get => _roundId; set { _roundId = value; } }
    public Djikstra djikstra { get => _djikstra; set { _djikstra = value; } }
    public TroopStepStates stepStates { get => _stepStates; set { _stepStates = value; } }
    public Paf paf { get => _paf; set { _paf = value; } }
    public Breadcrumb startBreadcrumb { get => new Breadcrumb(_parentTile, getRANGE()); }

    private Player _owner;
    private int _ownerId;
    private TileLoc _parentTileLoc;
    private Tile _parentTile;
    private bool _dead;
    [SerializeReference] private Role _role;
    private int _roleId = -1;
    private int _id = -1;
    private int _roundId = -1;
    private Djikstra _djikstra;
    private TroopStepStates _stepStates;
    private Paf _paf;
    //List<Mods> mods;

    public TroopState(Tile parentTile, Role role)
    {
        if (parentTile == null) Debug.LogError("You monkey");
        if (role == null) Debug.LogError("stupod");
        _parentTile = parentTile;
        this.role = role;
        _djikstra = new Djikstra(this);
        _paf = new Paf(this);
        _stepStates = new TroopStepStates(this);
        Grid.Instance.troopStates.Add(this);
        roundId = RoundManager.Instance.roundId;
        Debug.Log("Created a new TroopState with roundId " + roundId);
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
        _stepStates.prepareStep(step);
    }

    public override string ToString() => "TroopState{id "+id+" round"+roundId + " owner"+ownerId+"}";
}
