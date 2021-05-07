using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Djikstra
{
    private Breadcruumb marked;
    private Tile startTile;
    private int startRange;
    
    public Djikstra(Troop t)
    {
        startTile = t.getParentTile();
        startRange = t.GetRange();
    }
    public Djikstra(Tile startTile, int startRange)
    {
        this.startTile = startTile;
        this.startRange = startRange;
    }
    public PafChy getPafTo(Tile destination)
    {  // generates new paf, don't use too often
        Breadcruumb m = getMarked();
        Breadcrumb currentBreadcrumb = m.findByTile(destination);

        Paf paf = new Paf();
        while (true)
        {
            if (currentBreadcrumb.isInvalid()) return null;  // user clicks outside of green area
            paf.bruteAdd(currentBreadcrumb);  // bruteAdd because performance is key here
            if (currentBreadcrumb.isTile(startTile)) break;
            currentBreadcrumb = m.getNeigsOf(currentBreadcrumb).getHighestStepsRemaining();
        }

        paf.reverse();  // The way this is implemented, the paf starts off in wrong order
        PafChy pafChy = new PafChy(paf);
        return pafChy;
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
    public void showMarks() { getMarked().showMarks(); }
    public void hideMarks() { getMarked().hideMarks(); }


    private void compute()
    {  // gets called once trying to get info!
        marked = new Breadcruumb();
        Breadcrumb startBC = Breadcrumb.makeStarter(startTile, startRange);
        marked.tryAdd(startBC);
        recursiveMarkNeigs(startBC);
    }
    private void recursiveMarkNeigs(Breadcrumb breadcrumb)
    {
        if (IsAvailableForWalking(breadcrumb))
        {
            // is this breadcrumb better (shorter path) to this tile than any other?
            bool bestBreadcrumbSoFar = tryMark(breadcrumb);
            if (bestBreadcrumbSoFar)  
            {
                // If found the best path to this tile, recursively call neigs
                // No point in spreading breadcrumbs multiple times from same tile (when breadcrumb isnt more optimal)!
                Tiile neigs = breadcrumb.getNeigTiile();
                // these neigs are validated.

                // stepsremaining and step for this breadcrumb, not its neigs
                int stepsRemaining = breadcrumb.GetStepsRemaining();
                int step = breadcrumb.getStep();

                foreach (Tile neig in neigs.getTiles())
                {
                    Breadcrumb neigBc = Breadcrumb.makeNeig(neig, stepsRemaining, step);
                    recursiveMarkNeigs(neigBc);
                }
            }
        }
        return;
    }
    private bool IsAvailableForWalking(Breadcrumb breadcrumb)
    {
        // add things like tile.isActive and shit
        if (breadcrumb.GetStepsRemaining() < 0) return false;
        if (breadcrumb.getTile() == null) return false;  // This is checked twice: One here, and one in TryAddBreadcrumb. Without here, shit goes wild?
        return true;
    }
    private bool tryMark(Breadcrumb newBreadcrumb)  // returns FALSE if a better breadcrumb existed.
    {
        return marked.tryAdd(newBreadcrumb);
    }
}
