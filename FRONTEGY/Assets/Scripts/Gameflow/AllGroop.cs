using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllGroop : Groop
{
    public static AllGroop Instance { get; private set; }

    public AllGroop() { Instance = this; }
}
