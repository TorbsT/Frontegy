using System.Collections.Generic;
using UnityEngine;

public class TacticalPhase : Phase
{
    private View v;
    private TacticalHistory history;

    public TacticalPhase(Round round, Player phasePlayer) : base(round, phasePlayer)
    {
        // runs after the construction of Phase!
        v = new FreeView(this);
        setType(PhaseType.tactical);


        // TODO NEXT URGENT
        // when tactical phase starts, get caard of the phase player and put them into uicaard.
        // how is phase player stored again...?
        history = new TacticalHistory(roundId, phasePlayer.id);
        Grid.Instance.tacticalHistories.Add(history);
    }
    public List<Card> getCaardToShow()
    {
        return AllCaard.Instance.getCards().FindAll(match => match.state.ownerId == phaseOwnerId);
    }

    protected override void startAbstra()
    {
        UIManager.Instance.tacticalStart(this);
        foreach (Troop troop in Grid.Instance.allTroops)
        {
            troop.tacticalStart();
        }
    }
    protected override bool bupdateAbstra(Control c)
    {
        bool done = false;
        v.bupdate(c);
        UIManager.Instance.tacticalUpdate();
        return done;
    }
}
