using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsPhy : MonoBehaviour
{  // Attached to a GO. connected to its corresponding Phy object.
    [SerializeReference] private Phy phy;
    [SerializeField] private bool phyConnected;
    public void set(Phy phy) { if (phy == null) Debug.LogError("IllegalArgumentException"); this.phy = phy; phyConnected = true; }
}
