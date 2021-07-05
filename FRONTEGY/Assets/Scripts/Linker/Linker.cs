using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour
{
    [SerializeField] private GFX gfx;

    public GFX getGFX()
    {
        if (gfx == null) Debug.LogError("IllegalStateException");
        return gfx;
    }
}
