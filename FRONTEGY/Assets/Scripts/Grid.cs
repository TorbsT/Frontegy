using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid
{
    [System.Serializable]
    public enum GridConfigs
    {
        none,
        anchored,
        centered,
    }
    public enum TileShape
    {
        none,
        trianglePrism,
        squarePrism,
        hexagonPrism
    }

    [Header("Grid Variables")]
    public TileShape tileShape;
    public GridConfigs gridConfig;
    public Vector2Int gridSize;
    [Range(0.2f, 1.0f)]
    public float tileRatio;
    [Range(0.0f, 1.0f)]
    public float tileGap;
    [Range(0.1f, 10.0f)]
    public float tileSize;

    // ONLY TEMPORARY
    public int startingUnits;

    [Header("Grid Debug")]
    public List<Geo> geoBlueprints;  // duplicate of gridVers.geos - move this in there, or keep as feature and add unitBlueprints?


    public List<Reservoir> reservoirs;
    public List<Tile> tiles;
    public List<UnitScript> unitScripts;
    public List<GridContents> gridVers;  // nextGrid is 0, currentGrid is 1, lastGrid is 2 etc. 


    [System.NonSerialized] public GridPivotConfig currentGrid;
    [System.NonSerialized] public TileShape previousTileShape;

    public List<int> GetActiveTiles(bool useGeoBlueprints)
    {
        List<int> returnTiles = new List<int>();
        foreach (Tile tile in tiles)
        {
            int tileId = tile.initialGeo.id;
            bool isActive;
            if (useGeoBlueprints) isActive = tile.initialGeo.isActive;
            else isActive = tile.geo.isActive;
            if (isActive) returnTiles.Add(tileId);
        }
        return returnTiles;
    }
    public void ResetGeos()
    {
        foreach (Tile tile in tiles)
        {
            tile.geo = tile.initialGeo;
        }
    }
}
