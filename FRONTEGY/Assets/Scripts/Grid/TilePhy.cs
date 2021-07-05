using System.Collections.Generic;
using UnityEngine;


public class TilePhy : SelPhy
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

    void Awake()
    {
        tileCollider = GetComponent<Collider>();
    }
    public Pos2 getSurfacePos()
    {  // Returns the position if this tile's surface
        return new Pos2(planeTransform.position);  // TODO this is stupid
    }


    /// <summary>
    /// defaq
    /// </summary>
    void ResetTopMaterial()
    {
        /*
        Color color = getGM().getCurrentPlayer().getMat().color;
        Debug.Log(color);
        SetMaterial(topRenderer, getTile().getReservoir().material);
        SetColor(topRenderer, color);
        */
        
    }
    public void hideMark()
    {
        ResetTopMaterial();
    }

    Vector3 GetTileScale()
    {
        Vector3 scale = Vector3.one;
        scale.x = 1f;
        scale.y = 1f;
        scale.z = 1f;
        return scale;
    }
    private Pos2 getTilePos()
    {
        // TODO WHY IS THIS UNUSED
        Vector3 pos = Vector3.zero;
        pos.x = GetDimensionScalar(0);
        pos.y = 1f;  // TODO
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

    public Tile getTile()
    {
        return TilePool.Instance.getClient(this);
    }
    public override void unstage()
    {
        TilePool.Instance.unstage(this);
    }

    public override SelChy getSelChy()
    {
        return getTile();
    }

}