﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pathfinding
{
    static List<Breadcrumb> foundBreadcrumbs;
    public static List<Breadcrumb> GetAllTilesInRange(Breadcrumb breadcrumb)
    {
        foundBreadcrumbs = new List<Breadcrumb>();
        SetTilesInRange(breadcrumb, breadcrumb);
        return foundBreadcrumbs;
    }
    static void SetTilesInRange(Breadcrumb breadcrumb, Breadcrumb previousBreadcrumb)
    {
        if (IsAvailableForWalking(breadcrumb, previousBreadcrumb))
        {
            bool bestBreadcrumbSoFar = TryAddBreadcrumb(breadcrumb);
            if (bestBreadcrumbSoFar)  // No point in spreading breadcrumbs multiple times from same tile (when breadcrumb isnt more optimal)!
            {
                List<int> neighborTiles = GetNeighborTiles(breadcrumb);
                foreach (int neighborId in neighborTiles)
                {
                    SetTilesInRange(new Breadcrumb(neighborId, breadcrumb.stepsRemaining - 1, breadcrumb.stepId + 1), breadcrumb);
                }
            }
        }
        return;
    }
    public static List<Breadcrumb> GetBreadcrumbPath(int originTile, int destinationTile)
    {
        Breadcrumb currentBreadcrumb = TryFindBreadcrumb(destinationTile);
        if (currentBreadcrumb.tileId == -1) return null;
        List<Breadcrumb> path = new List<Breadcrumb>();

        while (true)
        {
            path.Add(currentBreadcrumb);
            if (currentBreadcrumb.tileId == originTile) break;

            List<int> neighborTiles = GetNeighborTiles(currentBreadcrumb);
            Breadcrumb bestBreadcrumb = currentBreadcrumb;
            foreach (int neighbor in neighborTiles)  // See which one has the highest stepsRemaining => closer to origin!
            {
                Breadcrumb neighborBreadcrumb = TryFindBreadcrumb(neighbor);
                if (neighborBreadcrumb.tileId == -1) continue;
                if (neighborBreadcrumb.stepsRemaining >= bestBreadcrumb.stepsRemaining) bestBreadcrumb = neighborBreadcrumb;
            }
            if (bestBreadcrumb.tileId == currentBreadcrumb.tileId) { Debug.LogError("This shouldn't happen. Portal involved?"); break; }
            currentBreadcrumb = bestBreadcrumb;
        }
        return path;
    }
    public static List<int> GetNeighborTiles(Breadcrumb breadcrumb)
    {
        List<Vector2Int> offsets = new List<Vector2Int>();
        offsets.Add(Vector2Int.up);
        offsets.Add(Vector2Int.right);
        offsets.Add(Vector2Int.down);
        offsets.Add(Vector2Int.left);

        List<int> tileIdsFound = new List<int>();

        foreach (Vector2Int offset in offsets)
        {
            int nextId = JumpFromTile(breadcrumb.tileId, offset);
            tileIdsFound.Add(nextId);
        }
        return tileIdsFound;
    }
    static Breadcrumb TryFindBreadcrumb(int tileId)  // RETURNS Breadcrumb(-1, -1) IF FAILED!
    {
        // ANALYSIS:
        // s = steps From Origin To Destination
        // f = foundBreadcrumbs
        // function self: called 4*s+1
        // foreach self reps Max: f
        // foreach total reps Max: f*(4*s+1)
        // check Concept/Pathfinding_GetPath().ggb for details
        // (yes, misleading name. deal with it)
        foreach (Breadcrumb breadcrumb in foundBreadcrumbs)
        {
            if (breadcrumb.tileId == tileId) return breadcrumb;
        }
        return new Breadcrumb(-1, -1, -1);
    }
    public static List<Breadcrumb> GetUntardedPath(List<Breadcrumb> path)
    {
        List<Breadcrumb> returnPath = new List<Breadcrumb>();
        for (int i = path.Count-1; i >= 0; i--)
        {
            returnPath.Add(path[i]);
        }
        return returnPath;
    }


    static bool IsAvailableForWalking(Breadcrumb breadcrumb, Breadcrumb previousBreadcrumb)
    {
        Vector2Int gridSize = TileTracker.GetGridSize();
        int tileCount = TileTracker.GetTileCount();
        if (breadcrumb.stepsRemaining < 0) return false;
        if (breadcrumb.tileId < 0) return false;
        if (breadcrumb.tileId > tileCount - 1) return false;
        if (TileTracker.GetTileById(breadcrumb.tileId) == null) return false;  // This is checked twice: One here, and one in TryAddBreadcrumb. Without here, shit goes wild?
        if (CrossesEdge(previousBreadcrumb.tileId, breadcrumb.tileId, gridSize)) return false;
        if (!TileTracker.GetTileById(breadcrumb.tileId).geo.isActive) return false;
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
    static bool TryAddBreadcrumb(Breadcrumb newBreadcrumb)  // returns FALSE if a better breadcrumb existed.
    {
        int oldBreadcrumbId = -1;
        for (int i = 0; i < foundBreadcrumbs.Count; i++)
        {
            Breadcrumb oldBreadcrumb = foundBreadcrumbs[i];
            if (newBreadcrumb.tileId == oldBreadcrumb.tileId)  // Already gone through. To prevent intinite recursion.
            {
                if (newBreadcrumb.stepsRemaining <= oldBreadcrumb.stepsRemaining) return false;  // This new path is less effective and should be ignored
                else oldBreadcrumbId = i;  // New path is faster, removes old path
            }
        }
        foundBreadcrumbs.Add(newBreadcrumb);
        if (oldBreadcrumbId != -1) foundBreadcrumbs.RemoveAt(oldBreadcrumbId);
        
        return true;
    }
    static int JumpFromTile(int startTileId, Vector2Int offset)
    {
        return startTileId + offset[0] + offset[1] * TileTracker.GetGridSize()[0];
    }
}
