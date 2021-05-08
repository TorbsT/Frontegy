using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField] private string name;
    [SerializeField] private Material mat;

    public bool isSamePlayer(Player p)
    {
        return p.Equals(this);
    }

    public string getName()
    {
        return name;
    }
    public Material getMat()
    {
        return mat;
    }
}
