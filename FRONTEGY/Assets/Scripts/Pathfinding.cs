using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    static List<Breadcrumb> alreadyFoundBreadcrumbs;
    public static List<Breadcrumb> GetAllTilesInRange(Breadcrumb breadcrumb)
    {
        alreadyFoundBreadcrumbs = new List<Breadcrumb>();  // A LITTLE MORE BACK AND ITS FUCXED
        SetTilesInRange(breadcrumb);
        Debug.Log(alreadyFoundBreadcrumbs.Count);
        return alreadyFoundBreadcrumbs;
    }
    static void SetTilesInRange(Breadcrumb breadcrumb)
    {
        if (TileTracker.GetTileById(breadcrumb.tileId) == null) return;  // This is checked twice: One here, and one in TryAddBreadcrumb. Without here, 
        if (breadcrumb.stepsRemaining < 0) return;

        if (IsAvailableForWalking(breadcrumb.tileId))
        {
            bool bestBreadcrumbSoFar = TryAddBreadcrumb(breadcrumb);
            if (bestBreadcrumbSoFar)  // No point in spreading breadcrumbs multiple times from same tile (when breadcrumb isnt more optimal)!
            {
                List<Vector2Int> offsets = new List<Vector2Int>();
                offsets.Add(Vector2Int.up);
                offsets.Add(Vector2Int.right);
                offsets.Add(Vector2Int.down);
                offsets.Add(Vector2Int.left);


                for (int i = 0; i < offsets.Count; i++)
                {
                    Vector2Int offset = offsets[i];
                    int nextId = Foo(breadcrumb.tileId, offset);
                    SetTilesInRange(new Breadcrumb(nextId, breadcrumb.stepsRemaining - 1));
                }
            }
        }

        return;
    }
    public static List<int> GetPaths(int centerTileId, int range, List<Vector2Int> alreadyFoundIds, int tileToFind = -1)
    {
        /*
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
            if (IsAvailableForWalking(centerTileId))
            id = TryAddBreadcrumb(centerTileId, offset, range);
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
        */
        return null;
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
        Vector2Int gridSize = TileTracker.GetGridSize();
        int tileCount = TileTracker.GetTileCount();
        if (centerTileId < 0) return false;
        if (centerTileId > tileCount - 1) return false;
        //if (CrossesEdge(centerTileId, id, gridSize)) return false;
        if (!TileTracker.GetTileById(centerTileId).geo.isActive) return false;
        return true;
    }
    static bool CrossesEdge(int startId, int endId, Vector2Int gridSize)  // true: crosses edge of the map, meaning 1 id apart, but really far apart
    {
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
    static bool TryAddBreadcrumb(Breadcrumb newBreadcrumb)  // returns FALSE if a better breadcrumb existed.
    {
        int oldBreadcrumbId = -1;
        for (int i = 0; i < alreadyFoundBreadcrumbs.Count; i++)
        {
            Breadcrumb oldBreadcrumb = alreadyFoundBreadcrumbs[i];
            if (newBreadcrumb.tileId == oldBreadcrumb.tileId)  // Already gone through. To prevent intinite recursion.
            {
                if (newBreadcrumb.stepsRemaining <= oldBreadcrumb.stepsRemaining) return false;  // This new path is less effective and should be ignored
                else oldBreadcrumbId = i;  // New path is faster, removes old path
            }
        }
        alreadyFoundBreadcrumbs.Add(newBreadcrumb);
        if (oldBreadcrumbId != -1) alreadyFoundBreadcrumbs.RemoveAt(oldBreadcrumbId);
        
        return true;
    }
    static int Foo(int startTileId, Vector2Int offset)
    {
        return startTileId + offset[0] + offset[1] * TileTracker.GetGridSize()[0];
    }
}
