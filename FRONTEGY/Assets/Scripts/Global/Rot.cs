using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Rot : ITransPropertyField<Rot>
{
    public Quaternion q { get { return _q; } set { _q = value; } }
    [SerializeField] private Quaternion _q;
    [SerializeField] private Vector4 _rawQuat;

    public Rot(Quaternion q)
    {
        _q = q;
        _rawQuat = new Vector4(q.x, q.y, q.z, q.w);
    }

    public Rot computeWorld(Transform parent) => new Rot(parent.rotation *_q);
    public Rot computeLocal(Transform parent) => new Rot(_q * Quaternion.Inverse(parent.rotation));
    public Rot transformToProperty(Transform transform) => new Rot(transform.localRotation);
    public void update(Transform transform) { transform.localRotation = _q; }
}
