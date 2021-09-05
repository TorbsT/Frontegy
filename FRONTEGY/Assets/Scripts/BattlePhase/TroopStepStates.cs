using System.Collections.Generic;
using UnityEngine;

public class TroopStepStates
{
    public int count { get => _stepStates.Count; }
    public bool currentDead { get => currentState.dead; }
    public Breadcrumb currentBreadcrumb { get => currentState.currentBreadcrumb; }
    public TroopStepState currentState { get => _stepStates[currentStep]; }
    public int currentStep { get => _stepStates.Count - 1; }

    // Index of lists corresponds to step id.
    private List<TroopStepState> _stepStates = new List<TroopStepState>();
    private TroopState _state;

    public TroopStepStates(TroopState state)
    {
        _state = state;
    }
    public TroopStepState getStepState(int step)
    {
        if (_stepStates.Count == 0) Debug.LogError("Troop has 0 step states but tried accessing");
        if (step < 0) step = 0;
        if (step >= _stepStates.Count) { step = _stepStates.Count - 1; Debug.Log("Step was over stepcount"); }
        return _stepStates[step];
    }
    public void addConsequence(Consequence consequence)
    {
        int step = consequence.step;
        if (step != currentStep) Debug.LogError("consequence has wrong step");
        if (consequence.dies)
        {
            currentState.dead = true;
            Debug.Log("Set dead on stepstate "+currentState);
        }
            Debug.Log("yASdhasdhdh");
    }
    public void prepareStep(int step)
    {
        if (step != currentStep + 1) Debug.LogError("Wrong step");
        Breadcrumb bc = _state.paf.getBreadcrumb(step);
        Debug.Log("Prepared step " + step + ", " + bc);
        _stepStates.Add(new TroopStepState(bc));
    }
}
