using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Control
{  // Rapidly constructed, therefore struct
   // not static since a list of Control might be suitable
   // Also lets AI agents play, although that will certainly never be implemented.
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private bool m0Down;
    [SerializeField] private bool m1Down;

    [SerializeField] private bool oDown;
    [SerializeField] private bool rDown;
    [SerializeField] private bool spaceDown;
    [SerializeField] private float horAxis;
    [SerializeField] private float verAxis;



    public Control(bool mustHaveAParameterForSomeReason = true)
    {
        mousePosition = Input.mousePosition;
        m0Down = Input.GetMouseButtonDown(0);
        m1Down = Input.GetMouseButtonDown(1);

        oDown = Input.GetKeyDown("o");
        rDown = Input.GetKeyDown("r");
        spaceDown = Input.GetKeyDown("space");
        horAxis = Input.GetAxis("Horizontal");
        verAxis = Input.GetAxis("Vertical");
    }
    public Vector3 getMousePosition() { return mousePosition; }
    public bool getM0Down() { return m0Down; }
    public bool getM1Down() { return m1Down; }

    public bool getODown() { return oDown; }
    public bool getRDown() { return rDown; }
    public bool getSpaceDown() { return spaceDown; }
    public float getHorAxis() { return horAxis; }
    public float getVerAxis() { return verAxis; }

}
