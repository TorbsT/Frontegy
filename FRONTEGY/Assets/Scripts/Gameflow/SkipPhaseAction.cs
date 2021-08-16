using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipPhaseAction : ITacticalAction
{
    private bool _wasLegal;
    public SkipPhaseAction()
    {
        _wasLegal = legal();
    }

    public bool wasLegal() => _wasLegal;
    public bool legal() => Phase.Instance.canSkipNow;
    public void apply()
    {
        Phase.Instance.trySkip();
    }
}
