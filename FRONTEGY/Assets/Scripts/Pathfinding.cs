using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    static int id;
    public static List<Tile> GetTilesInRange(Tile centerTile, int range, List<Vector2Int> alreadyFoundIds)
    {
        if (centerTile == null) return new List<Tile>();
        if (range < 0) return new List<Tile>();
        alreadyFoundIds.Add(new Vector2Int(centerTile.geo.id, range));
        List<Vector2Int> offsets = new List<Vector2Int>();
        offsets.Add(Vector2Int.up);
        offsets.Add(Vector2Int.right);
        offsets.Add(Vector2Int.down);
        offsets.Add(Vector2Int.left);

        List<Tile> addThese = new List<Tile>();
        addThese.Add(FindTile(centerTile.geo.id));
        for (int i = 0; i < offsets.Count; i++)
        {
            List<Tile> temp = new List<Tile>();

            Vector2Int offset = offsets[i];
            id = GetTileId(centerTile.geo.id, offset, alreadyFoundIds, range);
            if (IsAvailableForWalking(centerTile))
            {
                temp = GetTilesInRange(FindTile(id), range - 1, alreadyFoundIds);
                addThese.AddRange(temp);
            }
        }

        return addThese;
    }
    public static List<Tile> GetPaths(Tile centerTile, int range, List<Vector2Int> alreadyFoundIds, int tileToFind = -1)
    {
        if (centerTile == null) return new List<Tile>();
        if (range < 0) return new List<Tile>();
        alreadyFoundIds.Add(new Vector2Int(centerTile.geo.id, range));
        List<Vector2Int> offsets = new List<Vector2Int>();
        offsets.Add(Vector2Int.up);
        offsets.Add(Vector2Int.right);
        offsets.Add(Vector2Int.down);
        offsets.Add(Vector2Int.left);

        List<Tile> addThese = new List<Tile>();
        
        for (int i = 0; i < offsets.Count; i++)
        {
            List<Tile> temp = new List<Tile>();

            Vector2Int offset = offsets[i];
            id = GetTileId(centerTile.geo.id, offset, alreadyFoundIds, range);
            if (IsAvailableForWalking(centerTile))
            {
                temp = GetPaths(FindTile(id), range - 1, alreadyFoundIds, tileToFind);
                if (temp.Count > 0)
                {
                    //Debug.Log("Hell yeah: id == " + id + " and temp.Count == " + temp.Count);
                    addThese.AddRange(temp);
                }
            }
        }
        if (addThese.Count > 0 || centerTile.geo.id == tileToFind) addThese.Add(FindTile(centerTile.geo.id));
        return addThese;
    }
    public static List<Tile> GetUntardedPath(List<Tile> list, Tile targetTile)
    {
        List<Tile> returnPath = new List<Tile>();
        for (int i = list.Count-1; i >= 0; i--)
        {
            returnPath.Add(list[i]);
            if (list[i] == targetTile) break; 
        }
        return returnPath;
    }





    static bool IsAvailableForWalking(Tile centerTile)
    {
        if (id == -1) return false;
        if (CrossesEdge(centerTile.geo.id, id)) return false;
        return true;
    }
    static bool CrossesEdge(int startId, int endId)  // true: crosses edge
    {
        Vector2Int gridSize = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().grid.gridSize;
        float verticalStartSpot = ((float)startId) / ((float)gridSize[0]);
        float verticalEndSpot = ((float)endId) / ((float)gridSize[0]);

        // case1 NOR case2 returns true
        bool case1 = (Mathf.Floor(verticalStartSpot) == Mathf.Floor(verticalEndSpot));
        bool case2 = (Maffs.Divisible(endId-startId, gridSize[0]));
        return !(case1 || case2);
    }
    static Tile FindTile(int id)
    {
        List<Tile> tiles = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().grid.tiles;
        foreach (Tile tile in tiles)
        {
            if (tile.geo.id == id && tile.geo.isActive) return tile;
        }
        return null;
    }
    static int GetTileId(int startTileId, Vector2Int offset, List<Vector2Int> alreadyFoundIds, int thisRange = 0)
    {
        int returnId = startTileId + offset[0] + offset[1] * GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>().grid.gridSize[0];
        foreach (Vector2Int id in alreadyFoundIds)
        {
            if (id[0] == returnId && thisRange <= id[1]) return -1;  // Already gone through. To prevent intinite recursion.
        }
        return returnId;
    }
}
