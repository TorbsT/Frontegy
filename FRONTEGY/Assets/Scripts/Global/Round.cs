using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    public int roundId { get => _roundId; }

    private RoundManager phaseManager;
    public Phase currentPhase;
    private Player player;
    private int _roundId;
    private Results results;
    public Round(RoundManager phaseManager, int roundId)
    {
        if (phaseManager == null) Debug.LogError("IllegalArgumentException");
        this.phaseManager = phaseManager;

        if (roundId < 0) Debug.LogError("IllegalArgumentException");
        _roundId = roundId;

        player = Playyer.Instance.getFirstPlayer();
        currentPhase = new TacticalPhase(this, player);
    }

    public bool bupdate(Control c)
    {
        bool roundIsDone = false;
        bool phaseIsDone = currentPhase.bupdate(c);
        if (phaseIsDone || c.getSpaceDown())
        {
            roundIsDone = nextPhase();
        }
        return roundIsDone;
    }
    public bool skipPhase()
    {  // Provides interface from Phase
        nextPhase();
        return true;
    }
    private bool nextPhase()
    {  // returns true if round is done
        //selectionManager.ResetSelections();
        if (isThisPhase(PhaseType.battle))
        {
            return true;
        }
        else if (isLastPlayer()) weiterWeiter();
        else nextTacticalPhase();
        return false;
    }
    public Tile lastTileInPaf(Troop troop) { throw new System.NotImplementedException(); //return getRoundPlan().lastTileInPaf(troop);
                                                                                         }
    private void nextTacticalPhase()
    {
        player = Playyer.Instance.playerAfter(player);
        TacticalPhase tp = new TacticalPhase(this, player);
        currentPhase = tp;
    }
    private void weiterWeiter()
    {
        results = new Results(_roundId);  // used by BattlePhase
        player = Playyer.Instance.getNonePlayer();  // strictly necessary. UI things try to get playerId all the time
        currentPhase = new BattlePhase(this, player);
    }
    private bool isLastPlayer()
    {
        return Playyer.Instance.getLastPlayer().Equals(player);
    }
    public RoundManager getPhaseManager()
    {
        if (phaseManager == null) Debug.LogError("IllegalStateException");
        return phaseManager;
    }
    private bool isThisPhase(PhaseType phaseType)
    {
        return getPhase().isType(phaseType);
    }
    private Phase getPhase()
    {
        if (currentPhase == null) Debug.LogError("IllegalStateException");
        return currentPhase;
    }
    public Results getResults()
    {
        if (results == null) Debug.LogError("IllegalStateException: Results aren't generated yet!");
        return results;
    }
    public Player getPlayer()
    {
        if (player == null) Debug.LogError("IllegalStateException");
        return player;
    }
}
