using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TacticalHistory
{
    // Stores history of actions so they may be traced and repeated.
    // History is non-deletable
    // Actions right now are Select and Place
    // One TacticalHistory per round, per player

    public static TacticalHistory Instance { get; private set; }  // Current RoundPlan, AKA the one that may be edited
    public int roundId { get => _roundId; }
    public int ownerId { get => _ownerId; }

    [SerializeField] private int _roundId;
    [SerializeField] private int _ownerId;
    private List<ITacticalAction> actions = new List<ITacticalAction>();  // Ordered and may contain illegal actions

    // Following is only used for inspector
    [SerializeReference] private List<SelectAction> _selectActions = new List<SelectAction>();
    [SerializeReference] private List<SecondarySelectAction> _secondarySelectActions = new List<SecondarySelectAction>();

    public TacticalHistory(int roundId, int ownerId)
    {
        Instance = this;
        _roundId = roundId;
        _ownerId = ownerId;
    }
    public bool addActionAndApply(ITacticalAction action)
    {
        if (action is SelectAction a) _selectActions.Add(a);
        else if (action is SecondarySelectAction b) _secondarySelectActions.Add(b);

        actions.Add(action);
        if (action.legal()) { action.apply(); return true; }
        return false;
    }
}
