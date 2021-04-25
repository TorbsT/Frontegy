using UnityEngine;

public class TilePhy : Selectable
{
    enum SideMode
    {
        none,
        hover,
        select
    }
    enum TopMode
    {
        none,
        path
    }

    [Header("Variables")]
    bool swapRenderers = false;

    [Header("System")]
    SideMode sideMode = SideMode.none;
    TopMode topMode = TopMode.none;
    Collider tileCollider;
    Transform planeTransform;
    Mesh trianglePrismMesh;
    Mesh squarePrismMesh;
    Mesh hexagonPrismMesh;
    Renderer sideRenderer;
    Renderer topRenderer;
    private Tile tile;

    public TilePhy(Roster roster) : base(roster)
    {
        getGO().transform.localScale = new Vector3(1f, 0f, 1f);
        sideRenderer = getGO().GetComponent<Renderer>();
        topRenderer = getGO().transform.GetChild(0).GetComponentInChildren<Renderer>();  // TODO uh oh
        planeTransform = getGO().transform.GetChild(0);  // UH FUCKING OH
        if (topRenderer == null) Debug.LogError("ERROR: couldn't find topRenderer using child index 0");
        tileCollider = getGO().GetComponent<Collider>();
        if (swapRenderers) SwapRenderers();
    }

    public void ManualUpdate()
    {
        if (!hasGO()) Debug.LogError("ERROR: Tile is not instantiated");

        bool show = getTile().isActive();
        topRenderer.enabled = show;
        sideRenderer.enabled = show;
        tileCollider.enabled = show;
        getGO().transform.localScale = Vector3.Lerp(getGO().transform.localScale, GetTileScale(), getGM().tileCreateAnimationSpeed*Time.deltaTime*60f);
        updatePos();
    }
    public Pos2 getSurfacePos()
    {  // Returns the position if this tile's surface
        return new Pos2(planeTransform.position);  // TODO this is stupid
    }
    void SwapRenderers()
    {
        Renderer temp = sideRenderer;
        sideRenderer = topRenderer;
        topRenderer = temp;
    }


    /// <summary>
    /// defaq
    /// </summary>
    void ResetAllMaterials()
    {
        ResetSideMaterial();
        ResetTopMaterial();
    }
    void ResetTopMaterial()
    {
        SetMaterial(topRenderer, getTile().reservoir.material);
        SetColor(topRenderer, getGM().getCurrentPlayer().getMat().color);
    }
    void ResetSideMaterial()
    {
        SetMaterial(sideRenderer, getTile().reservoir.material);
    }
    void SetMaterial(Renderer r, Material mat)
    {
        r.material = mat;
    }
    void SetColor(Renderer renderer, Color color)
    {
        renderer.material.color = color;
    }
    /// <summary>
    /// dafaq
    /// </summary>
    /// 
    public override Tile SelGetTile() {return this.getTile();}
    public override void SelHover()
    {
        if (sideMode != SideMode.select)
        {
            sideMode = SideMode.hover;
            sideRenderer.material = getGM().globalHoverMat;
        }
    }
    public override void SelUnHover()
    {
        if (sideMode != SideMode.select)
        {
            sideMode = SideMode.none;
            ResetSideMaterial();
        }
    }
    public override void SelSelect()
    {
        if (true)
        {
            sideMode = SideMode.select;
            sideRenderer.material = getGM().globalSelectMat;
        }
    }
    public override void SelUnSelect()
    {
        if (true)
        {
            sideMode = SideMode.none;
            ResetSideMaterial();
        }
    }
    public void showMark(Breadcrumb breadcrumb)
    {
        topRenderer.material = getGM().breadcrumbMat;

        float timeOffset = ((float)breadcrumb.GetStepsRemaining()) * 0.1f;
        topRenderer.material.SetFloat("TimeOffset", timeOffset);
    }
    public void hideMark()
    {
        ResetTopMaterial();
    }

    Vector3 GetTileScale()
    {
        Vector3 scale = Vector3.one;
        scale.x = getGM().grid.tileSize;
        scale.y = getTile().height;
        scale.z = getGM().grid.tileSize;
        return scale;
    }
    private Pos2 getTilePos()
    {
        // TODO WHY IS THIS UNUSED
        Vector3 pos = Vector3.zero;
        pos.x = GetDimensionScalar(0);
        pos.y = getGO().transform.localScale.y/2f;  // TODO
        pos.z = GetDimensionScalar(1);
        return new Pos2(pos);
    }
    float GetDimensionScalar(int dimension)
    {
        return 0;/* TODO
        float scalar;
        scalar = 0;
        float realGapSize = (getGM().grid.tileSize * getGM().grid.tileGap);
        float extendedTileSize = (getGM().grid.tileSize + realGapSize);
        scalar += getGM().grid.gridSize[dimension] * extendedTileSize * getGM().grid.currentGrid.offset; // sets to expand from bottom left or expand from center. relative to grid size
        scalar += getTile().getPos().getV3()[dimension] * extendedTileSize; // places tiles on different parts of the grid. relative to grid position
        scalar += extendedTileSize * getGM().grid.currentGrid.globalOffset;  // moves all tiles relative to tile size
        return scalar;
        */
    }

    private Tile getTile() { if (tile == null) Debug.LogError("TilePhy has no Tile"); return tile; }

    protected override void setChy(Chy chy)
    {
        tile = (Tile)chy;
        ResetAllMaterials();
    }

    protected override Chy getChy()
    {
        return getTile();
    }

    protected override GameObject getPrefab()
    {
        return getGM().tileGOPrefab;
    }
}