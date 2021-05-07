using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTiile : Tiile
{  // Variant of Tiile, contains all in scene
    private Grid grid;

    public AllTiile(Grid grid) { if (grid == null) Debug.LogError("IllegalArgumentException"); this.grid = grid; }
    public static AllTiile sfind() { return GameMaster.GetGM().grid.getAllTiile(); }  // TODO cleanup
    public static AllTiile genRectTiile(int length, int width, Rd rd)
    {  // dysfunction hihihihi
        GameMaster gm = GameMaster.GetGM();
        Grid grid = gm.grid;
        int tileCount = length * width;
        List<int> playerMap = new Weights(tileCount, new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(1, 1) }, rd).getOutput();

        AllTiile tiile = new AllTiile(grid);
        string write = "";
        for (int w = 0; w < width; w++)
        {
            for (int l = 0; l < length; l++)
            {
                TileLoc tloc = new TileLoc(l, w);
                // TODO randomize gen params
                Tile tile = new Tile(true, tloc);

                int tempId = w * length + l;
                int p = playerMap[tempId];

                tile.setPlayer(gm.getPlayerById(p));
                write += p + ", ";
                
               



                tiile.add(tile);
            }
        }
        Debug.Log(write);
        return tiile;
    }
    public override Tile find(TileLoc tileLoc)
    {
        Tile t = getTile(tileLoc);
        //if (t == null) Debug.LogError("Couldn't find tile in AllTiile");  
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
}
