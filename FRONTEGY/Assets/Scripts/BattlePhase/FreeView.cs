using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeView : View
{
    public FreeView(Phase phase) : base(phase) { }
    protected override bool bupdateVirtual(Control c)
    {
        if (life == 0) cam.focusOn(grid.getCenterPos3());
        cam.freeView(c);
        SelMan.Instance.freeViewUpdate(c, player);
        return false;
    }
}
