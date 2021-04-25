using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chy  // Connector-Phy
{  // The stuff Phys are connected to. Can be Troop, Tile, etc
    public void stage()
    {
        if (isStaged()) Debug.LogError("Chy already staged, unstage first");
        connect();
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

    protected abstract Phy getPhy();
    protected abstract void connect();
    protected abstract void disconnect();
}
