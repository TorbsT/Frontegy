using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransProperty
{
    void setParent(Trans parent, bool keepWorldSpace = false);
    void computeLocalByTransformProperty();
    void computeWorld();
    void computeLocal();
    void showTransIfNecessary(bool force = false);
    void transformExternallyChanged();  // Reapplies world
}
