using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : Phy
{

    private bool selected;
    private bool hovered;

    public Selectable(Roster roster) : base(roster)
    {
        getGO().AddComponent<SelLinker>();
        SelLinker selLink = getGO().GetComponent<SelLinker>();  // TODO inefficient?
        selLink.link = this;
    }



    // Note: Should PROBABLY be impossible to get gameobject externally
    public virtual void SelHover()
    {
        hovered = true;
    }
    public virtual void SelUnHover()
    {

    }
    public virtual void SelSelect()
    {
        //isSelected = true;
    }
    public virtual void SelUnSelect()
    {
        //isSelected = false;
    }

    public bool isSelected() { return selected; }
    public bool isHovered() { return hovered; }

    public bool IsTroop() { return SelGetTroop() != null; }
    public bool IsTile() { return SelGetTile() != null; }
    public bool IsCard() { return SelGetCard() != null; }

    public virtual Troop SelGetTroop() { return null; }
    public virtual Tile SelGetTile() { return null; }
    public virtual Card SelGetCard() { return null; }

    public void LogScriptNotFound(string scriptTypeText)
    {
        Debug.LogWarning("ERROR: "+scriptTypeText+" script not found");
    }
}
