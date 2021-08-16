using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTiile : Tiile
{  // Variant of Tiile, contains all in scene
    public static AllTiile Instance { get; private set; }

    public AllTiile(Grid grid, List<Tile> tiles) : base(tiles) { if (Instance != null) Debug.LogWarning("Instantiated AllTiile twice"); Instance = this; }
    
    public override Tile find(TileLoc tileLoc)
    {
        Tile t = getTile(tileLoc);
        // MAY BE NULL; HANDLE ELSEWHERE
        return t;
    }
}
