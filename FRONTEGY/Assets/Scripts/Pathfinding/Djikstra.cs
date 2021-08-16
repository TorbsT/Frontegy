using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Djikstra
{

    [SerializeReference] private Breadcruumb marked;
    private Breadcrumb _startBreadcrumb;
    
    public Djikstra(Breadcrumb startBreadcrumb)
    {
        _startBreadcrumb = startBreadcrumb;
        compute();
    }
    public bool tileIsInRange(Tile t)
    {
        return getMarked().hasTile(t);
    }
    public Breadcruumb getMarked()
    {  // use as often as you'd like
        if (marked == null) compute();
        if (marked == null) Debug.LogError("Djikstra.compute() didn't do anything...? Should never happen");
        return marked;
    }
    public void showMarks() { Debug.Log("penuadsas"); getMarked().showMarks(); }
    public void hideMarks() { getMarked().hideMarks(); }


    private void compute()
    {  // gets called once trying to get info!
        marked = new Breadcruumb();
        recursiveMarkNeigs(_startBreadcrumb);
    }
    private void recursiveMarkNeigs(Breadcrumb breadcrumb)
    {
        Debug.Log(IsAvailableForWalking(breadcrumb));
        if (IsAvailableForWalking(breadcrumb))
        {
            // Tries to add this breadcrumb to marked list.
            // If the tile already exists there, compare the breadcrumbs and see which has highest stepsRemaining.
            bool bestBreadcrumbSoFar = tryMark(breadcrumb);
            if (bestBreadcrumbSoFar)  
            {
                // Recursively mark neigs
                // No point in spreading breadcrumbs multiple times from same tile (when breadcrumb isn't more optimal)!
                Tiile neigs = breadcrumb.getValidNeigTiile();
                // these neigs are validated.

                // stepsremaining and step for this breadcrumb, not its neigs
                int stepsRemaining = breadcrumb.stepsRemaining;

                Debug.Log(neigs);
                foreach (Tile neig in neigs.getTiles())
                {
                    Breadcrumb neigBc = Breadcrumb.makeNeig(neig, stepsRemaining);
                    Debug.Log(neigBc);
                    recursiveMarkNeigs(neigBc);
                }
            }
        }
        return;
    }
    private bool IsAvailableForWalking(Breadcrumb breadcrumb)
    {
        // add things like tile.isActive and shit
        if (breadcrumb.stepsRemaining < 0) return false;
        if (breadcrumb.getTile() == null) return false;  // This is checked twice: One here, and one in TryAddBreadcrumb. Without here, shit goes wild?
        return true;
    }
    private bool tryMark(Breadcrumb newBreadcrumb)  // returns FALSE if a better breadcrumb existed.
    {
        return marked.tryAdd(newBreadcrumb);
    }
}
