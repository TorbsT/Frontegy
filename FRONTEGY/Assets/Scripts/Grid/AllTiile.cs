using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTiile : Tiile
{  // Variant of Tiile, contains all in scene
    private Grid grid;

    public AllTiile(Grid grid, List<Tile> tiles) : base(tiles) { if (grid == null) Debug.LogError("IllegalArgumentException"); this.grid = grid; }
    public static AllTiile sfind() { return GameMaster.GetGM().grid.getAllTiile(); }  // TODO cleanup
    
    public override Tile find(TileLoc tileLoc)
    {
        Tile t = getTile(tileLoc);
        if (t == null) Debug.LogError("Couldn't find tile in AllTiile");  
        // MAY NOT BE FOUND HERE, PATHFINDING IN DJIKSTRA
        return t;
    }
    public void updateVisuals()
    {  // if some tiile should be updated, all should
        foreach (Tile t in getTiles())
        {
            t.updateVisual();
        }
    }
    public Grid getGrid()
    {
        if (grid == null) Debug.LogError("IllegalStateException");
        return grid;
    }
}
