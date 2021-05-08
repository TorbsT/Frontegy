using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Chy  // Connector-Phy
{  // The stuff Phys are connected to. Can be Troop, Tile, etc
    private Grid grid;

    public Chy(Grid grid)
    {
        if (grid == null) Debug.LogError("IllegalArgumentException");
        this.grid = grid;
    }
    public void stage()
    {
        if (isStaged()) Debug.LogError("Chy already staged, unstage first");
        connect();
        if (!isStaged()) Debug.LogError("Couldn't connect Chy to a Phy");
        getPhy().stage(this);
    }
    public void unstage()
    {
        if (!isStaged()) Debug.LogError("Chy already unstaged, can't unstage");
        getPhy().unstage();
        disconnect();
    }
    public bool isStaged()
    {
        return getPhy() != null;
    }

    public Grid getGrid() { if (grid == null) Debug.LogError("IllegalStateException"); return grid; }
    protected abstract Phy getPhy();
    protected abstract void connect();
    protected abstract void disconnect();
}
