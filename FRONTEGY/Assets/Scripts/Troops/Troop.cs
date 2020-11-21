using System.Collections.Generic;
using UnityEngine;

public class Troop : Selectable
{
    public Troop(TroopStats _stats)
    {
        stats = _stats;
    }
    [Header("Variables")]
    [SerializeField] public TroopStats stats;

    [Header("System/Debug")]
    public LineDoodooer line;
    [SerializeField] Renderer rndrr;
    //[SerializeField] Selectable selectable;
    public List<Breadcrumb> breadcrumbsInRange;
    
    [SerializeField] int defaultLayer = 0;
    [SerializeField] int ignoreRaycastLayer = 2;

    public override void Instantiate()
    {
        Instantiate2(this.GetType());
        rndrr = selGO.GetComponent<Renderer>();
        rndrr.material = gameMaster.GetPhasePlayer().mat;
    }
    public void ManualUpdate()
    {
        if (!isInstantiated) Debug.LogError("ERROR: Troop is not instantiated");
        SetLayer();
        selGO.transform.localScale = new Vector3(stats.scale, stats.scale, stats.scale);
        if (TileTracker.GetTileById(stats.parentTileId) == null) return;
        SetPosition();
    }
    public int To()
    {
        return TileIdByPathIndex(gameMaster.BigStep() + 1);
    }
    public int From()
    {
        return TileIdByPathIndex(gameMaster.BigStep());
    }
    int TileIdByPathIndex(int index)
    {  // This id is made out of id
        if (stats.path == null)
        {
            Debug.LogError("ERRORRRRRRRR HJEEØEØEØLEn");
            return -1;
        }
        int breadcrumbCount = stats.path.Count - 1;
        if (breadcrumbCount == 0)
        {
            Debug.LogError("ERRORRRRRRRR HJEEØEØEØLE");
            return -1;
        }
        index = Mathf.Clamp(index, 0, breadcrumbCount);
        int tileId = stats.path[index].tileId;
        return tileId;
    }
    void SetPosition()
    {
        if (gameMaster.IsThisPhase(StaticPhaseType.strategic) || line == null || line.line.positionCount == 0)
        {
            selGO.transform.position = TileTracker.GetTileById(stats.parentTileId).GetSurfacePos();
        }
        else if (gameMaster.IsThisPhase(StaticPhaseType.weiterWeiter))
        {
            int nextIndex;
            int previousIndex;
            int step = gameMaster.phase.step;
            int stepCount = line.line.positionCount;
            nextIndex = Mathf.Clamp(step+1, 0, stepCount-1);
            previousIndex = Mathf.Clamp(step, 0, stepCount - 1);

            selGO.transform.position = Vector3.Lerp(line.line.GetPosition(previousIndex), line.line.GetPosition(nextIndex), Mathf.Sqrt(gameMaster.GetStepTimeScalar()));
        }
        AdjustYByHeight();
    }
    void AdjustYByHeight()
    {
        selGO.transform.position += Vector3.up * selGO.transform.localScale[1];
    }
    void SetLayer()
    {
        if (gameMaster.IsThisPhase(StaticPhaseType.strategic) && gameMaster.phase.playerId == stats.playerId)
        {
            // selectable
            selGO.gameObject.layer = defaultLayer;
        }
        else selGO.gameObject.layer = ignoreRaycastLayer;
    }


    public override Troop SelGetTroop() { return this; }
    public override void SelHover()
    {
        if (!isSelected) rndrr.material = gameMaster.globalHoverMat;
    }
    public override void SelUnHover()
    {
        if (!isSelected) rndrr.material = gameMaster.GetPhasePlayer().mat;
    }
    public override void SelSelect()
    {
        rndrr.material = gameMaster.globalSelectMat;
        breadcrumbsInRange = Pathfinding.GetAllTilesInRange(new Breadcrumb(stats.parentTileId, stats.GetWalkRange(), 0));

        DebugUnits();
        foreach (Breadcrumb breadcrumb in breadcrumbsInRange)
        {
            TileTracker.GetTileById(breadcrumb.tileId).ShowBreadcrumb(breadcrumb);
        }

        //Conflict conflict = new Conflict(new List<TroopStats> { stats }, false);
        //conflict.DebugTroops(conflict.GetRankedTroops(new List<TroopStats> { stats }));

    }
    public override void SelUnSelect()
    {
        foreach (Breadcrumb breadcrumb in breadcrumbsInRange)
        {
            TileTracker.GetTileById(breadcrumb.tileId).UnShowBreadcrumb();
        }
        breadcrumbsInRange = new List<Breadcrumb>();
        rndrr.material = gameMaster.GetPhasePlayer().mat;
    }
    public override List<Breadcrumb> SelPlanMovement(int fromTileId, int toTileId)
    {
        List<Breadcrumb> path = Pathfinding.GetBreadcrumbPath(fromTileId, toTileId);
        path = Pathfinding.GetUntardedPath(path);

        if (path == null) return null;
        stats.path = path;
        if (line == null) line = Object.Instantiate(gameMaster.lineGOPrefab).GetComponent<LineDoodooer>();
        line.ownerUnit = this;
        return path;
    }
    public int GetStepCount()
    {
        int steps = 0;
        steps = line.line.positionCount - 1;
        return steps;
    }
    public void DebugUnits()
    { 
        foreach (Unit unit in stats.units)
        {
            unit.DebugRole();
        }
    }
}
