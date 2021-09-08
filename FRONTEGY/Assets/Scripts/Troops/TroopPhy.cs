using System.Collections.Generic;
using UnityEngine;

public class TroopPhy : SelPhy
{  // never delete a Troop object, reuse.

    public Transform tooltipTransform { get => _tooltipTransform; }
    public SpriteRenderer roleTooltipRenderer { get => _roleTooltipRenderer; }

    [Header("Variables")]
    [SerializeField] private Troop troop;
    [SerializeField] private Rigidbody _troopRb;
    [SerializeField] private Rigidbody _crownRb;
    [SerializeField] private Transform _tooltipTransform;
    [SerializeField] private bool _isRagdoll;
    [SerializeField] private SpriteRenderer _roleTooltipRenderer;
    [SerializeField] private Transive _roleTooltipTransive;

    [Header("System/Debug")]
    [SerializeField] MeshRenderer rndrr;
    //[SerializeField] Selectable selectable;
    
    [SerializeField] int defaultLayer = 0;
    [SerializeField] int ignoreRaycastLayer = 2;


    protected override void Awake()
    {
        base.Awake();
        _roleTooltipTransive = new Transive(_roleTooltipRenderer.transform);
        
    }
    private void Start()
    {
        Transive t = Cam.Instance.transive;
        _roleTooltipTransive.rotp.setParent(t);
    }

    /*
    public void ManualUpdate()
    {
        if (!isInstantiated()) Debug.LogError("ERROR: Troop is not instantiated");
        SetLayer();
        getGO().transform.localScale = new Vector3(troop.scale, troop.scale, troop.scale);
        if (TileTracker.GetTileById(troop.parentTileId) == null) return;
    }
    */
    /*
    void SetPosition()
    {
        if (gameMaster.isThisPhase(PhaseType.tactical) || line == null || line.line.positionCount == 0)
        {
            selGO.transform.position = TileTracker.GetTileById(troop.parentTileId).GetSurfacePos();
        }
        else if (gameMaster.isThisPhase(PhaseType.battle))
        {  CRITICAL
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
    */
    /*
    public void weiterUpdate(int step, Slid slid)
    {
        getGO().layer = ignoreRaycastLayer;  // MAYBUG remember to add another for not weiterupdate
        //if (noStrollAvailableAt(step)) return;  // if no stroll available: suppose position is correct, don't update.  // WHY WOULD YOU HAVE THIS? commented for now.
        FromTo ft = getTroop().getFromTo(step);
        Tile fromT = ft.getFrom();
        Tile toT = ft.getTo();
        Pos2 fromP = fromT.getPos();
        Pos2 toP = toT.getPos();
        Pos2 lerped = Pos2.lerp(fromP, toP, slid);
        setPos2(lerped);
        //updateVisual();
    }
    */
    void AdjustYByHeight()
    {
        //getGO().transform.position += Vector3.up * getGO().transform.localScale[1];
    }
    private void FixedUpdate()
    {
        //var rot = Quaternion.FromToRotation(transform.up, Vector3.up);
        //_troopRb.AddTorque(new Vector3(rot.x, rot.y, rot.z) * 1500*Time.fixedDeltaTime);
        if (!connected) return;
        Quaternion deltaQuat = Quaternion.FromToRotation(_troopRb.transform.up, Vector3.up);

        Vector3 axis;
        float angle;
        deltaQuat.ToAngleAxis(out angle, out axis);
        if (angle > 10f) angle = 100f;

        float dampenFactor = 10f; // this value requires tuning
        _troopRb.AddTorque(-_troopRb.angularVelocity * dampenFactor, ForceMode.Acceleration);

        float adjustFactor = 1f; // this value requires tuning
        _troopRb.AddTorque(axis.normalized * angle * adjustFactor, ForceMode.Acceleration);
    }

    protected Troop getTroop() { return TroopPool.Instance.getClient(this); }
    public override SelChy getSelChy()
    {
        return getTroop();
    }
    public override void unstage()
    {
        TroopPool.Instance.unstage(this);
    }
    public override bool tryRagdollMode()
    {
        _isRagdoll = true;
        if (_crownRb != null) _crownRb.isKinematic = false;
        //_troopRb.constraints = RigidbodyConstraints.None;
        _troopRb.isKinematic = false;
        return true;
    }
    public override bool tryUnragdollMode()
    {
        _isRagdoll = false;
        if (_crownRb != null) _crownRb.isKinematic = true;
        _troopRb.isKinematic = true;
        //_troopRb.constraints = RigidbodyConstraints.None; //RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY;
        return true;
    }

}
