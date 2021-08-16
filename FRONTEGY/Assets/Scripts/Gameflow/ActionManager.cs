using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager
{
    // Provides a standardized way for the user to do stuff
    // Raw input has some logic, but only to translate to moveTroop and summonTroop

    private static int moveCode = 0;
    public static ActionManager Instance { get; private set; }
    public ActionManager()
    {
        Instance = this;
    }

    public void rawAction(List<int> action)
    {
        if (action == null) Debug.LogError("don't know if this should be legal");
        // Map to another method
    }

    public bool nextPhase()
    {
        // TODO
        return false;
    }
    public bool doAction(ITacticalAction action)
    {
        return TacticalHistory.Instance.addActionAndApply(action);
    }
}
