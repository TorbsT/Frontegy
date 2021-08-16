using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : ITacticalAction
{
    private SelChy target;
    private bool _wasLegal;

    public SelectAction(SelChy target)
    {
        this.target = target;
        _wasLegal = legal();
    }
    public bool wasLegal() => _wasLegal;
    public bool legal() => SelMan.Instance.canSelect(target);
    public void apply()
    {
        SelMan.Instance.select(target);
    }
}
