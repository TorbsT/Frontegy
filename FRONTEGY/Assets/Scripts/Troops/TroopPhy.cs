using System.Collections.Generic;
using UnityEngine;

public class TroopPhy : Selectable
{  // never delete a Troop object, reuse.
    public TroopPhy(Roster roster) : base(roster)
    {
        rndrr = getGO().GetComponent<Renderer>();
        //rndrr.material = getGM().getCurrentPlayer().getMat();
    }

    [Header("Variables")]
    [SerializeField] private Troop troop;

    [Header("System/Debug")]
    [SerializeField] Renderer rndrr;
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
    void AdjustYByHeight()
    {
        getGO().transform.position += Vector3.up * getGO().transform.localScale[1];
    }


    public override Troop SelGetTroop() { return this.getTroop(); }
    public override void SelHover()
    {
        if (!isSelected()) rndrr.material = getGM().globalHoverMat;
    }
    public override void SelUnHover()
    {
        if (!isSelected()) rndrr.material = getGM().getCurrentPlayer().getMat();
    }
    public override void SelSelect()
    {
        rndrr.material = getGM().globalSelectMat;
        getTroop().select();  // renders tiles green

        //Conflict conflict = new Conflict(new List<TroopStats> { stats }, false);
        //conflict.DebugTroops(conflict.GetRankedTroops(new List<TroopStats> { stats }));

    }
    public override void SelUnSelect()
    {
        getTroop().unselect();
        rndrr.material = getGM().getCurrentPlayer().getMat();
    }
    public Troop getTroop() { return troop; }

    protected override void setChy(Chy chy)
    {
        troop = (Troop)chy;
    }

    public override Chy getChy()
    {
        return troop;
    }

    protected override GameObject getPrefab()
    {
        return getGM().troopGOPrefab;
    }
}
