using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Chy : IPoolClient  // Connector-Phy
{  // The stuff Phys are connected to. Can be Troop, Tile, etc
    public bool connected { get; set; }
    public Transive transive { get { Phy p = getPhy(); if (p == null) Debug.LogError("Tried accessing trans of '"+this+"', but it's unstaged"); if (p.transive == null) Debug.LogError("Staged Phy '" + p + "' has no transive"); return p.transive; } }
    public bool uized { get { return _uized; } }


    private bool _uized;


    public void setMat(string rendPlace, string matPlace)
    {
        getPhy().setMat(rendPlace, matPlace);
    }
    public void setCol(string rendPlace, string colPlace)
    {
        getPhy().setCol(rendPlace, colPlace);
    }
    public void setFloat(string rendPlace, string name, float f)
    {
        getPhy().setFloat(rendPlace, name, f);
    }
    public Bounds getColliderBounds()
    {
        return getPhy().colliderBounds;
    }
    public GameObject getGO() => getPhy().gameObject;
    public void uize()
    {
        if (uized) Debug.LogError("Already Uized");
        _uized = true;
    }
    public void unuize()
    {
        _uized = false;
    }


    public virtual void initMats() { }
    public virtual void justConnected() { }
    public virtual void justDisconnected() { }
    public abstract Phy getPhy();
    public abstract void stage();
    public abstract void unstage();
}
