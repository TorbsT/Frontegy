using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsPhy : MonoBehaviour
{  // Attached to a GO. connected to its corresponding Phy object.
    [SerializeReference] private Phy phy;

    public void set(Phy phy) { this.phy = phy; }
}
