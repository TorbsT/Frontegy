using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NonePlayer : Player
{
    public static NonePlayer Instance;

    public void init() { Instance = this; }
}
