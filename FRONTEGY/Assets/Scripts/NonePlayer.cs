using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "PlayerNone", menuName = "ScriptableObjects/PlayerNone", order = 2)]
public class NonePlayer : Player
{
    public static NonePlayer Instance { get; private set; }

    public void init() { Instance = this; }
}
