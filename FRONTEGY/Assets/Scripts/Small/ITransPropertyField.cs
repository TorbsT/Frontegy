using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransPropertyField<T>
{
    T computeWorld(Transform parent);
    T computeLocal(Transform parent);
    void update(Transform transform);
}
