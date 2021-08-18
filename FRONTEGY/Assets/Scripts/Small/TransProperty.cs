using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class TransProperty<T> where T : ITransPropertyField<T>  // bruh
{
    // whenever local changes, world gets outdated
    // whenever world changes, local gets outdates
    // whenever parent changes, you may choose which gets outdated
    public Transform transform { get { return trans.transform; } }
    public Transform parentTransform { get { return trans.parentTransform; } }
    public Trans parent { get { return trans.parent; } }
    private bool transformOutdated { get => !_world.Equals(_lastWorld); }
    public Trans trans { get; private set; }
    public string name { get => trans.name; }

    [SerializeField] protected float _lastComputation = -1;
    private T _local;
    [SerializeField] private T _world;
    private T _lastLocal;
    [SerializeField] private T _lastWorld;  // The transform's current value

    public TransProperty(Trans trans)
    {
        if (trans == null) Debug.LogError("IllegalArgumentException");
        this.trans = trans;
    }

    public void computeLocalByTransformProperty()
    {
        if (parent == null) Debug.LogError("very wrong");
        else _local = _local.transformToProperty(transform);
        _lastComputation = Time.time;
    }
    public void computeLocal()
    {
        if (parent == null) _local = _world;
        else _local = _world.computeLocal(getParentWorldProperty());
        _lastComputation = Time.time;
    }
    public void computeWorld()
    {
        if (parent == null) _world = _local;
        else
        {
            _world = _local.computeWorld(getParentWorldProperty());
            if (parent.name == "Main Camera") Debug.Log(_world);
        }
        _lastComputation = Time.time;
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
    public void showTransIfNecessary()
    {
        // Does no actual computing.
        // If transform already matches _world, no update is needed.
        // transform always matches _lastWorld.
        // Therefore, if _world == _lastWorld, _world == transform.
        if (!transformOutdated)
        {
            // Already up-to-date.
        } else
        {
            // Update necessary
            _lastWorld = _world;
            _lastLocal = _local;
            _world.update(transform);
        }
    }
    public void parentChanged(bool keepWorldSpace = false)
    {
        // Not very intuitive:
        // If keeping world space, compute world space and outdate local space.
        // If keeping local space, compute local space and outdate world space.
        set(get(keepWorldSpace), keepWorldSpace);
    }
    protected abstract T getParentWorldProperty();  // WHAT IS THIS TRASH
    
}

// VERY beautiful subclasses
[System.Serializable]
public class Pos3Property : TransProperty<Pos3> { public Pos3Property(Trans trans) : base(trans) { } protected override Pos3 getParentWorldProperty() => parent.pos3p.get(false); }
[System.Serializable]
public class RotProperty : TransProperty<Rot> { public RotProperty(Trans trans) : base(trans) { } protected override Rot getParentWorldProperty() => parent.rotp.get(false); }