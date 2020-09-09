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
    public List<Tile> tilesInRange;
    
    [SerializeField] int defaultLayer = 0;
    [SerializeField] int ignoreRaycastLayer = 2;

    void ManualStart()
    {
        isInitialized = true;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        RefreshStats();
        GetComponent<Renderer>().material = gameMaster.GetTeam(stats.teamId).material;
        Debug.Log(stats.teamId);
    }
    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
        RefreshStats();
        SetLayer();
        transform.localScale = new Vector3(stats.scale, stats.scale, stats.scale);
        if (stats.parentTile == null) return;
        SetPosition();
        
    }
    void SetPosition()
    {
        if (gameMaster.phase.type.name == "strategic" || line.line.positionCount == 0)
        {
            transform.position = stats.parentTile.GetSurfacePos();
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
    public List<Tile> PlanMovement(Tile toTile)
    {
        Debug.Log("Searching for tile with id " + toTile.geo.id + ", gridPos " + gameMaster.grid.GetGridPos(toTile.geo.id));

        List<Tile> tilesInPF = Pathfinding.GetUntardedPath(Pathfinding.GetPaths(stats.parentTile, stats.walkRange, new List<Vector2Int>(), toTile.geo.id), toTile);
        Debug.Log("Found " + tilesInPF.Count);
        foreach (Tile node in tilesInPF)
        {
            //Debug.Log(gameMaster.grid.GetGridPos(node.geo.id));
        }

        if (tilesInPF.Count == 0) return null;
        Tile destinationTile = tilesInPF[tilesInPF.Count - 1];
        Debug.Log(gameMaster.grid.gridVers[0]);
        SetStats().path = tilesInPF;
        if (line == null) line = Instantiate(linePrefab).GetComponent<LineDoodooer>();
        line.ownerUnit = this;
        return tilesInPF;
    }
    public void Select()
    {
        renderer.material = unitSelectMat;
        tilesInRange = Pathfinding.GetTilesInRange(stats.parentTile, stats.walkRange, new List<Vector2Int>());
        foreach (Tile bruh in tilesInRange)
        {
            //Debug.Log(gameMaster.grid.GetGridPos(bruh.geo.id));
            bruh.ShowPath();
        }
    }
    public void UnSelect()
    {
        foreach (Tile tile in tilesInRange)
        {
            tile.UnShowPath();
        }
        tilesInRange = new List<Tile>();
        renderer.material = gameMaster.GetTeam(stats.teamId).material;
    }
}
