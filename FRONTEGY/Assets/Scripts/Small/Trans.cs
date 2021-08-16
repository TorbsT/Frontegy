using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Trans
{  // May be Transive or Transtatic
    public Transform transform { get { return _transform; } protected set { _transform = value; } }
    [SerializeField] private Transform _transform;  // may NEVER be null
    [SerializeReference] private List<Trans> children = new List<Trans>();

   




    public void recursiveComputeWorld()
    {
        // DOES NOT COMPUTE FOR SELF.
        foreach (Trans child in children)
        {
            child.computeWorld();
            child.recursiveComputeWorld();
        }
    }
    public void stateChanged()
    {

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
}
