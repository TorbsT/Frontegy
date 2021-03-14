using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] Camera cam;

    [Header("System/Debug")]
    [SerializeField] GameObject linePrefab;
    [SerializeField] public GameObject selectedObj;
    [SerializeField] public GameObject hoveredObj;

    [SerializeField] private GameObject previousSelectedObj;
    [SerializeField] private GameObject previousHoveredObj;

    private bool isInitialized = false;
    private void ManualStart()
    {
        isInitialized = true;
    }
    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            hoveredObj = hit.transform.gameObject;
        } else
        {
            hoveredObj = null;
        }


        HoverMechanic();
        if (Input.GetMouseButtonDown(0)) SelectMechanic();
        if (Input.GetMouseButtonDown(1)) SecondarySelectMechanic();
    }
    public void ResetSelections()
    {
        hoveredObj = null;
        selectedObj = null;
        HoverMechanic();
        SelectMechanic();
    }
    void SecondarySelectMechanic()
    {  // Right click
        if (hoveredObj == null || selectedObj == null) return;  // hovered and selected must exist
        if (GetSelectable(selectedObj).IsCard() && GetSelectable(hoveredObj).IsTile()) PlaceCard();  // (sel = card, hov = tile) --> Place card
        else if (GetSelectable(selectedObj).IsTroop() && GetSelectable(hoveredObj).IsTile()) MoveUnit();  // (sel = unit, hov = tile) --> Move unit
    }
    void PlaceCard()
    {
        // SUPPOSES: selectedObj = CardEL
        Debug.Log("Place card now");

        Card card = GetSelectable(selectedObj).SelGetCard();
        card.Activate("summon");
    }
    void MoveUnit()
    {
        Troop troop = GetSelectable(selectedObj).SelGetTroop();
        int fromTileId = troop.stats.parentTileId;
        int toTileId = GetSelectable(hoveredObj).SelGetTile().geo.id;

        bool canMove = troop.tileIsInRange(toTileId);
        if (canMove) troop.SelPlanMovement(fromTileId, toTileId);
        else FailedMoveUnit();
        
        // maybe turn PlanMovement() into void since path is handled on the unit
    }
    void FailedMoveUnit()
    {

    }
    void HoverMechanic()
    {
        if (hoveredObj == previousHoveredObj) return;
        // => hoveredObj changed.

        if (previousHoveredObj != null && GetSelectable(previousHoveredObj) != null)
        {
            // => hoveredObj changed, previousHoveredObj exists, and is selectable.
            GetSelectable(previousHoveredObj).SelUnHover();
        }
        if (hoveredObj != null && GetSelectable(hoveredObj) != null)
        {
            // => hoveredObj changed, exists, and is selectable.
            GetSelectable(hoveredObj).SelHover();
        }

        previousHoveredObj = hoveredObj;
    }
    void SelectMechanic()
    {
        selectedObj = hoveredObj;

        if (selectedObj == previousSelectedObj) return;
        // => selectedObj changed.
        if (previousSelectedObj != null && GetSelectable(previousSelectedObj) != null)
        {
            // => selectedObj changed, previousSelectedObj exists, and is selectable.
            GetSelectable(previousSelectedObj).SelUnSelect();
        }
        if (selectedObj != null && GetSelectable(selectedObj) != null)
        {
            // => selectedObj changed, exists, and is selectable.
            GetSelectable(selectedObj).SelSelect();
        }

        previousSelectedObj = selectedObj;
    }
    
    public Selectable GetSelectable(GameObject obj)
    {
        if (obj == null) return null;
        SelLinker hitLinker = obj.GetComponent<SelLinker>();
        if (hitLinker == null) return null;
        Selectable hitSelectable = hitLinker.link;
        return hitSelectable;
    }
    
}
