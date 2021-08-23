using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid
{
    public static Grid Instance { get; private set; }
    [Header("Grid Variables")]
    public Vector2Int gridSize;
    [Range(0.2f, 1.0f)]
    public float tileRatio;

    [Header("Grid Debug")]
    private GridConfig config;

    private Rds rds;
    [SerializeReference] private SelMan selectionManager;

    public List<TileState> tileStates { get => _tileStates; }
    public List<TroopState> troopStates { get => _troopStates; }
    public List<CardState> cardStates { get => _cardStates; }

    public AllTiile allTiile { get => _allTiile; }
    public AllCaard allCaard { get => _allCaard; }
    public AllGroop allGroop { get => _allGroop; }
    public List<TacticalHistory> tacticalHistories { get => _tacticalHistories; }

    private List<TileState> _tileStates = new List<TileState>();
    private List<TroopState> _troopStates = new List<TroopState>();
    private List<CardState> _cardStates = new List<CardState>();

    [SerializeReference] private List<TacticalHistory> _tacticalHistories = new List<TacticalHistory>();
    private AllTiile _allTiile;
    private AllCaard _allCaard;
    private AllGroop _allGroop;
    private RoundManager pm;
    private ActionManager _actionManager;
    public Grid(GameMaster gm, GridConfig config)
    {
        Instance = this;
        Debug.Log("grid restarted");
        if (gm == null) Debug.LogError("IllegalArgumentException");
        if (config.getSize().x <= 0 || config.getSize().y <= 0) Debug.LogError("IllegalArgumentException");
        //if (config.getNonePlayerTileWeight() + config.getPlayer0TileWeight() + config.getPlayer1TileWeight() == 0) Debug.LogError("IllegalArgumentException: Please provide atleast one weight other than 0");
        this.config = config;
        config.cardBPs.init();
        config.getRoole().init();
        pm = new RoundManager(this);
        rds = new Rds(config.getSeed());
        _allTiile = genRectTiile();
        _allGroop = new AllGroop();
        _actionManager = new ActionManager();

        //allCaard = new AllCaard();  // temp
        _allCaard = genStartCaard();

        //gameMaster.StartingCards();
        selectionManager = new SelMan(Cam.Instance);
    }
    public void update(Control c)
    {
        pm.update(c);
    }
    public void makeStatesForNewRound(Round oldRound)
    {

        // states should be updated independently, e.g. use oldRound.results when changing tile owners.
        foreach (TileState tileState in _tileStates.FindAll(state => state.roundId == oldRound.roundId))
        {
            TileState newState = tileState.copy();
            newState.roundId++;

            // Updates the Tile of corresponding loc. (should be exactly one)
            Tile tile = _allTiile.find(newState.loc);
            if (tile == null) Debug.LogError("what the hell is this");

            // adds this state to list to be looked up later
            _tileStates.Add(newState);
        }
        foreach (TroopState troopState in _troopStates.FindAll(state => state.roundId == oldRound.roundId))
        {
            TroopState newState = troopState.copy();
            newState.roundId++;
            Breadcrumb startBreadcrumb = new Breadcrumb(newState.parentTile, newState.role.baseStats.getRANGE());
            newState.djikstra = new Djikstra(startBreadcrumb);
            newState.paf = new Paf(startBreadcrumb);
            if (!troopState.stepStates.currentDead)
            {
                Tile newParentTile = troopState.stepStates.currentBreadcrumb.tile;
                newState.parentTile = newParentTile;
                //TileState newParentTileState = tileStates.Find(state => state.roundId == newState.roundId && state.loc == newParentTile.loc);
                //newParentTileState.ownerId = troopState.ownerId;
            }

            _troopStates.Add(newState);
        }

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
                SummonCardBP sbp = CardBPs.Instance.getSummonBP(id);
                write += sbp + ", ";
                foreach (Player player in Playyer.Instance.getPlayers())
                {
                    CardState state = new CardState();
                    state.blueprint = sbp;
                    state.owner = player;
                    Card card = new Card(state);
                    ac.add(card);
                }
            }
            Debug.Log(write);
        }
        return ac;
    }
    private AllTiile genRectTiile()
    {  // dysfunction hihihihi
        // grow up you poopyhead
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
                if (p == -1) player = Playyer.Instance.getNonePlayer();
                else player = Playyer.Instance.getPlayerByIndex(p);

                TileState state = new TileState();
                state.owner = player;
                Tile tile = new Tile(state, tloc);
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
    }
}
