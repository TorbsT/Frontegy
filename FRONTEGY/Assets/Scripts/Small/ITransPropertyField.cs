using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransPropertyField<T>
{
    T computeWorld(Transform parent);
    T computeLocal(Transform parent);
    T transformToProperty(Transform transform);  // This should actually be a static method but interfaces can't have them. Spaghetti time
    void update(Transform transform);
}
