using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Chy : IPoolClient  // Connector-Phy
{  // The stuff Phys are connected to. Can be Troop, Tile, etc
    public bool staged { get; set; }
    public Transive trans { get { if (getPhy() == null) Debug.LogError("Tried accessing trans of '"+this+"', but it's unstaged"); return getPhy().transive; } }
    public bool uized { get { return _uized; } }


    private bool _uized;


    public void setMat(MatPlace matPlace, RendPlace rendPlace)
    {
        getPhy().setMat(matPlace, rendPlace);
    }
    public void setCol(MatPlace matPlace, RendPlace rendPlace)
    {
        getPhy().setCol(matPlace, rendPlace);
    }
    public void setFloat(RendPlace rendPlace, string name, float f)
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
    public abstract Phy getPhy();
    public abstract void stage();
    public abstract void unstage();
}
