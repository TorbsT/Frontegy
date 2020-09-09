using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] Camera camera;

    [Header("System/Debug")]
    [SerializeField] GameObject linePrefab;
    [SerializeField] GameObject selectedObj;
    [SerializeField] GameObject previousSelectedObj;
    [SerializeField] GameObject hoveredObj;
    [SerializeField] GameObject previousHoveredObj;

    private bool isInitialized = false;
    private void ManualStart()
    {
        isInitialized = true;
    }
    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            hoveredObj = hit.transform.GetComponent<Selectable>().gameObject;
        } else
        {
            hoveredObj = null;
        }


        HoverMechanic();
        if (Input.GetMouseButtonDown(0)) SelectMechanic();
        if (Input.GetMouseButtonDown(1)) PlanMovementMechanic();
    }
    public void ResetSelections()
    {
        hoveredObj = null;
        selectedObj = null;
        HoverMechanic();
        SelectMechanic();
    }
    void PlanMovementMechanic()
    {
        if (hoveredObj == null || selectedObj == null) return;  // hovered and selected must exist
        if (!GetSelectable(selectedObj).isUnit || !GetSelectable(hoveredObj).isTile) return;  // selected must be unit and hovered must be tile
        List<Tile> tilesInPath = GetSelectable(selectedObj).PlanMovement(GetSelectable(hoveredObj).redirectTile);
        // maybe turn PlanMovement() into void since path is handled on the unit
    }
    void HoverMechanic()
    {
        if (hoveredObj == previousHoveredObj) return;
        // => hoveredObj changed.

        if (previousHoveredObj != null && GetSelectable(previousHoveredObj) != null)
        {
            // => hoveredObj changed, previousHoveredObj exists, and is selectable.
            GetSelectable(previousHoveredObj).RedirectUnHover();
        }
        if (hoveredObj != null && GetSelectable(hoveredObj) != null)
        {
            // => hoveredObj changed, exists, and is selectable.
            GetSelectable(hoveredObj).RedirectHover();
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
            GetSelectable(previousSelectedObj).RedirectUnSelect();
        }
        if (selectedObj != null && GetSelectable(selectedObj) != null)
        {
            // => selectedObj changed, exists, and is selectable.
            GetSelectable(selectedObj).RedirectSelect();
        }

        previousSelectedObj = selectedObj;
    }
    Selectable GetSelectable(GameObject obj)
    {
        if (obj == null) return null;
        Selectable hitSelectable = obj.GetComponent<Selectable>();
        return hitSelectable;
    }
}
