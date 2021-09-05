using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paf
{  // Path was used, sorry
    private static bool showDoublePreview = true;

    public int count { get => _breadcrumbs.Count; }
    public Tile lastTile { get => lastBreadcrumb.tile; }
    public Breadcrumb lastBreadcrumb { get => getBreadcrumb(_breadcrumbs.Count - 1); }
    public Breadcrumb secondLastBreadcrumb { get => getBreadcrumb(_breadcrumbs.Count - 2); }
    public List<Breadcrumb> breadcrumbs { get => _breadcrumbs; }
    public List<Breadcrumb> availableNext { get => _availableNext; }


    private List<Breadcrumb> _breadcrumbs;
    private List<Breadcrumb> _availableNext = new List<Breadcrumb>();
    private List<PafStepChy> _stepChies = new List<PafStepChy>();  // "CHIES" LMAO
    private List<PafStepChy> _previewStepChies = new List<PafStepChy>();


    public Paf(TroopState state)
    {
        if (state == null) Debug.LogError("You fucking orange");
        _breadcrumbs = new List<Breadcrumb> { state.startBreadcrumb };
        computeAvailableNext();
    }
    public Paf(Breadcrumb start)
    {
        _breadcrumbs = new List<Breadcrumb> { start };
        computeAvailableNext();
    }

    public void add(Tile tile)
    {
        bool pred(Breadcrumb bc) => bc.tile.Equals(tile);
        if (_availableNext.Exists(pred))
        {
            add(_availableNext.Find(pred));
        }
        else Debug.LogError("NO");
    }
    public void add(Breadcrumb breadcrumb)
    {
        bool goingBack = false;
        int goingBackToStep = 0;
        for (int i = 0; i < _breadcrumbs.Count; i++)
        {
            Breadcrumb bc = _breadcrumbs[i];
            bool sameTile = bc.tile == breadcrumb.tile;
            bool sameStepsRemaining = bc.stepsRemaining == breadcrumb.stepsRemaining;
            if (sameTile && sameStepsRemaining) { goingBackToStep = i; goingBack = true; break; }
        }

        // if going back, remove all breadcrumbs after this
        if (goingBack)
        {
            while (_breadcrumbs.Count > goingBackToStep+1)
            {
                Debug.Log(_breadcrumbs.Count + " " + goingBackToStep);
                _breadcrumbs.RemoveAt(goingBackToStep + 1);
            }
        } else
        {
            _breadcrumbs.Add(breadcrumb);
        }

        computeAvailableNext();
        showAvailableNext();

        // Reset arrow visuals
        foreach (PafStepChy chy in _stepChies)
        {
            chy.unstage();
        }

        _stepChies = new List<PafStepChy>();
        
        int index = 0;
        for (index = 0; index < _breadcrumbs.Count; index++)
        {
            PafStepChy chy = new PafStepChy(this, index);
            chy.createRoadStep();
            _stepChies.Add(chy);
        }
    }
    public void showMarks()
    {
        foreach (Breadcrumb bc in _availableNext)
        {
            bc.showSecondaryMark();
        }
        // Show marks on road
        foreach (PafStepChy chy in _stepChies)
        {
            chy.showMark();
        }
        showAvailableNext();
    }
    public void hideMarks()
    {
        foreach (PafStepChy chy in _stepChies)
        {
            chy.hideMark();
        }
        foreach (PafStepChy chy in _previewStepChies)
        {
            //chy.hideMark();
            chy.unstage();
        }
        foreach (Breadcrumb bc in _availableNext)
        {
            bc.hideSecondaryMark();
        }
    }
    public void unstageAll()
    {
        foreach (PafStepChy chy in _stepChies)
        {
            chy.unstage();
        }
        foreach (PafStepChy chy in _previewStepChies)
        {
            if (chy.staged) chy.unstage();
        }
    }
    private void computeAvailableNext()
    {
        _availableNext = new List<Breadcrumb>();
        if (_breadcrumbs.Count >= 2)
        {  // Add backtracking as an option
            _availableNext.Add(secondLastBreadcrumb);
        }

        List<Breadcrumb> neigsOfLast = lastBreadcrumb.getValidNeigBreadcrumbs();
        foreach (Breadcrumb a in neigsOfLast)
        {
            if (_availableNext.Exists(match => match >= a)) continue;
            _availableNext.Add(a);
        }




    }
    private void showAvailableNext()
    {
        // Show visuals
        foreach (PafStepChy chy in _previewStepChies)
        {
            if (chy.staged)
            chy.unstage();
        }
        _previewStepChies = new List<PafStepChy>();
        int index = _breadcrumbs.Count-1;
        foreach (Breadcrumb bc in _availableNext)
        {
            PafStepChy chy1 = new PafStepChy(this, index);
            if (bc.stepsRemaining > lastBreadcrumb.stepsRemaining)
            {  // Backtracking
                chy1.createBacktrackPreview(bc, lastBreadcrumb);
            }
            else
            {  // Further preview
                chy1.createFurtherPreview(bc, lastBreadcrumb);
            }
            _previewStepChies.Add(chy1);

            if (showDoublePreview)
            {
                PafStepChy chy2 = new PafStepChy(this, index);
                if (bc.stepsRemaining > lastBreadcrumb.stepsRemaining)
                {  // Backtracking
                    chy2.createBacktrackPreview(lastBreadcrumb, bc);
                }
                else
                {  // Further preview
                    chy2.createFurtherPreview(lastBreadcrumb, bc);
                }
                _previewStepChies.Add(chy2);
            } 
        }
        PafStepChy circle = new PafStepChy(this, index);
        circle.createCircle();
        _previewStepChies.Add(circle);
    }
    
    public Tile getTile(int step) => getBreadcrumb(step).tile;
    public Breadcrumb getBreadcrumb(int step)
    {
        if (_breadcrumbs.Count == 0) { Debug.LogError("IllegalStateException"); return Breadcrumb.makeInvalid(); }
        if (step < 0) return _breadcrumbs[0];
        if (step >= _breadcrumbs.Count) return _breadcrumbs[_breadcrumbs.Count - 1];
        return _breadcrumbs[step];
    }
    public FromTo getFromTo(int step)
    {
        Tile from = getBreadcrumb(step).tile;
        Tile to = getBreadcrumb(step+1).tile;
        FromTo ft = new FromTo(from, to);
        return ft;
    }

    public bool isValidNext(Tile tile)
    {
        bool valid = _availableNext.Exists(bc => bc.tile.Equals(tile));
        return valid;
    }

    public void reverse()
    {  // meaningless to have this as a Breadcruumb method, it has no order
        _breadcrumbs.Reverse();
    }

    public override string ToString()
    {
        string inPaf = "in paf: [";
        string available = "], available: [";
        foreach (Breadcrumb bc in _breadcrumbs) inPaf += bc;
        foreach (Breadcrumb bc in _availableNext) available += bc;
        return inPaf + available + "]";
    }
}
