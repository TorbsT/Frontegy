using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class TileTracker
{
    /* ÙNUSED
    static bool isInstantiated;
    static GameMaster gameMaster;
    static Grid grid;

    static Vector2Int gridSize;
    static int tileCount;
    
    static void A()
    {
        if (!isInstantiated) Instantiate();
    }
    static void Instantiate()
    {
        isInstantiated = true;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        grid = gameMaster.grid;
        UpdateGridValues(true);
    }
    public static void UpdateGridValues(bool isBeforeTileInstantiation = false)
    {
        gridSize = grid.gridSize;
        if (isBeforeTileInstantiation) tileCount = grid.gridSize[0] * grid.gridSize[1];
        else tileCount = grid.data.getAllTiile().getCount();
    }
    */
}
