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
    private AllTiile allTiile;
    private AllCaard allCaard;
    private Groop allGroop;
    private Rds rds;

    public List<Reservoir> reservoirs;
    //public List<Tile> tiles;
    //public List<Troop> troopScripts;
    public GridData data = null;


    [System.NonSerialized] public GridPivotConfig currentGrid;
    [System.NonSerialized] public TileShape previousTileShape;

    public void ResetGrid()
    {
        rds = new Rds(0);
        int rows = 16;
        int cols = 8;
        int tileCount = rows * cols;

        gameMaster = GameMaster.GetGM();
        if (data != null)
        {
            Debug.Log("TODO: Destroy gameObjects");
        }
        
        allTiile = AllTiile.genRectTiile(rows, cols, rds.getTiile());
        allTiile.updateVisuals();
        allGroop = new Groop();

        allCaard = new AllCaard();  // temp
        //allCaard = AllCaard.genStartCaard(8);
       

        if (GameObject.Find("Grid") != null)
            Object.Destroy(gridGO.gameObject);
        gridGO = new GameObject("Grid").transform;

        //gameMaster.StartingCards();
    }
    public void UpdateTroopTiles()
    {
        getAllGroop().resetParentTiles();
    }


    public Groop getAllGroop()
    {
        if (allGroop == null) Debug.LogError("IllegalStateException: bad construction of grid");
        return allGroop;
    }
    public AllCaard getAllCaard()
    {
        if (allCaard == null) Debug.LogError("IllegalStateException: bad construction of grid");
        return allCaard;
    }
    public AllTiile getAllTiile()
    {
        if (allTiile == null) Debug.LogError("IllegalStateException: bad construction of grid");
        return allTiile;
    }
}
