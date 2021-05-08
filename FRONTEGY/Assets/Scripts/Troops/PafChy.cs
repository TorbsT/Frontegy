using System.Collections.Generic;
using UnityEngine;

public class PafChy : Chy
{  // Path was used, sorry
    private Paf paf;
    private PafPhy pafPhy;
    
    
    //
    public PafChy(Grid grid, Paf paf) : base(grid)
    {
        setPaf(paf);
    }


    //



    //
    public int getPafCount() { return getPaf().GetBreadcrumbCount(); }
    public Paf getPaf() { return paf; }


    //
    private void setPaf(Paf paf)
    {
        if (paf == null) Debug.LogError("IllegalArgumentException");
        this.paf = paf;
    }


    protected override Phy getPhy()
    {
        return pafPhy;
    }
    protected override void connect()
    {
        if (pafPhy != null) Debug.LogError("PafChy already staged.");
        pafPhy = PafRoster.sgetUnstagedPhy();
    }
    protected override void disconnect()
    {
        pafPhy = null;  // TODO this is bad and not good
    }
}
