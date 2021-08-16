using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Phy : MonoBehaviour, IPoolHost
{
    // Properties

    public bool staged { get { return _staged; } }
    public IPoolClient insChy { set { _chy = value; } }
    public Transive trans { get { if (staged) return _transive; Debug.LogError("Tried accessing trans of unstaged phy '" + this + "'"); return null; } }
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

    [Header("Assign")]
    [SerializeField] private GFX _gfx;

    [Header("Observe")]
    [SerializeField] private bool _staged;
    [SerializeField] private Transive _transive;
    [SerializeField] private bool hasBounds;
    [SerializeField] private Bounds _colliderBounds;
    [SerializeReference] private IPoolClient _chy;

    // Hidden
    private Roster roster;
    

    private void Awake()
    {
        _transive = new Transive(transform);
        //unstage(); WHY IS THIS HERE?
    }
    private void OnEnable()
    {
        _staged = true;
    }
    private void OnDisable()
    {
        _staged = false;
    }
    public void setMat(MatPlace matPlace, RendPlace rendPlace)
    {
        gfx.setMatAtPlace(matPlace, rendPlace);
    }
    public void setCol(MatPlace matPlace, RendPlace rendPlace)
    {
        gfx.setColAtPlace(matPlace, rendPlace);
    }
    public void setFloat(RendPlace rendPlace, string name, float f)
    {
        gfx.setFloat(rendPlace, name, f);
    }

    protected abstract Chy getChy();
    public abstract void unstage();
}
