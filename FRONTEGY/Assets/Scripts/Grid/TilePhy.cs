using System.Collections.Generic;
using UnityEngine;


public class TilePhy : SelPhy
{
    public Transform surfaceTransform { get { if (_surfaceTransform == null) Debug.LogError("InspectorException: Assign surfaceTransform on TilePhy"); return _surfaceTransform; } }
    public Transtatic surfaceTranstatic { get => _surfaceTranstatic; }


    [Header("Variables")]
    [SerializeField] Transform _surfaceTransform;
    [SerializeField] Rigidbody _rb;
    private Transtatic _surfaceTranstatic;

    protected override void Awake()
    {
        base.Awake();
        _surfaceTranstatic = new Transtatic(_surfaceTransform, transive);
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
    public override bool tryRagdollMode()
    {
        _rb.isKinematic = false;
        _rb.AddForce(new Vector3(0f, 1000f, 0f));
        return true;
    }
    public override bool tryUnragdollMode()
    {
        _rb.isKinematic = true;
        transive.transformExternallyChanged();
        return true;
    }

}