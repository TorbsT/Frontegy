using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Phy
{
    private static Pos3 unstagedPos = new Pos3(0f, -10f, 0f);
    [SerializeField] private Pos3 pos;  // ONLY USE FOR INSPECTING, NOT FOR CODE
    private GameObject go;
    private Roster roster;  // parent

    public Phy(Roster roster)
    {
        this.roster = roster;
        pos = new Pos3();
        go = null;  // only temporary, subclasses should instantiate
        instantiateGO();
        unstage();
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
        bool same = chy.Equals(getChy());  // debug
        if (isStaged()) Debug.LogError("Phy already staged");
        setChy(chy);
    }
    public void unstage()
    {
        // if (!isStaged()) Debug.LogError("Phy wasn't staged in the first place");
        // default behaviour, thus removed

        setChy(null);
        setPos3(unstagedPos);
    }
    public bool isStaged() { return getChy() != null; }
    public void setPos3(Pos3 pos) { this.pos = pos; showPos();  }
    public void setPos2(Pos2 pos) { this.pos = new Pos3(pos); showPos(); }
    private void showPos() { getGO().transform.position = getPos().getV3(); }
    public Pos3 getPos() { return pos; }

    protected abstract void setChy(Chy chy);  // must cast
    public abstract Chy getChy();
    protected abstract GameObject getPrefab();
}
