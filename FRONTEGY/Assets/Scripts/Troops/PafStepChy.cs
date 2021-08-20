using System.Collections.Generic;
using UnityEngine;

public class PafStepChy : Chy
{
    private static Pos3 unselectedPos = new Pos3(0f, 1f / 32f, 0f);
    private static Pos3 selectedPos = new Pos3(0f, 1f / 16f, 0f);
    private static Rot deg90 = Rot.eulerAngles(0f, 90f, 0f);
    private static Rot deg180 = Rot.eulerAngles(0f, 180f, 0f);
    private static Rot deg270 = Rot.eulerAngles(0f, 270f, 0f);

    private Paf _paf;
    private int _step;

    private Tile _previousTile = null;
    private Tile _tile = null;
    private Tile _nextTile = null;

    [SerializeField] private bool _north;
    [SerializeField] private bool _east;
    [SerializeField] private bool _south;
    [SerializeField] private bool _west;

    private bool _isNone;
    private bool _isEnd;
    private bool _isStraight;
    private bool _isTurn;


    public PafStepChy(Paf paf, int step)
    {  // one is created for every step on a paf, every time that paf is edited
        if (paf == null) Debug.LogError("IllegalArgumentException");
        if (step < 0 || step >= paf.count) Debug.LogError("OutOfRangeException");
        _paf = paf;
        _step = step;

        stage();

        if (step >= 1) _previousTile = paf.getTile(step - 1);
        _tile = paf.getTile(step);
        if (step < _paf.count-1) _nextTile = paf.getTile(step + 1);

        if (_previousTile != null) findDirection(_previousTile);
        if (_nextTile != null) findDirection(_nextTile);
        // 0-2 of the cardinal directions are now true.
        // if 0: troop hasn't really planned any paf yet. don't render anything
        // if 1: is either start or end.
        // if 2: is either straight or turn.

        _isNone = _previousTile == null && _nextTile == null;
        if (!_isNone)
        {
            _isEnd = _previousTile == null || _nextTile == null;
            if (!_isEnd)
            {  // Either straight or turn.
                _isStraight = (_north && _south) || (_east && _west);
                _isTurn = !_isStraight;
            }
        }

        // Set correct position
        transive.setParent(_tile.surfaceTranstatic);
        transive.pos3p.set(selectedPos);
        Rot rot = Rot.identity;
        if (_isEnd)
        {
            if (_north) rot = Rot.identity;
            if (_east) rot = deg270;
            if (_south) rot = deg180;
            if (_west) rot = deg90;
        } else if (_isStraight)
        {
            if (_north && _south) rot = Rot.identity;
            if (_east && _west) rot = deg90;
        } else if (_isTurn)
        {
            if (_north && _east) rot = deg270; 
            if (_east && _south) rot = deg180;
            if (_south && _west) rot = deg90;  // CORRECT
            if (_west && _north) rot = Rot.identity; 
        }
        transive.rotp.set(rot);

        getEndGO().SetActive(_isEnd);
        getStraightGO().SetActive(_isStraight);
        getTurnGO().SetActive(_isTurn);
    }

    public void showMark()
    {
        transive.pos3p.set(selectedPos);
        setMat(MatPlace.select, RendPlace.selectable);
        setFloat(RendPlace.selectable, "TimeOffset", _step * -0.3f);
    }
    public void hideMark()
    {
        transive.pos3p.set(unselectedPos);
        setMat(MatPlace.initialSel, RendPlace.selectable);
    }
    private void findDirection(Tile tile) { findDirection(tile.loc); }
    private void findDirection(TileLoc loc)
    {  // Which direction is this, relative to tile?
        TileLoc difference = loc - _tile.loc;
        if (difference.isNorth) _north = true;
        else if (difference.isEast) _east = true;
        else if (difference.isSouth) _south = true;
        else if (difference.isWest) _west = true;
        else Debug.LogError("WHAT");
    }


    private GameObject getEndGO() => getPafStepPhy().endGO;
    private GameObject getStraightGO() => getPafStepPhy().straightGO;
    private GameObject getTurnGO() => getPafStepPhy().turnGO;
    private PafStepPhy getPafStepPhy() => PafStepPool.Instance.getHost(this);

    public override Phy getPhy() => getPafStepPhy();
    public override void stage()
    {
        PafStepPool.Instance.stage(this);
    }

    public override void unstage()
    {
        PafStepPool.Instance.unstage(this);
    }
}
