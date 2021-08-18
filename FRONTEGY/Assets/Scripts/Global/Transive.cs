using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transive : Trans
{
    // Transive is a Trans whose parent and properties can be manually changed.
    // They are required NOT to be in the standard transform-hierarchy.
    // When changing transives, the following is ensured:
    // transform component gets called a maximum of once per frame for every property (pos, rot etc)
    // if a world-property hasn't changed the last frame, transform isn't called at all for that property that frame.


    public Transive(Transform transform) : base(transform) { construct(); }
    public Transive(Transform transform, Trans parent) : base(transform) { construct(); setParent(parent, true); }

    [SerializeField] private float _lastTransShow;

    private void construct()
    {
        GameMaster.GetGM().addTransive(this);  // Used for updating transes every frame
    }
    public void showTransIfNecessary()
    {
        _lastTransShow = Time.time;
        _pos3p.showTransIfNecessary();
        _rotp.showTransIfNecessary();
    }
    protected override void computeLocal()
    {
        if (parent == null)
        _pos3p.computeLocal();
        _rotp.computeLocal();
    }
    protected override void computeWorld()
    {
        _pos3p.computeWorld();
        _rotp.computeWorld();
    }
}
