using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Rot : ITransPropertyField<Rot>
{
    public Quaternion q { get { return _q; } set { _q = value; } }
    [SerializeField] private Quaternion _q;

    public Rot(Quaternion q) { _q = q; }

    public Rot computeWorld(Transform parent) => new Rot(_q * parent.rotation);
    public Rot computeLocal(Transform parent) => new Rot(_q * Quaternion.Inverse(parent.rotation));
    public void update(Transform transform) { transform.localRotation = _q; }
}
