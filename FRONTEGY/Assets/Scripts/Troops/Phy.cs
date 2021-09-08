using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[SelectionBase]
public abstract class Phy : MonoBehaviour, IPoolHost
{
    // Properties

    public bool connected { get { return _staged; } set { _staged = value; } }
    public IPoolClient chy { set { _chy = value; } }
    public Transive transive { get => _transive; }
    public Bounds colliderBounds { get { if (hasBounds) return _colliderBounds; else
            {
                Collider top = GetComponent<Collider>();
                hasBounds = top != null;
                if (hasBounds) _colliderBounds = top.bounds;
                foreach (Collider collider in GetComponentsInChildren<Collider>())
                {
                    if (!hasBounds) Debug.LogWarning("Weird shit");
                    if (!collider.Equals(top)) _colliderBounds.Encapsulate(collider.bounds);
                }
                hasBounds = true;  // Prevent doing this multiple times
                return _colliderBounds;
            } } }
    public GFX gfx { get { return _gfx; } }

    private GFX _gfx;

    [Header("Observe")]
    [SerializeField] private bool _staged;
    [SerializeReference] private Transive _transive;
    [SerializeField] private bool hasBounds;
    [SerializeField] private Bounds _colliderBounds;
    [SerializeReference] private IPoolClient _chy;

    // Hidden
    private Roster roster;
    

    protected virtual void Awake()
    {
        //_transive = GetComponent<Transive>();
        //if (_transive == null) Debug.LogError("InspectorException: " + this + " has no Transive component");
        _transive = new Transive(transform);
        _gfx = GetComponent<GFX>();
        if (_gfx == null) Debug.LogError("InspectorException: " + this + " has no GFX component");
    }
    public void setMat(string rendPlace, string matPlace)
    {
        gfx.setMatAtPlace(rendPlace, matPlace);
    }
    public void setCol(string rendPlace, string colPlace)
    {
        gfx.setColAtPlace(rendPlace, colPlace);
    }
    public void setFloat(string rendPlace, string name, float f)
    {
        gfx.setFloat(rendPlace, name, f);
    }
    public virtual bool tryRagdollMode() => false;
    public virtual bool tryUnragdollMode() => false;
    public virtual void justConnected() { }
    public virtual void justDisconnected() { }

    protected abstract Chy getChy();
    public abstract void unstage();
}
