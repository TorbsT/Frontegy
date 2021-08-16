using System;
using System.Collections.Generic;
using UnityEngine;
//using TMPro; please dont use here

[System.Serializable]
public class Card : SelChy  // changed to class since i didn't know why it should be struct
{  // TODO Currently only supports handCard, not upgrades
    public override Player owner { get => _state.owner; }
    public int roleId { get => blueprint.roleId; }
    public Role role { get => blueprint.role; }
    private SummonCardBP blueprint { get => _state.blueprint; }

    public CardState state { get => _state; }
    private CardState _state;

    public Card(CardState state)
    {
        _state = state;
        stage();
        initMats();
    }

    public void display(UIPlace place)
    {
        //getCardPhy().displayFGs(); MAYBUG

        uize();
    }

    public override bool canSecondarySelectOn(SelChy selChy)
    {
        Tile tile = (Tile)selChy;
        if (tile != null)
        {
            if (tile.state.ownerId == state.ownerId) return true;
        }
        return false;
    }
    public override void secondarySelectOn(SelChy selChy)
    {
        Tile tile = (Tile)selChy;
        if (tile != null)
        {
            blueprint.cast(this, tile, CastType.summon);
        }
    }

    public SummonCardBP getBlueprint() { if (blueprint == null) Debug.LogError("IllegalStateException"); return blueprint; }
    public bool canCastOn(Tile tile)
    {
        return tile.owner == owner;
    }
    public CardPhy getCardPhy()
    {
        return CardPool.Instance.getHost(this);
    }

    public override void initMats()
    {
        setMat(blueprint.frontFGMatPlace, RendPlace.frontFG);
        setMat(MatPlace.backFG, RendPlace.backFG);
        setMat(MatPlace.initialSel, RendPlace.selectable);
        setCol(getPlayerMatPlace(), RendPlace.FG);
    }
    protected override MatPlace getInitialSelMat()
    {
        return MatPlace.initialSel;
    }
    public override Phy getPhy()
    {
        return getCardPhy();
    }
    public override void stage()
    {
        CardPool.Instance.stage(this);
    }
    public override void unstage()
    {
        CardPool.Instance.unstage(this);
    }
}
