using System;
using System.Collections.Generic;
using UnityEngine;
//using TMPro; please dont use here

[System.Serializable]
public class Card : SelChy  // changed to class since i didn't know why it should be struct
{  // TODO Currently only supports handCard, not upgrades
    [SerializeReference] private SummonBP blueprint;

    public Card(Grid grid, SummonBP blueprint, Player owner) : base(grid)
    {
        if (blueprint == null) Debug.LogError("IllegalArgumentException");
        this.blueprint = blueprint;
        if (owner == null) Debug.LogError("IllegalArgumentException");
        this.owner = owner;
        stage();
        initMats();
    }

    public void display(UIPlace place)
    {
        //getCardPhy().displayFGs(); MAYBUG
        uize();
    }

    public void castOn(Tile tile, CastType type)  // ownerId difficult when casting from tactical?
    {
        getBlueprint().cast(this, tile, type);
    }

    public SummonBP getBlueprint() { if (blueprint == null) Debug.LogError("IllegalStateException"); return blueprint; }
    public bool canCastOn(Tile tile)
    {
        return tile.owner == owner;
    }
    public CardPhy getCardPhy()
    {
        return CardPool.Instance.getHost(this);
    }
    public Role getRole()
    {
        return blueprint.getRole();
    }

    private MatPlace getRoleMatPlace()
    {
        MatPlace place = getRole().getFrontFGPlace();
        return place;
    }

    public override void initMats()
    {
        setMat(getRoleMatPlace(), RendPlace.frontFG);
        setMat(MatPlace.backFG, RendPlace.backFG);
        setMat(MatPlace.initialSel, RendPlace.selectable);
        setCol(getPlayerMatPlace(), RendPlace.FG);
    }
    protected override MatPlace getInitialSelMat()
    {
        return MatPlace.initialSel;
    }
    protected override Phy getPhy()
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
