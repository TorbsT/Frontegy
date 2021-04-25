using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Phy
{
    private Pos3 pos;
    private GameObject go;
    private Roster roster;  // parent

    public Phy(Roster roster)
    {
        this.roster = roster;
        pos = new Pos3();
        go = null;  // only temporary, subclasses should instantiate
        instantiateGO();
    }

    public void updatePos()
    {
        getGO().transform.position = getPos().getV3();
    }

    protected void instantiateGO()
    {
        if (hasGO()) { Debug.LogError("Tried creating multiple GOs for one Phy"); return; }
        GameObject prefab = getPrefab();
        if (prefab == null) Debug.LogError("subclass returned null as its prefab");
        go = GameObject.Instantiate(prefab);
        InsPhy ip = go.AddComponent<InsPhy>();
        ip.set(this);
    }
    protected GameObject getGO() { if (!hasGO()) Debug.LogError("go is null, should never happen"); return go; }
    protected GameMaster getGM() { return getRoster().getGM(); }
    private Roster getRoster() { if (roster == null) Debug.LogError("Phy should store a reference to the roster"); return roster; }
    public bool hasGO() { return go != null; }
    public void stage(Chy chy)
    {
        if (chy == null) Debug.LogError("Program would work, but calling this is unecessary and probably shouldn't happen");
        if (isStaged()) Debug.LogError("Phy already staged");
        setChy(chy);
    }
    public void unstage()
    {
        if (!isStaged()) Debug.LogError("Phy wasn't staged in the first place");
        setChy(null);
    }
    public bool isStaged() { return getChy() != null; }
    public void setPos3(Pos3 pos) { this.pos = pos; }
    public void setPos2(Pos2 pos) { this.pos = new Pos3(pos); }
    public Pos3 getPos() { return pos; }

    protected abstract void setChy(Chy chy);  // must cast
    protected abstract Chy getChy();
    protected abstract GameObject getPrefab();
}
