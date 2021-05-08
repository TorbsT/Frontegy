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

    internal Player getFirstPlayer()
    {
        return getPlayyer().getFirstPlayer();
    }

    [Range(0.0f, 1.0f)]
    public float tileGap;
    [Range(0.1f, 10.0f)]
    public float tileSize;

    // ONLY TEMPORARY
    public int startingTroops;

    [Header("Grid Debug")]
    public GameMaster gm;
    private AllTiile allTiile;
    private AllCaard allCaard;
    private Groop allGroop;
    private Rds rds;
    private PhaseManager pm;
    private Playyer playyer;
    int debug;
    public List<Reservoir> reservoirs;
    //public List<Tile> tiles;
    //public List<Troop> troopScripts;


    [System.NonSerialized] public GridPivotConfig currentGrid;
    [System.NonSerialized] public TileShape previousTileShape;

    public Grid(GameMaster gm, int seed, Vector2Int size, int nonePlayerTileWeight, int player0TileWeight, int player1TileWeight)
    {
        Debug.Log("grid restarted");
        if (gm == null) Debug.LogError("IllegalArgumentException");
        if (size.x <= 0 || size.y <= 0) Debug.LogError("IllegalArgumentException");
        if (nonePlayerTileWeight + player0TileWeight + player1TileWeight == 0) Debug.LogError("IllegalArgumentException: Please provide atleast one weight other than 0");
        this.gm = gm;

        playyer = getGM().getPlayyer();
        pm = new PhaseManager(this);
        rds = new Rds(seed);
        allTiile = genRectTiile(size, rds.getTiile(), nonePlayerTileWeight, player0TileWeight, player1TileWeight);
        allTiile.updateVisuals();
        allGroop = new Groop();

        allCaard = new AllCaard();  // temp
                                    //allCaard = AllCaard.genStartCaard(8);
        
        //gameMaster.StartingCards();
    }

    public TilePhy getUnstagedTilePhy()
    {
        return getRooster().getUnstagedTilePhy();
    }
    public CardPhy getUnstagedCardPhy()
    {
        return getRooster().getUnstagedCardPhy();
    }
    public PafPhy getUnstagedPafPhy()
    {
        return getRooster().getUnstagedPafPhy();
    }
    public TroopPhy getUnstagedTroopPhy()
    {
        return getRooster().getUnstagedTroopPhy();
    }
    public Tile getHoveredTile() { return getSelectionManager().getHoveredTile(); }
    public SelectionManager getSelectionManager() { return getGM().getSelectionManager(); }

    private AllTiile genRectTiile(Vector2Int size, Rd rd, int nonePlayerTileWeight, int player0TileWeight, int player1TileWeight)
    {  // dysfunction hihihihi
        if (size.x <= 0 || size.y <= 0) Debug.LogError("IllegalArgumentException");
        if (nonePlayerTileWeight + player0TileWeight + player1TileWeight == 0) Debug.LogError("IllegalArgumentException: Please provide atleast one weight other than 0");
        GameMaster gm = GameMaster.GetGM();
        int rows = size.x;
        int cols = size.y;
        int tileCount = rows * cols;
        List<int> playerMap = new Weights(tileCount, new List<Vector2Int> { new Vector2Int(-1, nonePlayerTileWeight), new Vector2Int(0, player0TileWeight), new Vector2Int(1, player1TileWeight) }, rd).getOutput();

        AllTiile tiile = new AllTiile(this);
        string write = "";
        for (int w = 0; w < cols; w++)
        {
            for (int l = 0; l < rows; l++)
            {
                TileLoc tloc = new TileLoc(l, w);
                // TODO randomize gen params
                Tile tile = new Tile(this, true, tloc);

                int tempId = w * rows + l;
                int p = playerMap[tempId];

                Player player;
                if (p == -1) player = getNonePlayer();
                else player = getPlayerByIndex(p);
                tile.setPlayer(player);

                write += p + ", ";





                tiile.add(tile);
            }
        }
        Debug.Log(write);
        return tiile;
    }



    public Player getPlayerByIndex(int p)
    {
        return getPlayyer().getPlayerByIndex(p);
    }
    public Player playerAfter(Player player)
    {
        return getPlayyer().playerAfter(player);
    }
    public Player getNonePlayer()
    {
        return getPlayyer().getNonePlayer();
    }
    public bool isLastPlayer(Player player)
    {
        return getPlayyer().isLastPlayer(player);
    }
    public Player getCurrentPlayer()
    {
        return pm.getPlayer();
    }

    public PhaseManager getPhaseManager()
    {
        if (pm == null) Debug.LogError("IllegalStateException");
        return pm;
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

    public void update()
    {
        getPhaseManager().update();
    }
    private Rooster getRooster() { return getGM().getRooster(); }
    private GameMaster getGM() { if (gm == null) Debug.LogError("IllegalStateException"); return gm; }
    private Playyer getPlayyer() { if (playyer == null) Debug.LogError("IllegalStateException"); return playyer; }
}
