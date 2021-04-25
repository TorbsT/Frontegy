using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool randomSeed;
    [SerializeField] private int seed;
    [SerializeField] private float stepDuration = 1f;
    [SerializeField] public float heightScalar = 1f;
    [SerializeField] public float tileLineHeight = 1f;
    [SerializeField] public float tileCreateAnimationSpeed = 1f;
    [SerializeField] int playerStartingCards = 5;
    [SerializeField] public List<Card> cardBlueprints;
    [SerializeField] public Material globalHoverMat;
    [SerializeField] public Material globalSelectMat;
    [SerializeField] public Material breadcrumbMat;

    [Header("System")]
    public int displayHistory = 0;
    [SerializeField] public GameObject troopGOPrefab;
    [SerializeField] GameObject buildingPrefab;
    [SerializeField] public GameObject tileGOPrefab;
    [SerializeField] public GameObject cardGOPrefab;
    [SerializeField] public GameObject lineGOPrefab;
    [SerializeField] public SelectionManager selectionManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] CameraScript cameraScript;
    public Grid grid;
    [SerializeField] public float stepTimeLeft;

    private PhaseManager pm;
    public List<Player> players;
    public List<Conflict> conflicts = new List<Conflict>();
    public List<Merge> merges = new List<Merge>();
    public Player nonePlayer;
    private Rooster rooster;

    GridPivotConfig gridNone;
    GridPivotConfig gridAnchored;
    GridPivotConfig gridCentered;

    Conflict currentConflict;

    public static Groop getAllGroop() { return GetGM().internalGetAllGroop(); }
    public static GameObject GetGMGO()
    {
        GameObject gmGO = GameObject.FindGameObjectWithTag("GameMaster");
        if (gmGO == null) Debug.LogError("Couldn't find GMGO");
        return gmGO;
    }
    public static GameMaster GetGM()
    {
        GameMaster gm = GetGMGO().GetComponent<GameMaster>();
        if (gm == null) Debug.LogError("Couldn't find GM");
        return gm;
    }
    public static Tile sfindTile(TileLoc tileLoc) { return GetGM().findTile(tileLoc); }
    public Tile findTile(TileLoc tileLoc) { return grid.data.getAllTiile().find(tileLoc); }
    public static CardPhy sgetUnstagedCardPhy() { return sgetRooster().getUnstagedCardPhy(); }
    public static TilePhy sgetUnstagedTilePhy() { return sgetRooster().getUnstagedTilePhy(); }
    public static TroopPhy sgetUnstagedTroopPhy() { return sgetRooster().getUnstagedTroopPhy(); }
    public static PafPhy sgetUnstagedPafPhy() { return sgetRooster().getUnstagedPafPhy(); }
    public static Rooster sgetRooster() { return GetGM().getRooster(); }
    public Rooster getRooster() { if (rooster == null) Debug.LogError("Rooster should never be null"); return rooster; }
    public static SelectionManager GetSelectionManager()
    { return GetGM().selectionManager; }
    public static CameraScript getCameraScript()
    { return GetGM().cameraScript; }

    private Groop internalGetAllGroop() { return grid.data.getGroop(); }
    private AllCaard internalGetAllCaard() { return grid.data.getAllCaard(); }
    private AllTiile internalGetAllTiile() { return grid.data.getAllTiile(); }
    void Start()
    {
        rooster = new Rooster(20, 10, 200);
        Restart();
    }
    void Update()
    {
        //CheckIfPhaseShouldBeSkipped();

        CountDown();

        ExecuteManualUpdates();
        
        HandlePlayerInput();
    }
    void CountDown()
    {

    }
    public float GetStepTimeScalar()  // [0, 1]
    { return 1 - Mathf.Clamp01(stepTimeLeft / stepDuration); }
    /*
    void CheckIfPhaseShouldBeSkipped()
    {
        if (phase.round == 1 && phase.type.skipIfFirstRound)
        {
            NextPhase();
            CheckIfPhaseShouldBeSkipped();
        }
    }
    */
    public bool isThisPhase(PhaseType t)
    {
        return pm.isThisPhase(t);
    }
    public Player getCurrentPlayer()
    {
        int id = getCurrentPlayerId();
        Player p = getPlayerById(id);
        return p;
    }
    public Player getPlayerById(int id)
    {
        if (id == -1) return nonePlayer;

        foreach (Player p in players)
        {  // Find matching id
            if (p.hasId(id)) return p;
        }

        Debug.LogError("Should probably not happen, couldn't find player by id");
        return nonePlayer;
    }
    public bool currentPlayerIdIs(int id) { return getCurrentPlayerId() == id; }
    public int getCurrentPlayerId() { if (pm == null) return -1; return pm.getPlayerId(); }
    public int getPlayersCount()
    {
        if (players == null) return 0;
        return players.Count;
    }
    void HandlePlayerInput()
    {
        if (Input.GetKeyDown("r")) Restart();
        else if (Input.GetKeyDown("space")) pm.attemptSkip();
    }
    void ExecuteManualUpdates()  // Replacement for mono update
    {
        updatePhase();
        selectionManager.ManualUpdate();
        uiManager.ManualUpdate();
    }
    void updatePhase()
    {
        if (pm == null) return;
        pm.update();
    }
    void Restart()
    {
        if (randomSeed)
        {
            seed = Random.Range(0, 99999);
        }
        Random.InitState(seed);

        grid.ResetGrid();

        pm = new PhaseManager();

        gridNone = new GridPivotConfig(0f, 0f);
        gridAnchored = new GridPivotConfig(0f, 0.5f);
        gridCentered = new GridPivotConfig(-0.5f, 0.5f);

        grid.previousTileShape = Grid.TileShape.none;
    }
    /*
    public void StartingCards()
    {
        GiveEqualCards(playerStartingCards);
    }
    void GiveEqualCards(int count)
    {
        foreach (Player player in players)
        {
            GiveCards(player, count);
        }
    }
    void GiveCards(Player player, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GiveCard(player, PickCard());
        }
    }
    void GiveCard(Player player, CardPhy card)
    {
        card.SetPlayerHolder(player);
        grid.data.cards.Add(card);
    }
    CardPhy PickCard()
    {
        int blueprintCount = cardBlueprints.Count;
        int pickedCardIndex = Random.Range(0, blueprintCount);
        Card data = cardBlueprints[pickedCardIndex];
        CardPhy createdCard = grid.InstantiateCard(data);

        return createdCard;
    }



    */






    /*
    void InstantiateUnits(bool useGeoBlueprints = false)
    {
        if (grid.troopScripts != null)
        foreach (Troop troopS in grid.troopScripts)
        {
            Destroy(troopS.gameObject);
        }

        grid.troopScripts = new List<Troop>();
        List<int> availableTiles = grid.GetActiveTiles(useGeoBlueprints);

        for (int i = 0; i < grid.startingTroops; i++)
        {
            if (availableTiles.Count == 0) break;
            float randomValue = Random.value;
            int chosenTile = Mathf.FloorToInt(randomValue * availableTiles.Count);  // chosenTile is the id of the tile in collection of ALL ACtIVE TILES
            int tileId = availableTiles[chosenTile];  // tileId is id in collection of ALL TILES, active or not

            Troop uS = Instantiate(troopPrefab, instantiatedObjectsParent).GetComponent<Troop>();
            CurrentGrid().troopStats.Add(new TroopStats());
            TroopStats troopStatsBlueprint = CurrentGrid().troopStats[i];

            troopStatsBlueprint.parentTileId = tileId;
            troopStatsBlueprint.playerId = players.Count-Mathf.RoundToInt(Random.value+1f);
            uS.stats.id = i;
            grid.troopScripts.Add(uS);
            availableTiles.RemoveAt(chosenTile);
            if (i > 1000) break;
        }
    }
    */
   
}
