using System.Collections.Generic;
using UnityEngine;

public class PafChy : Chy
{  // Path was used, sorry
    private Paf paf;
    
    
    //
    public PafChy(Paf paf)
    {
        setPaf(paf);
    }


    //



    //
    public Tile lastTile { get => getPaf().lastTile; }
    public int getSteps { get => getPaf().count; }
    public Paf getPaf() { if (paf == null) Debug.LogError("IllegalStateException"); return paf; }
    public FromTo getFromTo(int step) { return getPaf().getFromTo(step); }


    //
    private void setPaf(Paf paf)
    {
        if (paf == null) Debug.LogError("IllegalArgumentException");
        this.paf = paf;
    }


    public override Phy getPhy()
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
