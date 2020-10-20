using UnityEngine;

public class Tile : Selectable
{
    bool isInitialized = false;
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
    public float lineHeight;
    [SerializeField] float createAnimationSpeed;
    [SerializeField] bool swapRenderers = false;
    [SerializeField] Material hoverMat;
    [SerializeField] Material selectMat;
    [SerializeField] Material pathableMat;

    [Header("System")]
    public GameObject tileGO;
    SideMode sideMode = SideMode.none;
    TopMode topMode = TopMode.none;
    [System.NonSerialized] public GameMaster gameMaster;
    //[SerializeField] Selectable selectable;
    [SerializeField] Collider tileCollider;
    [SerializeField] Transform planeTransform;
    //[SerializeField] GameObject modeller;
    [SerializeField] Mesh trianglePrismMesh;
    [SerializeField] Mesh squarePrismMesh;
    [SerializeField] Mesh hexagonPrismMesh;
    [SerializeField] Renderer sideRenderer;
    [SerializeField] Renderer topRenderer;

    void ManualStart()
    {
        isInitialized = true;

        transform.localScale = new Vector3(1f, 0f, 1f);
        if (swapRenderers) SwapRenderers();
        ResetAllMaterials();
    }
    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
        topRenderer.enabled = geo.isActive;
        sideRenderer.enabled = geo.isActive;
        tileCollider.enabled = geo.isActive;
        transform.position = GetTilePos();
        transform.localScale = Vector3.Lerp(transform.localScale, GetTileScale(), createAnimationSpeed*Time.deltaTime);
        
    }
    public Vector3 GetSurfacePos()
    {
        return planeTransform.position;
    }
    public Vector3 GetPosAtHeightOf(Tile t)
    {
        return new Vector3(GetSurfacePos().x, t.GetSurfacePos().y, GetSurfacePos().z);
    }
    public Vector3 GetLinePosAtHeightOf(Tile t)
    {
        return new Vector3(GetPosAtHeightOf(t).x, GetPosAtHeightOf(t).y+lineHeight, GetPosAtHeightOf(t).z);
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
        SetColor(topRenderer, gameMaster.GetTeamOfPlayer(geo.playerId).material.color);
    }
    void ResetSideMaterial()
    {
        SetMaterial(sideRenderer, geo.reservoir.material);
    }
    void SetMaterial(Renderer renderer, Material mat)
    {
        renderer.material = mat;
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
            sideRenderer.material = hoverMat;
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
            sideRenderer.material = selectMat;
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
        topRenderer.material = pathableMat;

        float timeOffset = ((float)breadcrumb.stepsRemaining) * 0.1f;
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
        GetComponent<MeshFilter>().mesh = mesh;
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
        pos.y = transform.localScale.y/2f;
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