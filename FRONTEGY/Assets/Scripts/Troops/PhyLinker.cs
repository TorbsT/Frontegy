using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyLinker : Linker
{  // Attached to a GO. connected to its corresponding Phy object.
    // 21/06/09 - now inherits from Linker
    // 21/06/28 - moved to Phy, Phy now derives from MonoBehaviour

    //public void link(Phy phy) { if (phy == null) Debug.LogError("IllegalArgumentException"); this.phy = phy; phyConnected = true; }

}
