using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trans
{  // Shared between Chy and Phy - should therefore be class
    public Pos3 pos3 { get { return _pos3; } set { _pos3 = value; } }
    public Quaternion rot { get { return _rot; } set { _rot = value; } }

    [SerializeField] private Pos3 _pos3;
    [SerializeField] private Quaternion _rot;

    public Trans() { }
    public Trans(Pos3 p3) { this.pos3 = p3; }
    public Trans(Pos3 p3, Quaternion rot) { this.pos3 = p3; this.rot = rot; }

    public void setPos3RelativeTo(Transform parent, Vector3 desiredLocalPos)
    {
        pos3 = new Pos3(parent.TransformPoint(desiredLocalPos));
    }
}
