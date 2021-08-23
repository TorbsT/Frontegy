using System;
using System.Collections.Generic;
using UnityEngine;  // Debug.LogError()

public class Results
{
    // Results has many TroopFates and List<list<conflict
    // TroopFate has many FateEvents
    // FateEvent is split up into different events:

    // not go in conflict
    // go in conflict (border, both attack)
    // go in conflict (gather, both attack)
    // go in conflict (on attacking side)
    // go in conflict (on defending side)
    // Start is called before the first frame update

    public int maxSteps { get => _maxSteps; }

    private List<TroopState> _involvedTroopStates;
    private Coonflict allCoonflict;
    private int _roundId;
    private int _maxSteps;
    public Results(int roundId)  // correct input?
    {
        _roundId = roundId;
        allCoonflict = new Coonflict();
        _involvedTroopStates = Grid.Instance.troopStates.FindAll(match => match.roundId == roundId);
        foreach (TroopState state in Grid.Instance.troopStates)
        {
            Debug.Log(state.roundId);
        }
        Debug.Log("There were " + _involvedTroopStates.Count + " states with round " + roundId);

        // check for colliding pafs.
        // do this by going step for step and look for collisions
        int step = 0;
        while (true)
        {
            foreach (TroopState state in _involvedTroopStates)
            {
                state.prepareStepState(step);
            }

            if (everyoneDone()) break;
            Debug.Log("computing step " + step);

            // Every step, this checks for coonflict that appears during that step.
            // When calculating coonflict on a step, these parameters are necessary:
            // step: all coonflict on the same step happen simultaneously and can therefore be calculated simultaneously.
            // consequi: information on what has happened previous steps. on first step, this is an empty Consequi.
            Coonflict coonflict = getCoonflict(step);
            foreach (Conflict conflict in coonflict.getConflicts())
            {
                conflict.compute();
            }


            // Logs which coonflict have passed. Can be used to replay in scene.
            allCoonflict.merge(coonflict);
            step++;
        }
        _maxSteps = step;
        Debug.Log("Max steps is " + _maxSteps);
    }
    private bool everyoneDone()
    {
        foreach (TroopState state in _involvedTroopStates)
        {
            bool dead = state.stepStates.currentDead;
            
            Tile currentTile = state.stepStates.currentBreadcrumb.tile;
            Tile goalTile = state.paf.lastBreadcrumb.tile;
            bool arrived = currentTile == goalTile;

            Debug.Log("This troop is dead and arrived: " + dead + ", " + arrived);
            if (!dead && !arrived) return false;
        }
        return true;
    }
    public Coonflict getCoonflict(int step)
    {  // coonflict not stored in state, rather computed
        Coonflict coonflict = new Coonflict();

        int troopCount = _involvedTroopStates.Count;  // doesn't change throughout this algorithm
        if (troopCount <= 0) Debug.LogError("This should never happen");
        for (int a = 0; a < troopCount; a++)
        {
            TroopState aState = _involvedTroopStates[a];
            if (aState.stepStates.currentDead) continue;


            for (int b = a + 1; b < troopCount; b++)
            {
                TroopState bState = _involvedTroopStates[b];


                if (bState.stepStates.currentDead) continue;


                Conflict newConflict = Conflict.makeConflict(_roundId, step, aState, bState);
                if (newConflict == null) continue;  // no suitable conflict found
                coonflict.mergeConflict(newConflict);  // should merge appropriately
            }
        }
        return coonflict;
    }
    public Coonflict getStepCoonflict(int step) { return getAllCoonflict().getStepCoonflict(step); }  // TODO getters
    public Coonflict getAllCoonflict() { return allCoonflict; }
}
