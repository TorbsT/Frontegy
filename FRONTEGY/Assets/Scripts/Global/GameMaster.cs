using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool randomSeed;
    [SerializeField] private int seed;
    [SerializeField] private float stepDuration = 1f;
    [SerializeField] int playerStartingCards = 5;
    [SerializeField] public List<CardData> cardBlueprints;
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
    [SerializeField] SelectionManager selectionManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] CameraScript cameraScript;
    public Grid grid;
    [SerializeField] private float stepTimeLeft;

    public Phase phase;
    public List<Player> players;
    public Player nonePlayer;

    GridPivotConfig gridNone;
    GridPivotConfig gridAnchored;
    GridPivotConfig gridCentered;

    Conflict currentConflict;

    void Start()
    {
        Restart();
    }
    void Update()
    {
        GridShit();

        //CheckIfPhaseShouldBeSkipped();

        CountDown();

        ExecuteManualUpdates();
        
        HandlePlayerInput();

    }
    void CountDown()
    {
        if (IsThisPhase(StaticPhaseType.weiterWeiter))
        {
            stepTimeLeft -= Time.deltaTime;
            if (stepTimeLeft <= 0f)
            {
                stepTimeLeft = stepDuration;
                phase.step++;
                if (phase.step > phase.steps)
                {
                    NextPhase();
                }
            }
        }
    }
    void GridShit()
    {
        VerifyTileShapes();
        SetGridAnchor();
    }
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
    void AttemptSkip()
    {
        if (true) NextPhase();//(phase.type.skippable) NextPhase();
    }
    void NextPhase()
    {
        selectionManager.ResetSelections();
        
        if (IsThisPhase(StaticPhaseType.weiterWeiter)) NextRound();
        else if (IsLastPlayer()) WeiterWeiter();
        else NextStrategicPhase();
        Debug.Log("Round " + phase.round + ": " + phase.type.name + " " + GetPhasePlayer().name);


    }
    void NextStrategicPhase()
    {
        phase.type = StaticPhaseType.strategic;
        phase.playerId++;
        if (phase.playerId >= players.Count) phase.playerId = 0;
    }
    void WeiterWeiter()
    {
        phase.type = StaticPhaseType.weiterWeiter;
        phase.playerId = -1;
        stepTimeLeft = stepDuration;
        phase.steps = GetMaxSteps();
        //Debug.Log("Round " + phase.round + ": " + phase.type.name + " " + GetPhasePlayer().name);
    }
    void NextRound()
    {
        phase.round++;
        TileTracker.UpdateGridValues();
        NextStrategicPhase();
    }
    int GetMaxSteps()
    {
        int steps = 0;
        foreach (Troop troop in grid.data.troops)
        {
            if (troop.line != null)
            {
                steps = Mathf.Max(steps, troop.GetStepCount());  // line.line position count is usually twice as big...
            }
        }
        return steps;
    }
    public Player GetPhasePlayer() {return GetPlayer(phase.playerId);}
    public Player GetPlayer(int id)
    {
        if (id == -1) return nonePlayer;
        else if (id < 0 || id >= players.Count)
        {
            Debug.LogError("ERROR: Tried to access invalid playerId "+id+", count is "+players.Count);
            return null;
        }
        return players[id];
    }
    bool IsLastPlayer() { return (phase.playerId >= players.Count-1); }
    public bool IsThisPhase(PhaseType spt) { return (phase.type.name == spt.name); }
    void HandlePlayerInput()
    {
        if (Input.GetKeyDown("r")) Restart();
        else if (Input.GetKeyDown("space")) AttemptSkip();
    }
    void ExecuteManualUpdates()  // Replacement for mono update
    {
        foreach (Tile tile in grid.data.tiles)
        {
            tile.ManualUpdate();
        }
        foreach (Troop troop in grid.data.troops)
        {
            troop.ManualUpdate();
        }
        selectionManager.ManualUpdate();
        cameraScript.ManualUpdate();
        uiManager.ManualUpdate();

    }
    void Restart()
    {
        if (randomSeed)
        {
            seed = Random.Range(0, 99999);
        }
        Random.InitState(seed);

        grid.ResetGrid();

        phase = new Phase();
        phase.playerId = 0;
        phase.type = StaticPhaseType.strategic;
        phase.round = 0;

        gridNone = new GridPivotConfig(0f, 0f);
        gridAnchored = new GridPivotConfig(0f, 0.5f);
        gridCentered = new GridPivotConfig(-0.5f, 0.5f);

        grid.previousTileShape = Grid.TileShape.none;
    }
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
    void GiveCard(Player player, Card card)
    {
        card.SetPlayerHolder(player);
        grid.data.cards.Add(card);
    }
    Card PickCard()
    {
        int blueprintCount = cardBlueprints.Count;
        int pickedCardIndex = Random.Range(0, blueprintCount);
        CardData data = cardBlueprints[pickedCardIndex];
        Card createdCard = grid.InstantiateCard(data);

        return createdCard;
    }












    void VerifyTileShapes()  // Checks if tile shapes was changed, and if so, forces each tile to change
    {
        if (grid.tileShape != grid.previousTileShape)
        {
            foreach (Tile tile in grid.data.tiles)
            {
                tile.VerifyMesh();
            }
            grid.previousTileShape = grid.tileShape;
        }
    }
    void SetGridAnchor()  // Offsets/anchors all tiles according to current config
    {
        switch (grid.gridConfig)
        {
            case (Grid.GridConfigs.none):
                grid.currentGrid = gridNone;
                break;
            case (Grid.GridConfigs.anchored):
                grid.currentGrid = gridAnchored;
                break;
            case (Grid.GridConfigs.centered):
                grid.currentGrid = gridCentered;
                break;
        }
    }
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
