using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paf : Breadcruumb
{
    public Paf() { }
    public Paf(Breadcruumb breadcruumb) { adapt(breadcruumb); }
    public void adapt(Breadcruumb breadcruumb) { setBreadcrumbs(breadcruumb.getBreadcrumbs()); }
    public void validate()
    {
        Debug.LogError("not implemented");
    }
    public Tile lastTile()
    {
        return lastBreadcrumb().getTile();
    }
    public Breadcrumb lastBreadcrumb()
    {
        return GetBreadcrumb(GetBreadcrumbCount() - 1);
    }
    public Tile getTileAt(int pathIndex)
    {
        return GetBreadcrumb(pathIndex).getTile();
    }
    public FromTo getFromTo(int step)
    {
        int fromId = GetIndexInRange(step);
        int toId = GetIndexInRange(step + 1);

        Tile from = getTileAt(fromId);
        Tile to = getTileAt(toId);
        if (!from.isNeigOfTile(to))
        {
            Debug.LogError("TEMP you messed up");
        }
        FromTo ft = new FromTo(from, to);
        return ft;
    }
    public void reverse()
    {  // meaningless to have this as a Breadcruumb method, it has no order
        getBreadcrumbs().Reverse();
    }
}
