using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTiile : Tiile
{  // Variant of Tiile, contains all in scene
    public AllTiile() { }
    public static AllTiile sfind() { return GameMaster.GetGM().grid.data.getAllTiile(); }  // TODO cleanup
    public static AllTiile genRectTiile(int length, int width)
    {  // dysfunction hihihihi
        AllTiile tiile = new AllTiile();
        for (int w = 0; w < width; w++)
        {
            for (int l = 0; l < length; l++)
            {
                TileLoc tloc = new TileLoc(l, w);
                // TODO randomize gen params
                Tile tile = new Tile(true, tloc);
                tiile.add(tile);
            }
        }
        return tiile;
    }
    public override Tile find(TileLoc tileLoc)
    {
        Tile t = getTile(tileLoc);
        //if (t == null) Debug.LogError("Couldn't find tile in AllTiile");  
        // MAY NOT BE FOUND HERE, PATHFINDING IN DJIKSTRA
        return t;
    }
}
