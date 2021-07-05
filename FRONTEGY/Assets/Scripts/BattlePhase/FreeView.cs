using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeView : View
{
    public FreeView(Phase phase) : base(phase) { }
    protected override bool bupdateVirtual(Control c)
    {
        if (life == 0) getCam().focusOn(getGrid().getCenterPos3());
        getCam().freeView(c);
        getSelectionManager().freeViewUpdate(c, Player);
        return false;
    }
}
