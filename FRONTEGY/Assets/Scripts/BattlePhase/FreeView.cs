using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeView : View
{
    public override bool bupdateVirtual()
    {
        cs.freeView();
        return false;
    }
}
