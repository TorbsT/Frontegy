using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private bool randomSeed;
    [SerializeField] private int seed;
    [SerializeField] private float stepDuration = 1f;

    [Header("System")]
    public int displayHistory = 0;
    [SerializeField] GameObject unitPrefab;
    [SerializeField] GameObject buildingPrefab;
    [SerializeField] GameObject tilePrefab;
    Transform instantiatedObjectsParent;
    [SerializeField] SelectionManager selectionManager;
    [SerializeField] UIManager uiManager;
    public Grid grid;
    [SerializeField] private float stepTimeLeft;

    public Phase phase;
    public List<Team> teams;

    GridPivotConfig gridNone;
    GridPivotConfig gridAnchored;
    GridPivotConfig gridCentered;

    void Start()
    {
        Restart();
    }
    void Update()
    {
        GridShit();

        CheckIfPhaseShouldBeSkipped();

        CountDown();

        ExecuteManualUpdates();

        HandlePlayerInput();
    }
    void CountDown()
    {
        if (phase.type.name == "weiterWeiter")
        {
            stepTimeLeft -= Time.deltaTime;
            if (stepTimeLeft <= 0f)
            {
                stepTimeLeft = stepDuration;
                phase.step++;
                if (phase.step > phase.steps)
                {
                    phase.steps = 0;  // necessary?
                    phase.step = 0;  // necessary?
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
    void CheckIfPhaseShouldBeSkipped()
    {
        if (phase.round == 1 && phase.type.skipIfFirstRound)
        {
            NextPhase();
            CheckIfPhaseShouldBeSkipped();
        }
    }
    void HandlePlayerInput()
    {
        if (Input.GetKeyDown("r")) Restart();
        else if (Input.GetKeyDown("space")) AttemptSkip();
    }
    void AttemptSkip()
    {
        if (true) NextPhase();//(phase.type.skippable) NextPhase();
    }
    void NextPhase()
    {
        selectionManager.ResetSelections();
        if (IsThisPhase(StaticPhaseType.weiterWeiter)) phase.type = StaticPhaseType.strategic;
        else if (IsThisPhase(StaticPhaseType.strategic)) NextTeam();
    }
    void NextTeam()
    {
        phase.type = StaticPhaseType.weiterWeiter;
        phase.steps = GetMaxSteps();
        phase.teamId++;
        if (WasLastTeam()) NextRound();
    }
    int GetMaxSteps()
    {
        int steps = 0;
        foreach (UnitScript unit in grid.unitScripts)
        {
            if (unit.line != null)
            {
                steps = Mathf.Max(steps, unit.line.line.positionCount-1);
            }
        }
        return steps;
    }
    void NextRound()
    {
        phase.teamId = 1;
        phase.round++;
        grid.gridVers.Insert(0, grid.gridVers[0]);
        grid.gridVers[0] = ObjectCopier.Clone(CurrentGrid());
        // SET PARENTTILE AND SHIT HERE??
        //grid.gridVers[0].unitStats
    }
    public Team GetPhaseTeam() {return GetTeam(phase.teamId);}
    public Team GetTeam(int id) { return teams[id]; }
    bool WasLastTeam() { return (phase.teamId >= teams.Count); }
    public bool IsThisPhase(PhaseType spt) { return (phase.type.name == spt.name); }
    void ExecuteManualUpdates()  // Replacement for mono update
    {
        foreach (Tile tile in grid.tiles)
        {
            tile.ManualUpdate();
        }
        foreach (UnitScript unitS in grid.unitScripts)
        {
            unitS.ManualUpdate();
        }
        selectionManager.ManualUpdate();
    }
    public GridContents LastGrid() { return grid.gridVers[1]; }
    public GridContents CurrentGrid() { return grid.gridVers[0]; }
    public GridContents DisplayGrid() { return grid.gridVers[displayHistory]; }
    void Restart()
    {
        if (randomSeed)
        {
            seed = Random.Range(0, 99999);
        }
        Random.InitState(seed);

        grid.gridVers = new List<GridContents>();
        grid.gridVers.Add(new GridContents(new List<Geo>(), new List<UnitStats>()));

        Debug.Log("PØLSe");
        phase = new Phase();
        phase.teamId = 1;
        phase.type = StaticPhaseType.weiterWeiter;
        phase.round = 1;

        gridNone = new GridPivotConfig(0f, 0f);
        gridAnchored = new GridPivotConfig(0f, 0.5f);
        gridCentered = new GridPivotConfig(-0.5f, 0.5f);

        if (GameObject.Find("InstantiatedObjects") == null) instantiatedObjectsParent = new GameObject("InstantiatedObjects").transform;
        grid.previousTileShape = Grid.TileShape.none;
        grid.geoBlueprints = GetRandomizedGeos();
        InstantiateTiles(grid.geoBlueprints);
        InstantiateUnits(true);
    }














    void VerifyTileShapes()  // Checks if tile shapes was changed, and if so, forces each tile to change
    {
        if (grid.tileShape != grid.previousTileShape)
        {
            foreach (Tile tile in grid.tiles)
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


    void InstantiateTiles(List<Geo> blueprints)
    {
        if (grid.tiles != null)
        foreach (Tile tile in grid.tiles)
        {
            Destroy(tile.transform.parent.gameObject);
        }
        grid.tiles = new List<Tile>();
        foreach (Geo blueprint in blueprints)
        {
            GameObject tileObj = Instantiate(tilePrefab, instantiatedObjectsParent);
            Tile createdTile = tileObj.GetComponentInChildren<Tile>();
            createdTile.initialGeo = blueprint;
            createdTile.gameMaster = this;
            grid.tiles.Add(createdTile);
        }
    }
    void InstantiateUnits(bool useGeoBlueprints = false)
    {
        if (grid.unitScripts != null)
        foreach (UnitScript unitS in grid.unitScripts)
        {
            Destroy(unitS.gameObject);
        }

        grid.unitScripts = new List<UnitScript>();
        List<Tile> availableTiles = grid.GetActiveTiles(useGeoBlueprints);
        for (int i = 0; i < grid.startingUnits; i++)
        {
            if (availableTiles.Count == 0) break;
            float randomValue = Random.value;
            int chosenTile = Mathf.FloorToInt(randomValue * availableTiles.Count);
            //int tileId = availableTiles[i].geo.id;

            UnitScript uS = Instantiate(unitPrefab, instantiatedObjectsParent).GetComponent<UnitScript>();
            CurrentGrid().unitStats.Add(new UnitStats());
            UnitStats unitStatsBlueprint = CurrentGrid().unitStats[i];

            unitStatsBlueprint.parentTileId = chosenTile;
            unitStatsBlueprint.teamId = teams.Count-Mathf.RoundToInt(Random.value+1f);
            uS.id = i;
            grid.unitScripts.Add(uS);
            availableTiles.RemoveAt(chosenTile);
            if (i > 1000) break;
        }
    }
    List<Geo> GetRandomizedGeos()
    {
        List<Geo> blueprintGeos = new List<Geo>();
        int remainingSpaces = grid.GetNodeCount();
        int remainingTiles = Mathf.RoundToInt(grid.tileRatio*grid.GetNodeCount());

        int gridXPos = 0;
        int gridYPos = 0;

        List<int> reservoirSpawnWeights = new List<int>();
        foreach (Reservoir res in grid.reservoirs)
        {
            reservoirSpawnWeights.Add(res.spawnWeight);
        }


        for (int i = 0; i < grid.GetNodeCount(); i++)
        {
            float prob = ((float)remainingTiles)/((float)remainingSpaces);

            Geo blueprintGeo = new Geo();
            float thisRandomValue = Random.value;
            if (prob >= thisRandomValue)
            {
                blueprintGeo.isActive = true;
                remainingTiles--;
            }
            else blueprintGeo.isActive = false;


            blueprintGeo.reservoir = DecideReservoir(reservoirSpawnWeights);
            
            if (0.5f >= Random.value) blueprintGeo.teamId = 1;
            else blueprintGeo.teamId = 2;


            remainingSpaces--;
            blueprintGeo.height = 1f+Mathf.Round(Random.value*10f)/10f;
            blueprintGeo.id = i;
            blueprintGeo.gridPos = new Vector2Int(gridXPos, gridYPos);
            blueprintGeos.Add(blueprintGeo);

            gridXPos++;
            if (gridXPos >= grid.gridSize[0])
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
        int reservoirValue = Mathf.FloorToInt(randomFloat*GetSumOfWeights(weights));
        if (debug) Debug.Log(randomFloat + " " + reservoirValue);
        for (int i = 0; i < weights.Count; i++)
        {
            int sW = weights[i] + resWeightsSoFar;
            if (debug) Debug.Log(sW + " " + weights[i] + " " + resWeightsSoFar);
            if (sW > reservoirValue)
            {
                if (debug) Debug.Log("true");
                return grid.reservoirs[i];
            }
            if (debug) Debug.Log("false");
            resWeightsSoFar += weights[i];
        }
        Debug.LogError("FATAL ERROR ON GETTING RESERVOIR");
        return grid.reservoirs[0]; 
    }
    int GetSumOfWeights(List<int> weights)
    {
        int sum = 0;
        foreach (int weight in weights)
        {
            sum += weight;
        }
        return sum;
    }
}
