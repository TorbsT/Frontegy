using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadcruumb
{
    private List<Breadcrumb> breadcrumbs;

    public Breadcruumb()
    {
        setBreadcrumbs(new List<Breadcrumb>());
    }
    public Breadcruumb(List<Breadcrumb> breadcrumbs)
    {
        setBreadcrumbs(breadcrumbs);
    }

    public bool hasTile(Tile t)
    {
        return findByTile(t).isValid();
    }
    public Breadcrumb findByTile(Tile t)
    {
        // may produce INVALID breadcrumb
        foreach (Breadcrumb bc in breadcrumbs)
        {
            if (bc.isTile(t)) return bc;
        }
        return Breadcrumb.makeInvalid();
    }
    public bool tryAdd(Breadcrumb newBreadcrumb)  
    {
        // Every breadcrumb in list should have unique tiles.
        // returns FALSE if a better breadcrumb existed.

        // Check if the tile already exists here.
        // Remove that breadcrumb if it exists and is worse.
        for (int i = 0; i < GetBreadcrumbCount(); i++)
        {
            Breadcrumb oldBreadcrumb = GetBreadcrumb(i);
            if (newBreadcrumb.sameTileAs(oldBreadcrumb))
            {
                if (newBreadcrumb > oldBreadcrumb)
                {
                    removeAt(i);
                    break;  // improves performance
                }
                else
                {  // better already exists, fail at adding.
                    return false;
                }
            }
        }

        bruteAdd(newBreadcrumb);
        return true;
    }
    public void bruteAdd(Breadcrumb newBreadcrumb)
    {
        // FASTER THAN tryAdd(),
        // but LESS SECURE.
        // use where YOU KNOW WHAT YOU'RE DOING, and PERFORMANCE IS KEY
        getBreadcrumbs().Add(newBreadcrumb);
    }
    private void removeAt(int index)
    {
        if (IsOutOfRange(index)) Debug.LogError("you messed up. you absolute moron.");
        getBreadcrumbs().RemoveAt(index);
    }
    public Breadcrumb getHighestStepsRemaining()
    {
        int bestScore = -1;
        Breadcrumb best = Breadcrumb.makeInvalid();
        foreach (Breadcrumb b in getBreadcrumbs())
        {
            int score = b.stepsRemaining;
            if (score > bestScore)
            {
                bestScore = score;
                best = b;
            }
        }
        return best;
    }
    public Breadcruumb getNeigsOf(Breadcrumb a)
    {  // gets neigs of a, only for bcs in this breadcruumb
        Breadcruumb breadcruumb = new Breadcruumb();
        foreach (Breadcrumb b in getBreadcrumbs())
        {
            if (b.isNeigOfBC(a))
            {
                breadcruumb.bruteAdd(b);
            }
        }
        return breadcruumb;
    }
    public List<Breadcrumb> getBreadcrumbs() { return breadcrumbs; }
    public void setBreadcrumbs(List<Breadcrumb> breadcrumbs) { this.breadcrumbs = breadcrumbs; }
    public int GetIndexInRange(int index)
    {
        if (IsEmpty()) Debug.LogError("Tried converting potentially out-of-range index to in-range, but there is no path");
        return Mathf.Clamp(index, 0, GetBreadcrumbCount() - 1);
    }
    public bool IsOutOfRange(int index)
    {
        if (index < 0 || index >= GetBreadcrumbCount()) return true;
        return false;
    }
    public bool IsEmpty()
    {
        return GetBreadcrumbCount() == 0;
    }
    public int GetBreadcrumbCount()
    {
        if (getBreadcrumbs() == null) return 0;
        return getBreadcrumbs().Count;
    }
    public Breadcrumb GetBreadcrumb(int index)
    {
        if (IsEmpty()) Debug.LogError("Tried getting a breadcrumb, but there is no path");
        else if (IsOutOfRange(index)) Debug.LogError("Tried getting a breadcrumb, but index is out of range");

        return getBreadcrumbs()[index];
    }
    public Paf makePaf()
    {
        return new Paf(this);
    }
    public void showMarks()
    {
        foreach (Breadcrumb bc in getBreadcrumbs())
        {
            bc.showMark();
        }
    }
    public void hideMarks()
    {
        foreach (Breadcrumb bc in getBreadcrumbs())
        {
            bc.hideMark();
        }
    }
}
