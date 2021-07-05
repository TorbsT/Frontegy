using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Phy : MonoBehaviour, IPoolHost
{
    // Properties

    public bool staged { get; set; }
    public Chy inspectorChy { set { _chy = value; } }
    public Trans trans { get { if (staged) return getChy().trans; else return unstagedTrans; } }
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
    [SerializeField] private bool hasBounds;
    [SerializeField] private Bounds _colliderBounds;
    [SerializeReference] private Chy _chy;

    // Hidden
    private static Trans unstagedTrans = new Trans();
    private Roster roster;
    

    private void Awake()
    {
        //unstage(); WHY IS THIS HERE?
    }
    public void showTrans()
    {
        transform.position = trans.pos3.v3;
        transform.rotation = trans.rot;
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
