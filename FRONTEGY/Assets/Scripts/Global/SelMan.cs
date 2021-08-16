using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelMan
{
    public static SelMan Instance { get; private set; }

    [SerializeField] private Cam cam;
    [SerializeField] private SelChy hovered;
    [SerializeField] private SelChy selected;
    [SerializeField] private GameObject hoveredGO;
    [SerializeField] private GameObject selectedGO;

    public SelMan(Cam cam)
    {
        Instance = this;
        if (cam == null) Debug.LogError("IllegalArgumentException");
        this.cam = cam;
    }
    public void freeViewUpdate(Control control, Player player)
    {
        if (player == null) Debug.LogError("IllegalArgumentException");
        hoverMechanic(getMousedGO(control));
        if (control.getM0Down()) selectMechanic();
        if (control.getM1Down()) placeMechanic();
    }
    private bool canHover(SelChy selChy)
    {  // For free view
        return true;
        return canSelect(selChy) || canSecondarySelect(selChy);
    }



    public void resetSelections()
    {
        selectedGO = null;
        hoveredGO = null;
        hoverMechanic(null);
        placeMechanic();
    }
    private void hoverMechanic(GameObject go)
    {
        if (go == hoveredGO) return;
        // => hoveredObj changed.
        if (hovered != null) hovered.unhover();

        if (go == selectedGO) { return; }
        hoveredGO = go;
        hovered = toChy(go);
        if (canHover(hovered))
        {
            if (hovered != null) hovered.hover();
        }
    }
    private void selectMechanic()
    {
        if (selectedGO == hoveredGO) return;
        // => selectedObj changed.

        if (hovered != null)
        {
            SelectAction action = new SelectAction(hovered);
            ActionManager.Instance.doAction(action);
        }
    }
    private void placeMechanic()
    {  // Right click
        SecondarySelectAction action = new SecondarySelectAction(hovered);
        ActionManager.Instance.doAction(action);
    }
    private GameObject getMousedGO(Control control)
    {
        return getCam().getMousedGO(control);
    }
    private Cam getCam()
    {
        if (cam == null) Debug.LogError("IllegalStateException");
        return cam;
    }


    private SelChy toChy(GameObject go)
    {
        if (go == null) return null;
        SelPhy s = go.GetComponent<SelPhy>();  // Selectable is attached to the GO that has meshcollider.
        if (s == null) return null;  // go is not selectable
        if (!s.staged) return null;  // Phy is not staged, nothing should happen
        SelChy selChy = s.getSelChy();
        if (selChy == null) Debug.LogError("A SelPhy is staged but it has no SelChy");
        return selChy;
    }



    public void select(SelChy selChy)
    {
        selected = selChy;
        selectedGO = selChy.getGO();
    }
    public bool canSelect(SelChy selChy)
    {
        // Independent of what hovered() is.
        if (selChy is Tile) return false;
        return true;
    }
    public void secondarySelect(SelChy selChy)
    {
        if (selected == null) Debug.LogError("Tried forcing secondary selection when none selected");
        selected.secondarySelectOn(selChy);
    }
    public bool canSecondarySelect(SelChy selChy)
    {
        if (selected == null) return false;
        return selected.canSecondarySelectOn(selChy);
    }
}
