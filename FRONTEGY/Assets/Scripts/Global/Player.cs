using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public Player(int id)
    {
        if (id == -1) Debug.LogError("-1 is reserved for nonePlayer");
        // add debug log error for ensuring ids are unique
        this.id = id;
    }
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private Material mat;

    public bool isSamePlayer(Player p)
    {
        return p.hasId(id);
    }
    public bool hasId(int id)
    {
        return (this.id == id);
    }
    public static Player getById(int id)
    {
        return GameMaster.GetGM().getPlayerById(id);
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
