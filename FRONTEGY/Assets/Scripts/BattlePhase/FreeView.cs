using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeView : View
{
    public FreeView(Phase phase) : base(phase) { }
    protected override bool bupdateVirtual()
    {
        cs.freeView();
        return false;
    }
}
