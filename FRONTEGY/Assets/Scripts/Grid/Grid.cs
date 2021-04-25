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
    public Transform gridGO;

    public List<Reservoir> reservoirs;
    //public List<Tile> tiles;
    //public List<Troop> troopScripts;
    public GridData data = null;


    [System.NonSerialized] public GridPivotConfig currentGrid;
    [System.NonSerialized] public TileShape previousTileShape;

    public void ResetGrid()
    {
        gameMaster = GameMaster.GetGM();
        if (data != null)
        {
            Debug.Log("TODO: Destroy gameObjects");
        }
        AllTiile allTiile = AllTiile.genRectTiile(16, 8);
        Groop groop = new Groop();
        AllCaard allCaard = new AllCaard();
        data = new GridData(allTiile, groop, allCaard);

        if (GameObject.Find("Grid") != null)
            Object.Destroy(gridGO.gameObject);
        gridGO = new GameObject("Grid").transform;

        //gameMaster.StartingCards();
    }
    public void UpdateTroopTiles()
    {
        data.getGroop().resetParentTiles();
    }
}
