using System.Collections.Generic;
using UnityEngine;

public class TroopPhy : SelPhy
{  // never delete a Troop object, reuse.
    public TroopPhy()
    {

    }

    [Header("Variables")]
    [SerializeField] private Troop troop;

    [Header("System/Debug")]
    [SerializeField] MeshRenderer rndrr;
    //[SerializeField] Selectable selectable;
    
    [SerializeField] int defaultLayer = 0;
    [SerializeField] int ignoreRaycastLayer = 2;


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


    protected Troop getTroop() { return TroopPool.Instance.getClient(this); }
    public override SelChy getSelChy()
    {
        return getTroop();
    }
    public override void unstage()
    {
        TroopPool.Instance.unstage(this);
    }
}
