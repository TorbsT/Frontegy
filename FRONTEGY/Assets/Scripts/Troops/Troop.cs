using System.Collections.Generic;
using UnityEngine;

public class Troop : Selectable
{
    [Header("Variables")]
    [Range(0f, 10f)]
    [SerializeField] float walkAnimSpeed = 0.5f;
    [SerializeField] public TroopStats stats;
    [SerializeField] Material unitHoverMat;
    [SerializeField] Material unitSelectMat;

    [Header("System/Debug")]
    public GameObject troopGO;
    bool isInitialized = false;
    GameMaster gameMaster;
    public LineDoodooer line;
    bool isSelected;
    [SerializeField] GameObject linePrefab;
    [SerializeField] Renderer rndrr;
    //[SerializeField] Selectable selectable;
    public List<Breadcrumb> breadcrumbsInRange;
    
    [SerializeField] int defaultLayer = 0;
    [SerializeField] int ignoreRaycastLayer = 2;

    void ManualStart()
    {
        isInitialized = true;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        GetComponent<Renderer>().material = gameMaster.GetTeamOfPlayer(stats.playerId).material;
    }
    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
        SetLayer();
        transform.localScale = new Vector3(stats.scale, stats.scale, stats.scale);
        if (TileTracker.GetTileById(stats.parentTileId) == null) return;
        SetPosition();
        
    }
    void SetPosition()
    {
        if (gameMaster.IsThisPhase(StaticPhaseType.strategic) || line == null || line.line.positionCount == 0)
        {
            transform.position = TileTracker.GetTileById(stats.parentTileId).GetSurfacePos();
            transform.position += Vector3.up * transform.localScale[1];
        }
        else if (gameMaster.IsThisPhase(StaticPhaseType.weiterWeiter))
        {
            int index;
            int step = gameMaster.phase.step;
            if (step < line.line.positionCount) index = step;
            else index = line.line.positionCount - 1;
            transform.position = Vector3.Lerp(transform.position, line.line.GetPosition(index), walkAnimSpeed*Time.deltaTime);
        }
    }
    void SetLayer()
    {
        if (gameMaster.IsThisPhase(StaticPhaseType.strategic) && gameMaster.phase.playerId == stats.playerId)
        {
            // selectable
            gameObject.layer = defaultLayer;
        }
        else gameObject.layer = ignoreRaycastLayer;
    }


    public override Troop SelGetTroop() { return this; }
    public override void SelHover()
    {
        if (!isSelected) rndrr.material = unitHoverMat;
    }
    public override void SelUnHover()
    {
        if (!isSelected) rndrr.material = gameMaster.GetTeamOfPlayer(stats.playerId).material;
    }
    public override void SelSelect()
    {
        rndrr.material = unitSelectMat;
        breadcrumbsInRange = Pathfinding.GetAllTilesInRange(new Breadcrumb(stats.parentTileId, stats.walkRange, 0));
        foreach (Breadcrumb breadcrumb in breadcrumbsInRange)
        {
            TileTracker.GetTileById(breadcrumb.tileId).ShowBreadcrumb(breadcrumb);
        }

        Conflict conflict = new Conflict(new List<TroopStats> { stats }, false);
        conflict.DebugTroops(conflict.GetRankedTroops(new List<TroopStats> { stats }));

    }
    public override void SelUnSelect()
    {
        foreach (Breadcrumb breadcrumb in breadcrumbsInRange)
        {
            TileTracker.GetTileById(breadcrumb.tileId).UnShowBreadcrumb();
        }
        breadcrumbsInRange = new List<Breadcrumb>();
        rndrr.material = gameMaster.GetTeamOfPlayer(stats.playerId).material;
    }
    public override List<Breadcrumb> SelPlanMovement(int fromTileId, int toTileId)
    {
        List<Breadcrumb> path = Pathfinding.GetBreadcrumbPath(fromTileId, toTileId);
        path = Pathfinding.GetUntardedPath(path);

        if (path == null) return null;
        stats.path = path;
        if (line == null) line = Instantiate(linePrefab).GetComponent<LineDoodooer>();
        line.ownerUnit = this;
        return path;
    }
}
