using System.Collections.Generic;
using UnityEngine;

public class TacticalPhase : Phase
{
    View v;

    public TacticalPhase(PhaseManager pm) : base(pm)
    {
        // runs after the construction of Phase!
        v = new FreeView(this);
        setType(PhaseType.tactical);
    }



    protected override bool bupdateAbstra()
    {
        bool done = false;
        v.bupdate();
        return done;
    }
}
