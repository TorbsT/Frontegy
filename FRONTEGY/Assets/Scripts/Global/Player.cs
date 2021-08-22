using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player", order = 2)]
public class Player : ScriptableObject
{
    public int id { get => _id; }

    [SerializeField] private int _id;
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
    public string getMatPlace()
    {
        return getMat().place;
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
