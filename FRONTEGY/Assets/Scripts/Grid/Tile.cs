using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile : SelChy
{
    public static readonly float primaryMarkHeight = 1f;

    public override Player owner { get => _state.owner; }
    public TileState state { get => _state; }
    public TileLoc loc { get { return _loc; } }
    public Transform surfaceTransform { get { return getHost().surfaceTransform; } }
    public Transtatic surfaceTranstatic { get => getHost().surfaceTranstatic; }

    [SerializeField] private TileLoc _loc;
    
    private TileState _state;

    public Tile(TileState state, TileLoc loc)
    {
        _state = state;
        _loc = loc;
        stage();
        initMats();
        transive.pos3p.set(getPos3(), true);
    }

    public Pos3 getPos3()
    {
        return loc.toPos3();
    }
    public bool isNeigOfTile(Tile t) { return isNeigOfTileLoc(t.loc); }
    public bool isNeigOfTileLoc(TileLoc tl) { return TileLoc.areNeigs(loc, tl); }
    public void showPrimaryMark(Breadcrumb bc)
    {
        setMat(MatPlace.mark, RendPlace.top);

        float timeMod = Mathf.Pow(2, bc.stepsRemaining);
        float timeOffset = (float)bc.stepsRemaining * 0.3f;
        setFloat(RendPlace.top, "TimeOffset", timeOffset);
        transive.scalep.set(new Scale(1f, 1f+primaryMarkHeight, 1f));
    }
    public void showSecondaryMark(Breadcrumb bc)
    {
        setMat(MatPlace.secondaryMark, RendPlace.top);

        float timeMod = Mathf.Pow(2, bc.stepsRemaining);
        float timeOffset = (float)bc.stepsRemaining * 0.3f;
        setFloat(RendPlace.top, "TimeOffset", timeOffset);
    }
    public void hidePrimaryMark()
    {
        setMat(owner.getMatPlace(), RendPlace.top);
        transive.scalep.set(Scale.identity());
    }//hehehehe
    public void hideSecondaryMark(Breadcrumb bc) { showPrimaryMark(bc); }// what is wrong with you

    public TilePhy getHost()
    {
        return TilePool.Instance.getHost(this);
    }



    public override Phy getPhy()
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
    public override string ToString()
    {
        return loc.ToString();
    }
}
