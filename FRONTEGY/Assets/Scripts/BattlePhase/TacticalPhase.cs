using System.Collections.Generic;
using UnityEngine;

public class TacticalPhase : Phase
{
    private View v;
    private RoundPlan phasePlan;

    public TacticalPhase(Round round, Player phasePlayer) : base(round, phasePlayer)
    {
        // runs after the construction of Phase!
        v = new FreeView(this);
        setType(PhaseType.tactical);

        
        // TODO NEXT URGENT
        // when tactical phase starts, get caard of the phase player and put them into uicaard.
        // how is phase player stored again...?
    }
    public Results getResults(RoundPlan phasePlan)
    {
        return null;
    }
    public Caard getCaardToShow()
    {
        return getGrid().getCaardInHandOf(getPhasePlayer());
    }

    protected override void startAbstra()
    {
        getUiManager().tacticalStart(this);
        getAllGroop().tacticalStart();
    }
    protected override bool bupdateAbstra(Control c)
    {
        bool done = false;
        v.bupdate(c);
        getUiManager().tacticalUpdate();
        return done;
    }
}
