using System.Collections.Generic;
using UnityEngine;

public class PafStepChy : Chy
{
    private static Pos3 defaultPos = new Pos3(0f, 1f / 32f, 0f);
    private static Pos3 selectedPos = new Pos3(0f, 2f / 32f, 0f);
    private static Pos3 furtherPos = new Pos3(0f, 2f / 32f, 0f);
    private static Pos3 circlePos = new Pos3(0f, 2f / 32f, 0f);
    private static Pos3 backtrackPos = new Pos3(0f, 2f / 32f, 0f);
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
    private bool _isCircle;

    [SerializeField] private bool _isBacktrack;
    [SerializeField] private bool _isFurther;


    public PafStepChy(Paf paf, int step)
    {  // one is created for every step on a paf, every time that paf is edited
        if (paf == null) Debug.LogError("IllegalArgumentException");
        _paf = paf;
        _step = step;
        stage();
    }
    public void createRoadStep()
    {
        if (_step < 0 || _step >= _paf.count) Debug.LogError("OutOfRangeException");
        if (_step >= 1) _previousTile = _paf.getTile(_step - 1);
        _tile = _paf.getTile(_step);
        if (_step < _paf.count - 1) _nextTile = _paf.getTile(_step + 1);

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
        display();
        showMark();
    }
    public void createFurtherPreview(Breadcrumb from, Breadcrumb to)
    {
        _isEnd = true;
        _isFurther = true;
        _tile = from.tile;
        findDirection(to.tile);
        display();
        showMark();
    }
    public void createBacktrackPreview(Breadcrumb from, Breadcrumb to)
    {
        _isEnd = true;
        _isBacktrack = true;
        _tile = from.tile;
        findDirection(to.tile);
        display();
        showMark();
    }
    public void createCircle()
    {
        _isCircle = true;
        _tile = _paf.getTile(_step);
        display();
        showMark();
    }
    public void showMark()
    {
        if (_isCircle)
        {
            transive.pos3p.set(circlePos);
            setMat("selectable", "circle");
            setFloat("selectable", "TimeOffset", _step * -0.3f);
        } else if (_isFurther)
        {
            //Debug.Log(_tile + " is further");
            transive.pos3p.set(furtherPos);
            setMat("selectable", "further");
            setFloat("selectable", "TimeOffset", _step * -0.3f);
        } else if (_isBacktrack)
        {
            //Debug.Log(_tile + " is backtrack");
            transive.pos3p.set(backtrackPos);
            setMat("selectable", "backtrack");
            setFloat("selectable", "TimeOffset", _step * -0.3f);
        } else
        {
            transive.pos3p.set(selectedPos);
            setMat("selectable", "showroad");
            setFloat("selectable", "TimeOffset", _step * -0.3f);
        }
    }
    public void hideMark()
    {
        transive.pos3p.set(defaultPos);
        setMat("selectable", "initial");
    }



    private GameObject getEndGO() => getPafStepPhy().endGO;
    private GameObject getTurnGO() => getPafStepPhy().turnGO;
    private GameObject getCircleGO() => getPafStepPhy().circleGO;
    private GameObject getStraightGO() => getPafStepPhy().straightGO;
    private PafStepPhy getPafStepPhy() => PafStepPool.Instance.getHost(this);

    
    



    private void display()
    {
        transive.setParent(_tile.surfaceTranstatic);
        transive.pos3p.set(selectedPos);
        Rot rot = Rot.identity;
        if (_isEnd)
        {
            if (_north) rot = Rot.identity;
            if (_east) rot = deg270;
            if (_south) rot = deg180;
            if (_west) rot = deg90;
        }
        else if (_isStraight)
        {
            if (_north && _south) rot = Rot.identity;
            if (_east && _west) rot = deg90;
        }
        else if (_isTurn)
        {
            if (_north && _east) rot = deg270;
            if (_east && _south) rot = deg180;
            if (_south && _west) rot = deg90;  // CORRECT
            if (_west && _north) rot = Rot.identity;
        }
        transive.rotp.set(rot);

        getEndGO().SetActive(_isEnd);
        getTurnGO().SetActive(_isTurn);
        getCircleGO().SetActive(_isCircle);
        getStraightGO().SetActive(_isStraight);
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
