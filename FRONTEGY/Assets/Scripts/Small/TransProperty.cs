﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TransProperty<T> : ITransProperty
{
    // whenever local changes, world gets outdated
    // whenever world changes, local gets outdates
    // whenever parent changes, you may choose which gets outdated
    public Transform transform { get { return trans.transform; } }
    public Transform parentTransform { get => parent.transform; }
    public Trans parent { get => _parent; }
    private bool transformOutdated { get => !_world.Equals(_lastWorld); }
    public Trans trans { get; private set; }
    public string name { get => trans.name; }

    [SerializeReference] private Trans _parent;
    [SerializeField] private float _lastComputation = -1;
    [SerializeField] private float _lastComputationType = -1;
    [SerializeField] private T _local;
    [SerializeField] private T _world;
    [SerializeField] private T _lastLocal;
    [SerializeField] private T _lastWorld;  // The transform's current value

    public TransProperty(Trans trans)
    {
        if (trans == null) Debug.LogError("IllegalArgumentException");
        this.trans = trans;
    }
    public void setParent(Trans parent, bool keepWorldSpace = false)
    {
        if (_parent != null) _parent.unsubscribe(trans);
        if (parent != null) parent.subscribe(trans);
        _parent = parent;
        if (keepWorldSpace) computeLocal();
        else { computeWorld(); trans.recursiveComputeWorld(); }
    }
    public void computeLocalByTransformProperty()
    {
        if (parent == null) Debug.LogError("very wrong");
        else _local = getLocalPropertyFromTransform();
        _lastComputation = Time.time;
        _lastComputationType = 0;
    }
    public void computeLocal()
    {
        if (parent == null) _local = _world;
        else
        {
            _local = computeLocalWithParentWorld();
        }
        _lastComputation = Time.time;
        _lastComputationType = 1;
    }
    public void computeWorld()
    {
        if (parent == null) _world = _local;
        else
        {
            _world = computeWorldWithParentWorld();
        }
        _lastComputation = Time.time;
        _lastComputationType = 2;
    }
    public void transformExternallyChanged()
    {  // Transform externally modified (e.g. with a rigidbody), which this property didn't notice.
        // Update transform to match this property
        showTransIfNecessary(true);
    }

    /// <summary>
    /// Sets the value.
    /// </summary>
    /// <param name="value">The value to set.</param>
    /// <param name="local">True if should set in local space.</param>
    /// <param name="showTrans">True if should immediately update transform (if changed).</param>
    public void set(T value, bool local = true)
    {
        if (local)
        {
            _local = value;
            computeWorld();
        } else
        {
            _world = value;
            computeLocal();
        }

        // Alerts children to recompute world
        if (transformOutdated)
        {
            trans.recursiveComputeWorld();
        }
    }
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <param name="local">True if should return value relative to parent field</param>
    /// <param name="lastUpdate">True if should return value from last update.</param>
    public T get(bool local = true, bool lastUpdate = false)
    {
        if (lastUpdate)
        {
            if (local) return _lastLocal;
            else return _lastWorld;
        } else
        {
            if (local) return _local;
            else return _world;
        }
    }
    public void showTransIfNecessary(bool force = false)
    {
        // Does no actual computing.
        // If transform already matches _world, no update is needed.
        // transform always matches _lastWorld.
        // Therefore, if _world == _lastWorld, _world == transform.
        if (!transformOutdated && !force)
        {
            // Already up-to-date.
        } else
        {
            // Update necessary
            _lastWorld = _world;
            _lastLocal = _local;
            showTrans();
        }
    }
    protected abstract T computeWorldWithParentWorld();  // parent must not be null
    protected abstract T computeLocalWithParentWorld();  // parent must not be null
    protected abstract T getLocalPropertyFromTransform();
    protected abstract void showTrans();
}

// VERY beautiful subclasses
[System.Serializable] public class Pos3Property : TransProperty<Pos3> { public Pos3Property(Trans trans) : base(trans) { }
    protected override Pos3 computeWorldWithParentWorld()
    {
        // take parent pos, rot, and scale into account.
        Pos3 diffPos = get(true);  // get from local
        diffPos *= parent.scalep.get(false);  // Scale difference with parent scale
        diffPos *= parent.rotp.get(false);  // Rotate difference with parent rotation
        return parent.pos3p.get(false) + diffPos;  // Add parent world and difference to get this.world
    }
    protected override Pos3 computeLocalWithParentWorld()
    {
        // take parent pos, rot, and scale into account.
        Pos3 diffPos = get(false);  // get from world
        diffPos /= parent.scalep.get(false);  // Undo scale to find local distance
        diffPos /= parent.rotp.get(false);  // Undo rotation to get correct local direction
        return parent.pos3p.get(false) + diffPos;  // Add parent world and difference to get this.world
    }
    protected override Pos3 getLocalPropertyFromTransform() => new Pos3(transform.localPosition);
    protected override void showTrans()
    {
        transform.localPosition = get(false).v3;
    }
}
[System.Serializable] public class RotProperty : TransProperty<Rot> { public RotProperty(Trans trans) : base(trans) { }
    protected override Rot computeWorldWithParentWorld() => parent.rotp.get(false)*get(true);
    protected override Rot computeLocalWithParentWorld() => get(false)/ parent.rotp.get(false);
    protected override Rot getLocalPropertyFromTransform() => new Rot(transform.localRotation);
    protected override void showTrans()
    {
        transform.localRotation = get(false).q;
    }
}
[System.Serializable] public class ScaleProperty : TransProperty<Scale> { public ScaleProperty(Trans trans) : base(trans) { }
    protected override Scale computeWorldWithParentWorld() => get(true) * parent.scalep.get(false);
    protected override Scale computeLocalWithParentWorld() => get(false)/ parent.scalep.get(false);
    protected override Scale getLocalPropertyFromTransform() => new Scale(transform.localScale);
    protected override void showTrans()
    {
        transform.localScale = get(false).v3;
    }
}