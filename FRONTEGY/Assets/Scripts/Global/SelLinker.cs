using UnityEngine;

public class SelLinker : MonoBehaviour
{
    private SelPhy selectable;

    public void link(SelPhy selectable)
    {
        if (this.selectable != null) Debug.LogError("IllegalStateException: can't link a SelLinker twice");
        if (selectable == null) Debug.LogError("IllegalArgumentException");
        this.selectable = selectable;
    }
    public SelPhy getSelectable()
    {
        if (selectable == null) Debug.LogError("IllegalStateException");
        return selectable;
    }
}
