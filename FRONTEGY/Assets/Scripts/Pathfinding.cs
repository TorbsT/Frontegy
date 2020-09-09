using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    static int id;
    public static List<int> GetTilesInRange(int centerTileId, int range, List<Vector2Int> alreadyFoundIds)
    {
        if (TileTracker.GetTileById(centerTileId) == null) return new List<int>();
        if (range < 0) return new List<int>();
        alreadyFoundIds.Add(new Vector2Int(centerTileId, range));
        List<Vector2Int> offsets = new List<Vector2Int>();
        offsets.Add(Vector2Int.up);
        offsets.Add(Vector2Int.right);
        offsets.Add(Vector2Int.down);
        offsets.Add(Vector2Int.left);

        List<int> addThese = new List<int>();
        addThese.Add(centerTileId);
        for (int i = 0; i < offsets.Count; i++)
        {
            List<int> temp = new List<int>();

            Vector2Int offset = offsets[i];
            id = GetTileId(centerTileId, offset, alreadyFoundIds, range);
            if (IsAvailableForWalking(centerTileId))
            {
                temp = GetTilesInRange(id, range - 1, alreadyFoundIds);
                addThese.AddRange(temp);
            }
        }

        return addThese;
    }
    public static List<int> GetPaths(int centerTileId, int range, List<Vector2Int> alreadyFoundIds, int tileToFind = -1)
    {
        if (TileTracker.GetTileById(centerTileId) == null) return new List<int>();
        if (range < 0) return new List<int>();
        alreadyFoundIds.Add(new Vector2Int(centerTileId, range));  // maybe tile.id fixes
        List<Vector2Int> offsets = new List<Vector2Int>();
        offsets.Add(Vector2Int.up);
        offsets.Add(Vector2Int.right);
        offsets.Add(Vector2Int.down);
        offsets.Add(Vector2Int.left);

        List<int> addThese = new List<int>();
        
        for (int i = 0; i < offsets.Count; i++)
        {
            List<int> temp = new List<int>();

            Vector2Int offset = offsets[i];
            id = GetTileId(centerTileId, offset, alreadyFoundIds, range);
            if (IsAvailableForWalking(centerTileId))
            {
                temp = GetPaths(id, range - 1, alreadyFoundIds, tileToFind);
                if (temp.Count > 0)
                {
                    //Debug.Log("Hell yeah: id == " + id + " and temp.Count == " + temp.Count);
                    addThese.AddRange(temp);
                }
            }
        }
        if (addThese.Count > 0 || centerTileId == tileToFind) addThese.Add(centerTileId);
        return addThese;
    }
    public static List<int> GetUntardedPath(List<int> pathIdList, int targetTileId)
    {
        List<int> returnPath = new List<int>();
        for (int i = pathIdList.Count-1; i >= 0; i--)
        {
            returnPath.Add(pathIdList[i]);
            if (pathIdList[i] == targetTileId) break; 
        }
        return returnPath;
    }





    static bool IsAvailableForWalking(int centerTileId)
    {
        if (id == -1) return false;
        if (CrossesEdge(centerTileId, id)) return false;
        return true;
    }
    static bool CrossesEdge(int startId, int endId)  // true: crosses edge of the map, meaning
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
