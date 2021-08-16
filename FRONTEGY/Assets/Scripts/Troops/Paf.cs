using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paf
{
    public static bool infiBacktrack = false;

    public int count { get => _breadcrumbs.Count; }
    public Tile lastTile { get => lastBreadcrumb.tile; }
    public Breadcrumb lastBreadcrumb { get => getBreadcrumb(_breadcrumbs.Count - 1); }
    public Breadcrumb secondLastBreadcrumb { get => getBreadcrumb(_breadcrumbs.Count - 2); }
    public List<Breadcrumb> breadcrumbs { get => _breadcrumbs; }


    private List<Breadcrumb> _breadcrumbs;


    public Paf(Breadcrumb start)
    {
        _breadcrumbs = new List<Breadcrumb> { start };
    }

    public void add(Tile tile)
    {
        if (_breadcrumbs.Count >= 2)
        {
            Predicate<Breadcrumb> pred = bc => bc.tile.Equals(tile);
            if (_breadcrumbs.Exists(pred))
            {
                add(_breadcrumbs.Find(pred));
                return;
            }
        }
        add(new Breadcrumb(tile, lastBreadcrumb.stepsRemaining - 1));
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
            while (_breadcrumbs.Count >= goingBackToStep)
            {
                _breadcrumbs.RemoveAt(goingBackToStep + 1);
            }
        } else
        {
            _breadcrumbs.Add(breadcrumb);
        }
    }
    public List<Breadcrumb> availableNextSteps()
    {
        List<Breadcrumb> available = new List<Breadcrumb>();
        if (infiBacktrack)
        for (int i = 0; i < _breadcrumbs.Count-1; i++)
        {
            available.Add(getBreadcrumb(i));
        }

        List<Breadcrumb> neigsOfLast = lastBreadcrumb.getValidNeigBreadcrumbs();
        foreach (Breadcrumb a in neigsOfLast)
        {
            foreach (Breadcrumb b in available)
            {
                if (a <= b) continue;
                available.Add(a);
            }
        }

        return available;
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

    public bool isValidNext(Tile tile) => availableNextSteps().Find(bc => bc.tile.Equals(tile)) != null;

    public void reverse()
    {  // meaningless to have this as a Breadcruumb method, it has no order
        _breadcrumbs.Reverse();
    }
}
