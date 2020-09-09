using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class TileTracker
{
    static GameMaster GetGameMaster()
    {
        return GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
    public static List<Tile> GetTilesByIds(List<int> ids)
    {
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
        GameMaster gameMaster = GetGameMaster();
        List<Tile> tiles = gameMaster.grid.tiles;
        if (id < 0 || id >= tiles.Count) return null;
        return tiles[id];
    }
    public static List<Tile> GetTilesByPoses(List<Vector2Int> poses)
    {
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
        GameMaster gameMaster = GetGameMaster();
        List<Tile> tiles = gameMaster.grid.tiles;
        foreach (Tile tile in tiles)
        {
            if (tile.geo.gridPos == pos) return tile;
        }
        return null;
    }
}
