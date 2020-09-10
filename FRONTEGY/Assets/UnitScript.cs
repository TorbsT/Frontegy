using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    [Header("Variables")]
    [Range(0f, 10f)]
    [SerializeField] float walkAnimSpeed = 0.5f;
    [SerializeField] public UnitStats stats;
    [SerializeField] Material unitHoverMat;
    [SerializeField] Material unitSelectMat;

    [Header("System/Debug")]
    bool isInitialized = false;
    GameMaster gameMaster;
    public LineDoodooer line;
    public int id;
    [SerializeField] GameObject linePrefab;
    [SerializeField] Renderer renderer;
    [SerializeField] Selectable selectable;
    public List<Breadcrumb> breadcrumbsInRange;
    
    [SerializeField] int defaultLayer = 0;
    [SerializeField] int ignoreRaycastLayer = 2;

    void ManualStart()
    {
        isInitialized = true;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        RefreshStats();
        GetComponent<Renderer>().material = gameMaster.GetTeam(stats.teamId).material;
    }
    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
        RefreshStats();
        SetLayer();
        transform.localScale = new Vector3(stats.scale, stats.scale, stats.scale);
        if (TileTracker.GetTileById(stats.parentTileId) == null) return;
        SetPosition();
        
    }
    void SetPosition()
    {
        if (gameMaster.phase.type.name == "strategic" || line.line.positionCount == 0)
        {
            transform.position = TileTracker.GetTileById(stats.parentTileId).GetSurfacePos();
            transform.position += Vector3.up * transform.localScale[1];
        }
        else if (gameMaster.phase.type.name == "weiterWeiter")
        {
            int index;
            int step = gameMaster.phase.step;
            if (step < line.line.positionCount) index = step;
            else index = line.line.positionCount - 1;
            transform.position = Vector3.Lerp(transform.position, line.line.GetPosition(index), walkAnimSpeed*Time.deltaTime);
        }
    }
    void RefreshStats() {stats = gameMaster.DisplayGrid().unitStats[id];}
    public UnitStats SetStats() { return gameMaster.CurrentGrid().unitStats[id]; }
    void SetLayer()
    {
        if (gameMaster.IsThisPhase(StaticPhaseType.strategic) && gameMaster.phase.teamId == stats.teamId)
        {
            // selectable
            gameObject.layer = defaultLayer;
        }
        else gameObject.layer = ignoreRaycastLayer;
    }
    public void Hover()
    {
        if (!selectable.isSelected) renderer.material = unitHoverMat;
    }
    public void UnHover()
    {
        if (!selectable.isSelected) renderer.material = gameMaster.GetTeam(stats.teamId).material;
    }
    public List<int> PlanMovement(int toTileId)
    {
        Tile toTile = TileTracker.GetTileById(toTileId);
        Debug.Log("Searching for tile with id " + toTileId + ", gridPos " + TileTracker.GetPosById(toTileId));

        List<int> tilesInPF = Pathfinding.GetUntardedPath(Pathfinding.GetPaths(stats.parentTileId, stats.walkRange, new List<Vector2Int>(), toTile.geo.id), toTileId);
        Debug.Log("Found " + tilesInPF.Count);
        foreach (int nodeId in tilesInPF)
        {
            //Debug.Log(gameMaster.grid.GetGridPos(node.geo.id));
        }

        if (tilesInPF.Count == 0) return null;
        int destinationTileId = tilesInPF[tilesInPF.Count - 1];
        Debug.Log(gameMaster.grid.gridVers[0]);
        SetStats().path = tilesInPF;
        if (line == null) line = Instantiate(linePrefab).GetComponent<LineDoodooer>();
        line.ownerUnit = this;
        return tilesInPF;
    }
    public void Select()
    {
        renderer.material = unitSelectMat;
        breadcrumbsInRange = Pathfinding.GetAllTilesInRange(new Breadcrumb(stats.parentTileId, stats.walkRange));
        foreach (Breadcrumb breadcrumb in breadcrumbsInRange)
        {
            TileTracker.GetTileById(breadcrumb.tileId).ShowBreadcrumb(breadcrumb);
        }
    }
    public void UnSelect()
    {
        foreach (Breadcrumb breadcrumb in breadcrumbsInRange)
        {
            TileTracker.GetTileById(breadcrumb.tileId).UnShowBreadcrumb();
        }
        breadcrumbsInRange = new List<Breadcrumb>();
        renderer.material = gameMaster.GetTeam(stats.teamId).material;
    }
}
