using System.Collections.Generic;
using UnityEngine;

public class TroopStepStates
{
    
    public bool currentDead { get => currentState.dead; }
    public Breadcrumb currentBreadcrumb { get => currentState.currentBreadcrumb; }
    public TroopStepState currentState { get => _stepStates[currentStep]; }
    public int currentStep { get => _stepStates.Count - 1; }

    // Index of lists corresponds to step id.
    private List<TroopStepState> _stepStates = new List<TroopStepState>();
    private TroopState _state;

    public TroopStepStates(TroopState wrapper)
    {
        _state = wrapper;
    }
    public TroopStepState getStepState(int step)
    {
        if (step < 0) return getStepState(0);
        if (step >= _stepStates.Count) return getStepState(_stepStates.Count - 1);
        return _stepStates[step];
    }
    public void addConsequence(Consequence consequence)
    {
        int step = consequence.step;
        if (step != currentStep) Debug.LogError("consequence has wrong step");
        if (consequence.dies)
        {
            currentState.dead = true;
        }
    }
    public void prepareStep(int step)
    {
        if (step != currentStep + 1) Debug.LogError("Wrong step");
        _stepStates.Add(new TroopStepState(_state.paf.getBreadcrumb(step)));
    }
}
