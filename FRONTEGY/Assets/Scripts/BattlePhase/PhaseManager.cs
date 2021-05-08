using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager
{
    private Grid grid;
    public Phase currentPhase;
    private Player player;
    private int round;

    public PhaseManager(Grid grid)
    {
        if (grid == null) Debug.LogError("IllegalArgumentException");
        this.grid = grid;
        currentPhase = new TacticalPhase(this);
        player = getFirstPlayer();
        round = 0;
    }

    private Player getFirstPlayer()
    {
        return grid.getFirstPlayer();
    }

    public void update()
    {
        bool phaseIsDone = currentPhase.bupdate();
        if (phaseIsDone) nextPhase();
    }
    public void attemptSkip()
    {
        nextPhase();
    }
    private void nextPhase()
    {
        //selectionManager.ResetSelections();
        if (isThisPhase(PhaseType.battle)) nextRound();
        else if (isLastPlayer()) weiterWeiter();
        else nextTacticalPhase();
    }
    private void nextRound()
    {
        round++;
        grid.UpdateTroopTiles();
        player = null;
        nextTacticalPhase();
    }
    private void nextTacticalPhase()
    {
        currentPhase = new TacticalPhase(this);
        player = grid.playerAfter(player);
    }
    private void weiterWeiter()
    {
        currentPhase = new BattlePhase(this);
        player = grid.getNonePlayer();  // strictly necessary. UI things try to get playerId all the time
    }
    private bool isLastPlayer()
    {
        return grid.isLastPlayer(player);
    }
    /*

    void NextPhase()
{
    selectionManager.ResetSelections();

    if (IsThisPhase(StaticPhaseType.weiterWeiter)) NextRound();
    else if (IsLastPlayer()) WeiterWeiter();
    else NextStrategicPhase();
    Debug.Log("Round " + phase.round + ": " + phase.type.name + " " + GetPhasePlayer().name);


}
void NextStrategicPhase()
{
    Debug.Log("Making a new phase:");
    phase = new TacticalPhase();
    Debug.Log("Made a new phase.");
    phase.playerId++;
    if (phase.playerId >= players.Count) phase.playerId = 0;
    Debug.LogWarning("nextStrategicPhase");
}
void WeiterWeiter()
{
    phase = new BattlePhase();
    phase.playerId = -1;
    stepTimeLeft = stepDuration;
    phase.steps = GetMaxSteps();
    //Debug.Log("Round " + phase.round + ": " + phase.type.name + " " + GetPhasePlayer().name);
}
void NextRound()
{
    Debug.LogWarning("nextRound");
    phase.round++;
    phase.step = 0;
    TileTracker.UpdateGridValues();
    Debug.Log("next round!");
    grid.UpdateTroopTiles();
    NextStrategicPhase();
}



    if (IsThisPhase(StaticPhaseType.weiterWeiter))
    {
        stepTimeLeft -= Time.deltaTime;
        if (stepTimeLeft <= 0f)
        {
            HandleEncounters();
            if (conflicts.Count > 0 || merges.Count > 0)
            {   // Make a conflict when:
                // Let x and y be troops from different teams.
                // x.to = y.from & x.from = y.to
                // x.to = y.to
                // ...but what about troops moving multiple tiles at once? Make them move multiple in one step, or make them skip?

                foreach (Merge merge in merges)
                {

                }
                foreach (Conflict conflict in conflicts)
                {
                    conflict.AutoResolve();
                }
            }
            else
            {
                stepTimeLeft = stepDuration;
                phase.step++;
                if (phase.step > phase.steps)
                {
                    NextPhase();
                }
            }
        }
    }
    */
    public Grid getGrid() { if (grid == null) Debug.LogError("IllegalStateException"); return grid; }
    public Player getPlayer() { return player; }
    public int getRound() { return round; }
    public bool isThisPhase(PhaseType p) { return currentPhase.getType() == p; }
}
