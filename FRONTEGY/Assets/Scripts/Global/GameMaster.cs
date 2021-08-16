using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public Roster roster { get { return _roster; } }
    public Mat notFoundMat { get { return _notFoundMat; } }

    [Header("Variables")]
    [SerializeField] private bool randomSeed;
    [SerializeField] private CamConfig camConfig;
    [SerializeField] private GridConfig gridConfig;
    [SerializeField] private float stepDuration = 1f;
    [SerializeField] public float heightScalar = 1f;
    [SerializeField] public float tileLineHeight = 1f;
    [SerializeField] private Mat _notFoundMat;

    [SerializeField] public float tileCreateAnimationSpeed = 1f;
    [SerializeField] public Material globalHoverMat;
    [SerializeField] public Material globalSelectMat;
    [SerializeField] public Material breadcrumbMat;

    [Header("System")]
    public int displayHistory = 0;
    [SerializeField] public GameObject troopGOPrefab;
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] GameObject buildingPrefab;
    [SerializeField] public GameObject tileGOPrefab;
    [SerializeField] public GameObject cardGOPrefab;
    [SerializeField] public GameObject lineGOPrefab;
    [SerializeField] private Transform phyContainer;
    [SerializeField] private Camera camera;
    [SerializeField] private Cam cam;
    [SerializeField] private Pools pools;
    [SerializeReference] private Coontrol coontrol;
    [SerializeField] private Control control;
    [SerializeReference] public Grid grid;
    [SerializeField] public float stepTimeLeft;

    public List<Conflict> conflicts = new List<Conflict>();
    public List<Merge> merges = new List<Merge>();
    [SerializeField] private Playyer playyer;
    [SerializeField] private Roster _roster;
    [SerializeReference] private UIManager uiManager;

    GridPivotConfig gridNone;
    GridPivotConfig gridAnchored;
    GridPivotConfig gridCentered;
    private List<Transive> transives = new List<Transive>();

    public Cam getCam()
    {
        if (cam == null) Debug.LogError("IllegalStateException");
        return cam;
    }
    public GameObject getUIPrefab()
    {
        if (uiPrefab == null) Debug.LogError("InspectorException: Set uiPrefab in gm");
        return uiPrefab;
    }



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
    public Coontrol getCoontrol()
    {
        if (coontrol == null) Debug.LogError("IllegalStateException");
        return coontrol;
    }

    void Start()
    {
        cam = new Cam(getCamera(), getCamConfig());
        uiManager = new UIManager(2f);
        playyer.init();
        pools.init();
        Restart();
    }
    void Update()
    {
        control = getCoontrol().record();
        
        ExecuteManualUpdates(control);
        HandlePlayerInput(control);  // outdated - TODO
        showAllTransives();
    }
    private void showAllTransives()
    {
        foreach (Transive transive in transives)
        {
            transive.showTransIfNecessary();
        }
    }
    public void addTransive(Transive transive) { transives.Add(transive); }
    private void ExecuteManualUpdates(Control c)  // Replacement for mono update
    {
        grid.update(c);
    }
    private void Restart()
    {
        uiManager.restart();
        coontrol = new Coontrol();
        // destroys previous grid
        if (grid != null)
        {
            pools.unstageAll();
        }

        // starts new grid
        Debug.Log("restarted");
        if (randomSeed)
        {
            gridConfig.setSeed(Random.Range(0, 9999999));
        }
        grid = new Grid(this, gridConfig);
    }
    public Transform getPhyContainer()
    {
        if (phyContainer == null) Debug.LogError("InspectorException: set GameMaster.phyContainer");
        return phyContainer;
    }
    private CamConfig getCamConfig()
    {
        if (camConfig == null) Debug.LogError("InspectorException: Set GameMaster.camConfig");
        return camConfig;
    }
    private Camera getCamera()
    {
        if (camera == null) Debug.Log("InspectorException: Set GameMaster.camera");
        return camera;
    }
    void HandlePlayerInput(Control c)
    {
        if (c.getRDown()) Restart();
        //else if (c.getSpaceDown()) grid.getPhaseManager().attemptSkip();
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
