using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransProperty
{
    void setParent(Trans parent, bool keepWorldSpace = false);
    void computeLocalByTransformProperty();
    void computeWorld();
}
