using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round
{
    private PhaseManager phaseManager;
    public Phase currentPhase;
    private Player player;
    private int roundId;
    private RoundPlan roundPlan;
    private Results results;
    public Round(PhaseManager phaseManager, int roundId)
    {
        if (phaseManager == null) Debug.LogError("IllegalArgumentException");
        this.phaseManager = phaseManager;

        if (roundId < 0) Debug.LogError("IllegalArgumentException");
        this.roundId = roundId;

        roundPlan = new RoundPlan();
        player = getFirstPlayer();
        currentPhase = new TacticalPhase(this, player);
    }

    public int getRoundId()
    {
        return roundId;
    }
    private Player getFirstPlayer()
    {
        return getPhaseManager().getFirstPlayer();
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
    public Tile lastTileInPaf(Troop troop) { return getRoundPlan().lastTileInPaf(troop); }

    public Cam getCam()
    {
        return getPhaseManager().getCam();
    }

    public Grid getGrid()
    {
        return getPhaseManager().getGrid();
    }
    public Groop getAllGroop() { return getPhaseManager().getAllGroop(); }
    public UIManager getUiManager()
    {
        return getPhaseManager().getUiManager();
    }

    public SelMan getSelectionManager()
    {
        return getPhaseManager().getSelectionManager();
    }

    private void nextTacticalPhase()
    {
        player = getPhaseManager().playerAfter(player);
        currentPhase = new TacticalPhase(this, player);
    }
    private void weiterWeiter()
    {
        results = new Results(this);  // used by BattlePhase
        player = getPhaseManager().getNonePlayer();  // strictly necessary. UI things try to get playerId all the time
        currentPhase = new BattlePhase(this, player);
    }
    private bool isLastPlayer()
    {
        return getPhaseManager().isLastPlayer(player);
    }
    public PhaseManager getPhaseManager()
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
    public TroopPlan getTroopPlan(Troop troop)
    {
        return getRoundPlan().getTroopPlan(troop);
    }
    public RoundPlan getRoundPlan()
    {
        if (roundPlan == null) Debug.LogError("IllegalStateException");
        return roundPlan;
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
