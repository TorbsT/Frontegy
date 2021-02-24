using System.Collections.Generic;
using UnityEngine;

public class Paf
{  // Path was used, sorry
    private List<Breadcrumb> breadcrumbs;

    public Paf(List<Breadcrumb> breadcrumbs)
    {
        ReplaceBreadcrumbs(breadcrumbs);
    }

    public int GetFinalTileId()
    {
        return GetFinalBreadcrumb().GetTileId();
    }
    private Breadcrumb GetFinalBreadcrumb()
    {
        return GetBreadcrumb(GetBreadcrumbCount()-1);
    }
    public int GetTileId(int pathIndex)
    {
        return GetBreadcrumb(pathIndex).GetTileId();
    }
    public Breadcrumb GetBreadcrumb(int index)
    {
        if (IsEmpty()) Debug.LogError("Tried getting a breadcrumb, but there is no path");
        else if (IsOutOfRange(index)) Debug.LogError("Tried getting a breadcrumb, but index is out of range");
        
        return GetBreadcrumbs()[index];
    }
    public int GetIndexInRange(int index)
    {
        if (IsEmpty()) Debug.LogError("Tried converting potentially out-of-range index to in-range, but there is no path");
        return Mathf.Clamp(index, 0, GetBreadcrumbCount()-1);
    }
    public bool IsOutOfRange(int index)
    {
        if (IsEmpty()) return true;
        if (index < 0 || index >= GetBreadcrumbCount()) return true;
        return false;
    }
    public bool IsEmpty()
    {
        return GetBreadcrumbCount() == 0;
    }
    public int GetBreadcrumbCount()
    {
        if (GetBreadcrumbs() == null) return 0;
        return GetBreadcrumbs().Count;
    }
    public void ReplaceBreadcrumbs(List<Breadcrumb> breadcrumbs) { this.breadcrumbs = breadcrumbs; }
    public List<Breadcrumb> GetBreadcrumbs() { return breadcrumbs; }
}
