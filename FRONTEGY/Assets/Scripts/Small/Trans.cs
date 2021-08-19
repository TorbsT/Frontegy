using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Trans
{  // May be Transive or Transtatic
    public Transform transform { get { if (_transform == null) Debug.LogError("IllegalStateException: "+GetType()+" without transform"); return _transform; } }



    public Transform parentTransform { get { return parent.transform; } }
    public Trans parent { get { return _parent; } }
    public string name { get => _name; }


    public Pos3Property pos3p { get { if (_pos3p == null) Debug.LogError(GetType() + " " + transform.gameObject + " has no pos3p"); return _pos3p; } }
    public RotProperty rotp { get { return _rotp; } }
    public ScaleProperty scalep { get => _scalep; }


    [SerializeField] private string _name;
    [SerializeReference] private List<Trans> children = new List<Trans>();
    [SerializeReference] protected Pos3Property _pos3p;
    [SerializeReference] protected RotProperty _rotp;
    [SerializeReference] protected ScaleProperty _scalep;
    private Transform _transform;
    [System.NonSerialized] private Trans _parent;  // may be null



    public Trans(Transform transform)
    {
        _transform = transform;
        _name = transform.gameObject.name;
        _pos3p = new Pos3Property(this);
        _pos3p.set(new Pos3(0f, 0f, 0f));
        _rotp = new RotProperty(this);
        _rotp.set(new Rot(Quaternion.identity));
        _scalep = new ScaleProperty(this);
        _scalep.set(Scale.identity());
    }

    public void setParent(Trans parent, bool keepWorldSpace = false)
    {
        if (parent == null) Debug.LogError("IllegalArgumentException");

        // if keepWorldSpace, showTrans = false
        if (parent == this.parent) Debug.LogWarning("IllegalStateException: '" + parent + "' is already the parent of '" + this + "'");
        if (_parent != null) _parent.unsubscribe(this);
        if (parent != null) parent.subscribe(this);
        _parent = parent;
        if (keepWorldSpace) computeLocal();
        else { computeWorld(); recursiveComputeWorld(); }
    }
    public void recursiveComputeWorld()
    {
        //Debug.Log("recursed " + this + " at time " + Time.time);
        // DOES NOT COMPUTE FOR SELF.
        foreach (Trans child in children)
        {
            child.computeWorld();
            child.recursiveComputeWorld();
        }
    }
    public void subscribe(Trans observer)
    {
        children.Add(observer);
    }
    public void unsubscribe(Trans observer)
    {
        children.Remove(observer);
    }
    protected abstract void computeWorld();
    protected abstract void computeLocal();
    public override string ToString() => "["+GetType()+" '"+_name+"']";
}
