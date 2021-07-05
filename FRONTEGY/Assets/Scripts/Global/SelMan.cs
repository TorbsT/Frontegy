using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SelMan
{
    [SerializeField] private Cam cam;
    [SerializeField] private GameObject selectedGO;
    [SerializeField] private GameObject hoveredGO;

    [SerializeField] private GameObject previousSelectedGO;
    [SerializeField] private GameObject previousHoveredGO;

    public SelMan(Cam cam)
    {
        if (cam == null) Debug.LogError("IllegalArgumentException");
        this.cam = cam;
    }
    public void freeViewUpdate(Control control, Player player)
    {
        if (player == null) Debug.LogError("IllegalArgumentException");
        hoveredGO = getMousedGO(control);

        // Constraints
        if (!canHover(player)) hoveredGO = null;

        hoverMechanic();
        if (control.getM0Down()) selectMechanic();
        if (control.getM1Down()) placeMechanic();
    }
    private bool canHover(Player player)
    {  // For free view
        return true;
        // analysis: pretty bad. maybe make an interpreter? https://docs.google.com/spreadsheets/d/1Wn4ijIl1e0L8mXiqv4A9uE3gDlESAVUYH8g61tGiez0/edit#gid=0
        if (has(hovered()))
        {
            if (has(selected()))
            {
                if (selected() is Tile) return true;  // should never happen. Tiles can't be selected (as of now)
                if (selected() is Card)
                {
                    if (hovered() is Tile) return hoverAllowedByCard((Card)selected());
                    if (hovered() is Card) return true;
                    if (hovered() is Troop) return true;
                }
                if (selected() is Troop)
                {
                    if (hovered() is Tile) return hoverInTroopRange((Troop)selected());
                    if (hovered() is Card) return true;
                    if (hovered() is Troop) return true;
                } 

            }
            
            else
            {  // None selected
                if (hovered() is Tile) return false;
                if (hovered() is Card && ownsHover(player)) return true;
                if (hovered() is Troop && ownsHover(player)) return true;
            }
        }
        return false;
    }
    private bool canSelect()
    {  // For free view. Supposes hovered() is already verified.
        if (has(hovered()))
        {
            if (hovered() is Tile) return false;
            if (hovered() is Card) return true;
            if (hovered() is Troop) return true;
        }
        return false;
    }


    public void resetSelections()
    {
        selectedGO = null;
        hoveredGO = null;
        hoverMechanic();
        placeMechanic();
    }
    private void hoverMechanic()
    {
        if (hoveredGO == selectedGO) hoveredGO = null;
        if (hoveredGO == previousHoveredGO) return;
        // => hoveredObj changed.

        if (has(previousHovered()))
        {
            previousHovered().unhover();
        }
        
        if (has(hovered()))
        {
            hovered().hover();
        }
        previousHoveredGO = hoveredGO;
    }
    private void selectMechanic()
    {
        selectedGO = hoveredGO;

        if (selectedGO == previousSelectedGO) return;
        // => selectedObj changed.

        if (has(previousSelected()))
        {
            // => selectedObj changed, previousSelectedObj exists, and is selectable.
            previousSelected().unselect();
        }
        if (has(selected()))
        {
            // => selectedObj changed, exists, and is selectable.
            selected().primarySelect();
            previousHoveredGO = null;
        }

        previousSelectedGO = selectedGO;
    }
    private void placeMechanic()
    {  // Right click
        if (!has(hovered()) || !has(selected())) return;
        if (selected() is Card && hovered() is Tile) ((Card)selected()).castOn((Tile)hovered(), CastType.summon);
        else if (selected() is Troop && hovered() is Tile) ((Troop)selected()).planPafTo((Tile)hovered());
        //else if (selected() is Card && hovered() is Troop) ((Card)selected()).castOn((Troop)hovered());
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
    /*
    private void placeCard()
    {
        if (!has(selectedCard())) Debug.LogError("IllegalStateException: SelectionManager.placeCard() called without having selected a card");
        if (!has(hoveredTile())) Debug.LogError("IllegalStateException: SelectionManager.placeCard() called without having hovered a tile");
        selectedCard().cast(CastType.summon);
    }
    private void moveTroop()
    {
        if (!has(selectedTroop())) Debug.LogError("IllegalStateException: SelectionManager.moveTroop() called without having selected a troop");
        if (!has(hoveredTile())) Debug.LogError("IllegalStateException: SelectionManager.moveTroop() called without hovering a tile");

        if (!selectedTroop().planPafTo(hoveredTile())) failedMoveUnit();
    }
    private void failedMoveUnit()
    {

    }
    */



    private bool has(System.Object obj) { return obj != null; }

    private bool noOwnerHover() { return hovered().owner == NonePlayer.Instance; }
    private bool ownsHover(Player player) { return hovered().owner == player; }
    private bool hoverInTroopRange(Troop troop) { return troop.tileIsInRange((Tile)hovered()); }
    private bool hoverAllowedByCard(Card card) { return card.canCastOn((Tile)hovered()); }


    public Tile getSelectedTile()
    {
        if (selected() == null || !(selected() is Tile)) return null;
        return (Tile)selected();
    }

    private SelChy hovered()
    {
        return toChy(hoveredGO);
    }
    private SelChy selected()
    {
        return toChy(selectedGO);
    }
    private SelChy previousHovered()
    {
        return toChy(previousHoveredGO);
    }
    private SelChy previousSelected()
    {
        return toChy(previousSelectedGO);
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
}
