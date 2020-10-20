using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class TileTracker
{
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
        else tileCount = grid.data.tiles.Count;
    }


    public static Vector2Int GetGridSize() { A(); return gridSize; }
    public static int GetTileCount() { A(); return tileCount; }
    public static Vector2Int GetPosById(int id)
    {
        A();

        if (id < 0 || id > tileCount) return new Vector2Int(-1, -1);

        Vector2Int pos = new Vector2Int(0, 0);
        pos.y = Mathf.FloorToInt(((float)id) / ((float)gridSize[0]));
        pos.x = id - gridSize[0] * pos.y;
        return pos;
    }
    public static List<Tile> GetTilesByIds(List<int> ids)
    {
        A();
        List<Tile> tiles = new List<Tile>();
        foreach (int id in ids)
        {
            Tile tile = GetTileById(id);
            if (tile != null) tiles.Add(tile);
        }
        return tiles;
    }
    public static Tile GetTileById(int id)
    {
        A();
        List<Tile> tiles = grid.data.tiles;
        if (id < 0 || id >= tiles.Count) return null;
        return tiles[id];
    }
    public static List<Tile> GetTilesByPoses(List<Vector2Int> poses)
    {
        A();
        List<Tile> tiles = new List<Tile>();
        foreach (Vector2Int pos in poses)
        {
            Tile tile = GetTileByPos(pos);
            if (tile != null) tiles.Add(tile);
        }
        return tiles;
    }
    public static Tile GetTileByPos(Vector2Int pos)
    {
        A();
        List<Tile> tiles = grid.data.tiles;
        foreach (Tile tile in tiles)
        {
            if (tile.geo.gridPos == pos) return tile;
        }
        return null;
    }
}
