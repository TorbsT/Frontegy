using System.Collections.Generic;
using UnityEngine;

public class PafChy : Chy
{  // Path was used, sorry
    private Paf paf;
    
    
    //
    public PafChy(Grid grid, Paf paf) : base(grid)
    {
        setPaf(paf);
    }


    //



    //
    public Tile lastTile() { return getPaf().lastTile(); }
    public int getSteps() { return getPaf().GetBreadcrumbCount(); }
    public Paf getPaf() { if (paf == null) Debug.LogError("IllegalStateException"); return paf; }
    public FromTo getFromTo(int step) { return getPaf().getFromTo(step); }


    //
    private void setPaf(Paf paf)
    {
        if (paf == null) Debug.LogError("IllegalArgumentException");
        this.paf = paf;
    }


    protected override Phy getPhy()
    {
        return PafPool.Instance.getHost(this);
    }

    public override void stage()
    {
        PafPool.Instance.stage(this);
    }

    public override void unstage()
    {
        PafPool.Instance.unstage(this);
    }
}
