using UnityEngine;

public class Tile : Selectable
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
    public Geo initialGeo;
    public Geo geo;
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

    public override void Instantiate()
    {
        Instantiate2(this.GetType());
        geo = initialGeo;
        selGO.transform.localScale = new Vector3(1f, 0f, 1f);
        sideRenderer = selGO.GetComponent<Renderer>();
        topRenderer = selGO.transform.GetChild(0).GetComponentInChildren<Renderer>();  // uh oh
        planeTransform = selGO.transform.GetChild(0);  // UH FUCKING OH
        if (topRenderer == null) Debug.LogError("ERROR: couldn't find topRenderer using child index 0");
        tileCollider = selGO.GetComponent<Collider>();
        if (swapRenderers) SwapRenderers();
        ResetAllMaterials();
    }
    public void ManualUpdate()
    {
        if (!isInstantiated) Debug.LogError("ERROR: Tile is not instantiated");

        topRenderer.enabled = geo.isActive;
        sideRenderer.enabled = geo.isActive;
        tileCollider.enabled = geo.isActive;
        selGO.transform.position = GetTilePos();
        selGO.transform.localScale = Vector3.Lerp(selGO.transform.localScale, GetTileScale(), gameMaster.tileCreateAnimationSpeed*Time.deltaTime*60f);
        
    }
    public Vector3 GetSurfacePos()
    {  // Returns the position if this tile's surface
        return planeTransform.position;
    }
    public Vector3 GetPosAtHeightOf(Tile t)
    {  // Returns the position if this tile's surface, but at the height of another tile's surface
        return new Vector3(GetSurfacePos().x, t.GetSurfacePos().y, GetSurfacePos().z);
    }
    public Vector3 GetLinePosAtHeightOf(Tile t)
    {
        return new Vector3(GetPosAtHeightOf(t).x, GetPosAtHeightOf(t).y+gameMaster.tileLineHeight, GetPosAtHeightOf(t).z);
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
        SetMaterial(topRenderer, geo.reservoir.material);
        SetColor(topRenderer, gameMaster.GetPhasePlayer().mat.color);
    }
    void ResetSideMaterial()
    {
        SetMaterial(sideRenderer, geo.reservoir.material);
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


    public override Tile SelGetTile() {return this;}
    public override void SelHover()
    {
        if (sideMode != SideMode.select)
        {
            sideMode = SideMode.hover;
            sideRenderer.material = gameMaster.globalHoverMat;
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
            sideRenderer.material = gameMaster.globalSelectMat;
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
    public void ShowBreadcrumb(Breadcrumb breadcrumb)
    {
        topRenderer.material = gameMaster.breadcrumbMat;

        float timeOffset = ((float)breadcrumb.GetStepsRemaining()) * 0.1f;
        topRenderer.material.SetFloat("TimeOffset", timeOffset);
    }
    public void UnShowBreadcrumb()
    {
        ResetTopMaterial();
    }

    public void VerifyMesh()  // CONSUMES A LOT OF PROCESSING POWER!
    {
        switch (gameMaster.grid.tileShape)
        {
            case Grid.TileShape.none:
                SetMesh(null);
                break;
            case Grid.TileShape.trianglePrism:
                SetMesh(trianglePrismMesh);
                break;
            case Grid.TileShape.squarePrism:
                SetMesh(squarePrismMesh);
                break;
            case Grid.TileShape.hexagonPrism:
                SetMesh(hexagonPrismMesh);
                break;
        }
    }
    void SetMesh(Mesh mesh)
    {
        //selGO.GetComponent<MeshFilter>().mesh = mesh;
    }

    Vector3 GetTileScale()
    {
        Vector3 scale = Vector3.one;
        scale.x = gameMaster.grid.tileSize;
        scale.y = geo.height;
        scale.z = gameMaster.grid.tileSize;
        return scale;
    }
    Vector3 GetTilePos()
    {
        Vector3 pos = Vector3.zero;
        pos.x = GetDimensionScalar(0);
        pos.y = selGO.transform.localScale.y/2f;
        pos.z = GetDimensionScalar(1);
        return pos;
    }
    float GetDimensionScalar(int dimension)
    {
        float scalar;
        scalar = 0;
        float realGapSize = (gameMaster.grid.tileSize * gameMaster.grid.tileGap);
        float extendedTileSize = (gameMaster.grid.tileSize + realGapSize);
        scalar += gameMaster.grid.gridSize[dimension] * extendedTileSize * gameMaster.grid.currentGrid.offset; // sets to expand from bottom left or expand from center. relative to grid size
        scalar += geo.gridPos[dimension] * extendedTileSize; // places tiles on different parts of the grid. relative to grid position
        scalar += extendedTileSize * gameMaster.grid.currentGrid.globalOffset;  // moves all tiles relative to tile size
        return scalar;
    }
}