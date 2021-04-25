using System.Collections.Generic;
using UnityEngine;

public class TacticalPhase : Phase
{
    View v;

    public TacticalPhase()
    {
        // runs after the construction of Phase!
        v = new FreeView();
        setType(PhaseType.tactical);
    }

    protected override bool bupdateVirtual()
    {
        bool done = false;

        return done;
    }
}
