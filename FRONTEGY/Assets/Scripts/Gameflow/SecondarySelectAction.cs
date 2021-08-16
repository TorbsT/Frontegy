using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondarySelectAction : ITacticalAction
{
    private SelChy target;
    private bool _wasLegal;

    public SecondarySelectAction(SelChy target)
    {
        this.target = target;
        _wasLegal = legal();
    }
    public bool wasLegal() => _wasLegal;
    public bool legal() => SelMan.Instance.canSecondarySelect(target);
    public void apply()
    {
        SelMan.Instance.secondarySelect(target);
    }
}
