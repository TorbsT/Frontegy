using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager
{
    private GameMaster gm;
    public Phase currentPhase;
    private int playerId;
    private int round;

    public PhaseManager()
    {
        gm = GameMaster.GetGM();
        currentPhase = new TacticalPhase();
        playerId = 0;
        round = 0;
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
        gm.grid.UpdateTroopTiles();
        playerId = -1;
        nextTacticalPhase();
    }
    private void nextTacticalPhase()
    {
        currentPhase = new TacticalPhase();
        playerId++;
    }
    private void weiterWeiter()
    {
        currentPhase = new BattlePhase();
        playerId = -1;  // strictly necessary. UI things try to get playerId all the time
    }
    private bool isLastPlayer()
    {
        return (playerId == gm.getPlayersCount()-1);
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

    public int getPlayerId() { return playerId; }
    public int getRound() { return round; }
    public bool isThisPhase(PhaseType p) { return currentPhase.getType() == p; }
}
