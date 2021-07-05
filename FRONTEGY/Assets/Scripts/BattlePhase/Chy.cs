using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Chy : IPoolClient  // Connector-Phy
{  // The stuff Phys are connected to. Can be Troop, Tile, etc
    public bool staged { get; set; }
    public Trans trans { get { if (_trans == null) _trans = new Trans(); return _trans; } }
    public bool uized { get { return _uized; } }


    private Grid grid;
    [SerializeReference] private Trans _trans;
    private bool _uized;



    public Chy(Grid grid)
    {
        if (grid == null) Debug.LogError("IllegalArgumentException");
        this.grid = grid;
    }
    public void showTrans()
    {
        getPhy().showTrans();
    }
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
    public void uize()
    {
        if (uized) Debug.LogError("Already Uized");
        _uized = true;
    }
    public void unuize()
    {
        _uized = false;
    }




    public Grid getGrid() { if (grid == null) Debug.LogError("IllegalStateException"); return grid; }


    public virtual void initMats() { }
    protected abstract Phy getPhy();
    public abstract void stage();
    public abstract void unstage();
}
