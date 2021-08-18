using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transtatic : Trans
{
    // Always has a parent.
    // Local properties are by definition constant, but not world.
    public Transponent transponent { get => _transponent; }
    private Transponent _transponent;
    public Transtatic(Transform transform, Trans parent)
    {
        if (parent == null) Debug.LogError("IllegalArgumentException");

        this.transform = transform;
        _transponent = transform.GetComponent<Transponent>();
        if (_transponent == null) Debug.LogError("InspectorException: Tried creating transtatic on " + transform.gameObject + ", but it has no Transponent component");
        _transponent.transtatic = this;
        setParent(parent, true);
        
    }
    protected override void computeWorld()
    {
        _lastWorldComputation = Time.time;
        _pos3p.computeWorld();
        _rotp.computeWorld();
    }
    protected override void computeLocal()
    {
        _pos3p.computeLocalByTransformProperty();
        _rotp.computeLocalByTransformProperty();
    }
}
