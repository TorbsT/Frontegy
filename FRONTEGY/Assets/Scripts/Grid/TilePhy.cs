using System.Collections.Generic;
using UnityEngine;


public class TilePhy : SelPhy
{
    public Transform surfaceTransform { get { if (_surfaceTransform == null) Debug.LogError("InspectorException: Assign surfaceTransform on TilePhy"); return _surfaceTransform; } }

    [Header("Variables")]
    [SerializeField] Transform _surfaceTransform;

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