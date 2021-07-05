using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField] private string name;
    [SerializeField] private Mat mat;  // 21/07/06 - ONLY DEFAULT. may be overriden in phys

    public bool isSamePlayer(Player p)
    {
        return Equals(p);
    }

    public string getName()
    {
        if (name == null) Debug.LogError("How is that even possible");
        if (name.Equals("")) Debug.LogError("InspectorException: Player with empty name");
        return name;
    }
    public MatPlace getMatPlace()
    {
        return getMat().getPlace();
    }
    private Mat getMat()
    {
        if (mat == null) Debug.LogError("InspectorException: set " + this + "'s mat");
        return mat;
    }
    public override string ToString()
    {
        return getName();
    }
}
