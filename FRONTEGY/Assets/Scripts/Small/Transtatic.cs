﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transtatic : Trans
{
    // Always has a parent.
    // Local properties are by definition constant, but not world.
    public Transponent transponent { get => _transponent; }
    private Transponent _transponent;
    private bool _transformExternallyChanged = true;
    public Transtatic(Transform transform, Trans parent) : base(transform)
    {
        if (parent == null) Debug.LogError("IllegalArgumentException");
        _transponent = transform.GetComponent<Transponent>();
        if (_transponent == null) Debug.LogError("InspectorException: Tried creating transtatic on " + transform.gameObject + ", but it has no Transponent component");
        _transponent.transtatic = this;
        setParent(parent, true);
        
    }
    protected override void computeWorld()
    {
        if (_transformExternallyChanged) computeLocalByTransformProperty();
        _properties.ForEach(p => p.computeWorld());
    }
    protected override void computeLocal()
    {
        computeLocalByTransformProperty();   
    }
    private void computeLocalByTransformProperty()
    {
        _transformExternallyChanged = false;
        _properties.ForEach(p => p.computeLocalByTransformProperty());
    }
}
