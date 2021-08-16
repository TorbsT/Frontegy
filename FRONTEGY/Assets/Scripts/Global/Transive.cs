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
    // if a property hasn't changed the last frame, transform isn't called at all for that property that frame.

    public Transform parentTransform { get { return parent.transform; } }
    public Trans parent { get { return _parent; } }


    public Pos3Property pos3p { get { return _pos3p; } }
    public RotProperty rotp { get { return _rotp; } }



    [System.NonSerialized] private Trans _parent;  // may be null
    [SerializeField] private int promg;
    [SerializeReference] private Pos3Property _pos3p;
    [SerializeField] private RotProperty _rotp;

    public Transive(Transform transform) { this.transform = transform; construct(); }
    public Transive(Transform transform, Trans parent) { this.transform = transform; _parent = parent; if (parent == null) Debug.LogError("IllegalArgumentException: you used the wrong constructor"); construct(); }


    private void construct()
    {
        if (transform == null) Debug.LogError("IllegalArgumentException");
        _pos3p = new Pos3Property(this);
        _rotp = new RotProperty(this);
        GameMaster.GetGM().addTransive(this);  // Used for updating transes every frame
    }

    public void setParent(Trans parent, bool keepWorldSpace = false)
    {
        // if keepWorldSpace, showTrans = false
        if (parent == this.parent) Debug.LogWarning("IllegalStateException: '" + parent + "' is already the parent of '" + this + "'");
        if (_parent != null) _parent.unsubscribe(this);
        if (parent != null) parent.subscribe(this);
        _parent = parent;
        if (keepWorldSpace) computeLocal();
        else { computeWorld(); recursiveComputeWorld(); }
    }
    public void setParent(Transform parent, bool keepWorldSpace = false)
    {
        setParent(new Transtatic(parent), keepWorldSpace);
    }
    public void showTransIfNecessary()
    {
        _pos3p.showTransIfNecessary();
        _rotp.showTransIfNecessary();
    }
    private void computeLocal()
    {
        _pos3p.computeLocal();
        _rotp.computeLocal();
    }
    protected override void computeWorld()
    {
        _pos3p.computeWorld();
        _rotp.computeWorld();
    }
}
