using System.Collections.Generic;

public class BattlePhase : Phase
{

    public BattlePhase()
    {
        views = new List<View>();
        views.Add(new ViewBattles());
        type = StaticPhaseType.weiterWeiter;
    }
}
