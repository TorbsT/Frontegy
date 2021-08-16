using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Trans
{  // May be Transive or Transtatic
    public Transform transform { get => _transform; set { _transform = value; } }

    [SerializeReference] private List<Trans> children = new List<Trans>();
    private Transform _transform;
    public Trans(Transform transform)
    {
        _transform = transform;
    }
    public void recursiveComputeWorld()
    {
        // DOES NOT COMPUTE FOR SELF.
        foreach (Trans child in children)
        {
            Debug.Log("COCK");
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
