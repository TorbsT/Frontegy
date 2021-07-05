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
    private GridConfig config;
    private AllTiile allTiile;
    private AllCaard allCaard;
    private Groop allGroop;
    private Rds rds;
    private PhaseManager pm;
    private Playyer playyer;
    private Roole roole;
    [SerializeReference] private SelMan selectionManager;
    int debug;
    public List<Reservoir> reservoirs;

    //public List<Tile> tiles;
    //public List<Troop> troopScripts;


    [System.NonSerialized] public GridPivotConfig currentGrid;
    [System.NonSerialized] public TileShape previousTileShape;
    public UIManager UiManager { get { return gm.UiManager; } }




    public Grid(GameMaster gm, GridConfig config)
    {
        Debug.Log("grid restarted");
        if (gm == null) Debug.LogError("IllegalArgumentException");
        if (config.getSize().x <= 0 || config.getSize().y <= 0) Debug.LogError("IllegalArgumentException");
        //if (config.getNonePlayerTileWeight() + config.getPlayer0TileWeight() + config.getPlayer1TileWeight() == 0) Debug.LogError("IllegalArgumentException: Please provide atleast one weight other than 0");
        this.gm = gm;
        this.config = config;

        roole = config.getBPs().makeRooleFromSummonBPs();
        roole.applyRoles();

        playyer = getGM().getPlayyer();
        pm = new PhaseManager(this);
        rds = new Rds(config.getSeed());
        allTiile = genRectTiile();
        allTiile.updateVisuals();
        allGroop = new Groop();

        //allCaard = new AllCaard();  // temp
        allCaard = genStartCaard();

        //gameMaster.StartingCards();
        selectionManager = new SelMan(getCam());
    }

    public void planPaf(Troop troop, PafChy pafChy)
    {

    }
    public void removePaf(Troop troop)
    {

    }
    public TroopPlan getTroopPlan(Troop troop)
    {
        return getPhaseManager().getTroopPlan(troop);
    }
    public Transform getPhyContainer()
    {
        return getGM().getPhyContainer();
    }
    private void setTransformParent(Transform p, Transform t)
    {
        t.SetParent(p, true);
    }
    private Transform getUIPlace(UIPlace place)
    {
        return UiManager.getUIPlace(place);
    }
    public SelMan getSelectionManager()
    {
        if (selectionManager == null) Debug.LogError("IllegalStateException");
        return selectionManager;
    }

    private AllCaard genStartCaard()
    {
        AllCaard ac = new AllCaard();
        int count = config.getStartCardCount();
        if (count < 0) Debug.LogError("IllegalArgumentException");

        
        if (config.getGiveSameCards())
        {
            List<int> cardMap = new Weights(count, config.getCardWeights(), rds.getCaard0()).getOutput();

            string write = "";
            foreach (int id in cardMap)
            {
                SummonBP sbp = config.getBPs().getSummonBP(id);
                write += sbp + ", ";
                Card c0 = new Card(this, sbp, getPlayerByIndex(0));
                Card c1 = new Card(this, sbp, getPlayerByIndex(1));
                ac.add(c0);
                ac.add(c1);
            }
            Debug.Log(write);
        }
        return ac;
    }
    private AllTiile genRectTiile()
    {  // dysfunction hihihihi
        GameMaster gm = GameMaster.GetGM();
        int rows = config.getSize().x;
        int cols = config.getSize().y;
        int tileCount = rows * cols;
        List<int> playerMap = new Weights(tileCount, config.getTileWeights(), rds.getTiile()).getOutput();

        List<Tile> tiles = new List<Tile>();
        for (int w = 0; w < cols; w++)
        {
            for (int l = 0; l < rows; l++)
            {
                TileLoc tloc = new TileLoc(l, w);
                // TODO randomize gen params
                

                int tempId = w * rows + l;
                int p = playerMap[tempId];

                Player player;
                if (p == -1) player = getNonePlayer();
                else player = getPlayerByIndex(p);

                Tile tile = new Tile(this, true, tloc, player);
                tiles.Add(tile);
            }
        }
        AllTiile tiile = new AllTiile(this, tiles);
        return tiile;
    }
    public Pos3 getCenterPos3()
    {  // May be TileLoc or BorderLoc
        // Assuming grid represents config
        Vector2Int size = config.getSize();
        int rows = size[0] - 1;
        int cols = size[1] - 1;
        float centerRowFloat = rows/2f;
        float centerColFloat = cols/2f;
        return TileLoc.toPos3(centerRowFloat, 0f, centerColFloat);
        /*
        bool centerIsOnBorder = Mathf.Floor(centerRowFloat) != centerRowFloat || Mathf.Floor(centerColFloat) != centerColFloat;
        if (centerIsOnBorder)
        {
            // MiddleLoc is between two or more tiles. Must create a FloatLoc.
            int centerRowA = Mathf.FloorToInt(centerRowFloat);
            int centerColA = Mathf.FloorToInt(centerColFloat);
            int centerRowB = Mathf.CeilToInt(centerRowFloat);
            int centerColB = Mathf.CeilToInt(centerColFloat);

            TileLoc a = new TileLoc(centerRowA, centerColA);
            TileLoc b = new TileLoc(centerRowB, centerColB);

            return new BorderLoc(a, b);
        } else
        {
            // MiddleLoc is a TileLoc. Easy job.
            int centerRow = rows / 2;
            int centerCol = cols / 2;
            return new TileLoc(centerRow, centerCol);
        }
        */
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
    public Caard getCaardInHandOf(Player player) { return getAllCaard().getCaardOwnedBy(player); }

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

    public void update(Control c)
    {
        getPhaseManager().update(c);
    }
    public Cam getCam() { return getGM().getCam(); }
    public GameMaster getGM() { if (gm == null) Debug.LogError("IllegalStateException"); return gm; }
    private Playyer getPlayyer() { if (playyer == null) Debug.LogError("IllegalStateException"); return playyer; }
}
