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
    public int startingTroops;

    [Header("Grid Debug")]
    public GameMaster gameMaster;
    public List<Geo> geoBlueprints;
    Transform gridGO;


    public List<Reservoir> reservoirs;
    //public List<Tile> tiles;
    //public List<Troop> troopScripts;
    public GridData data = null;


    [System.NonSerialized] public GridPivotConfig currentGrid;
    [System.NonSerialized] public TileShape previousTileShape;

    public void ResetGrid()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        if (data != null)
        {
            // TODO: Destroy gameObjects
        }
        data = new GridData(new List<Tile>(), new List<Troop>());

        if (GameObject.Find("Grid") != null)
            Object.Destroy(gridGO.gameObject);
        gridGO = new GameObject("Grid").transform;

        geoBlueprints = GetRandomizedGeos();
        InstantiateTiles(geoBlueprints);
    }
    void InstantiateTiles(List<Geo> blueprints)
    {
        if (data.tiles != null)
            foreach (Tile tile in data.tiles)
            {
                Object.Destroy(tile.gameObject);
            }
        List<Tile> tempTiles = new List<Tile>();
        foreach (Geo blueprint in blueprints)
        {
            GameObject tileGO = Object.Instantiate(gameMaster.tilePrefab, gridGO);
            Tile createdTile = tileGO.GetComponentInChildren<Tile>();
            createdTile.initialGeo = blueprint;
            createdTile.gameMaster = gameMaster;  // TEMPORARY
            tempTiles.Add(createdTile);
        }
        data.tiles = tempTiles;
        Debug.Log("count: " + data.tiles.Count);
        tempTiles = null;
        Debug.Log("count: " + data.tiles.Count);
        ResetTileGeos();
    }
    public List<int> GetActiveTiles(bool useGeoBlueprints)
    {
        List<int> returnTiles = new List<int>();
        foreach (Tile tile in data.tiles)
        {
            int tileId = tile.initialGeo.id;
            bool isActive;
            if (useGeoBlueprints) isActive = tile.initialGeo.isActive;
            else isActive = tile.geo.isActive;
            if (isActive) returnTiles.Add(tileId);
        }
        return returnTiles;
    }
    List<Geo> GetRandomizedGeos()
    {
        List<Geo> blueprintGeos = new List<Geo>();
        int remainingSpaces = TileTracker.GetTileCount();
        int remainingTiles = Mathf.RoundToInt(tileRatio * TileTracker.GetTileCount());

        int gridXPos = 0;
        int gridYPos = 0;

        List<int> reservoirSpawnWeights = new List<int>();
        foreach (Reservoir res in reservoirs)
        {
            reservoirSpawnWeights.Add(res.spawnWeight);
        }


        for (int i = 0; i < TileTracker.GetTileCount(); i++)
        {
            float prob = ((float)remainingTiles) / ((float)remainingSpaces);

            Geo blueprintGeo = new Geo();
            float thisRandomValue = Random.value;
            if (prob >= thisRandomValue)
            {
                blueprintGeo.isActive = true;
                remainingTiles--;
            }
            else blueprintGeo.isActive = false;


            blueprintGeo.reservoir = DecideReservoir(reservoirSpawnWeights);

            if (0.5f >= Random.value) blueprintGeo.playerId = 0;
            else blueprintGeo.playerId = 1;


            remainingSpaces--;
            blueprintGeo.height = 1f + Mathf.Round(Random.value * 10f) / 10f;
            blueprintGeo.id = i;
            blueprintGeo.gridPos = new Vector2Int(gridXPos, gridYPos);
            blueprintGeos.Add(blueprintGeo);

            gridXPos++;
            if (gridXPos >= gridSize[0])
            {
                gridXPos = 0;
                gridYPos++;
            }
        }
        return blueprintGeos;
    }
    Reservoir DecideReservoir(List<int> weights, bool debug = false)
    {
        int resWeightsSoFar = 0;
        float randomFloat = Random.value;
        int reservoirValue = Mathf.FloorToInt(randomFloat * Maffs.GetSumOfWeights(weights));
        if (debug) Debug.Log(randomFloat + " " + reservoirValue);
        for (int i = 0; i < weights.Count; i++)
        {
            int sW = weights[i] + resWeightsSoFar;
            if (debug) Debug.Log(sW + " " + weights[i] + " " + resWeightsSoFar);
            if (sW > reservoirValue)
            {
                if (debug) Debug.Log("true");
                return reservoirs[i];
            }
            if (debug) Debug.Log("false");
            resWeightsSoFar += weights[i];
        }
        Debug.LogError("FATAL ERROR ON GETTING RESERVOIR");
        return reservoirs[0];
    }
    public void ResetTileGeos()
    {
        foreach (Tile tile in data.tiles)
        {
            tile.geo = tile.initialGeo;
        }
    }
}
