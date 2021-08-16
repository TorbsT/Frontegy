using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transtatic : Trans
{
    public Transtatic(Transform transform)
    {
        if (transform == null) Debug.LogError("IllegalArgumentException");
        this.transform = transform;
    }
    protected override void computeWorld()
    {
        
    }

}
