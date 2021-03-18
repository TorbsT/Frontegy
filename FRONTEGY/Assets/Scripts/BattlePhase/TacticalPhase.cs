using System.Collections.Generic;
using UnityEngine;

public class TacticalPhase : Phase
{
    public TacticalPhase()
    {
        // runs after the construction of Phase!
        views = new List<View>();
        views.Add(new FreeView());
        type = StaticPhaseType.strategic;

    }
}
