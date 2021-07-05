using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLinker : MonoBehaviour
{  // Makes it possible to see Cam fields on the Camera GameObject.
    [SerializeReference] private Cam cam;

    public void setCam(Cam cam)
    {
        if (cam == null) Debug.LogError("IllegalArgumentException");
        if (this.cam != null) Debug.LogError("IllegalStateException");
        this.cam = cam;
    }
}
