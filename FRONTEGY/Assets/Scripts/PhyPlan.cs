using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PhyPlan
{
    private GameObject prefab { get { if (_prefab == null) Debug.LogError(this + " has no associated prefab"); return _prefab; } }
    public int count { get { if (_count <= 0) Debug.LogWarning(this + " has count == " + _count); return _count; } }
    public int looper { get { _looper++; if (_looper >= count) _looper = 0; return _looper; } }

    [Header("Assign")]
    [SerializeField] private string name;  // Only for use in editor
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _count;

    [Header("Observe")]
    [SerializeField] private int _looper;

    public Phy gen()
    {
        Phy p = Object.Instantiate(prefab).GetComponent<Phy>();
        if (p == null) Debug.LogError("InspectorException: " + this + " has no Phy component attached to its prefab");
        return p;
    }
    public override string ToString()
    {
        string txt = "PhyPlan '";
        if (name == "") txt += "unnamed";
        else txt += name;
        txt += "'";
        return txt;
    }
}
