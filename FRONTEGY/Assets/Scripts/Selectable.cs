using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] public Tile redirectTile;
    [SerializeField] UnitScript redirectUnitScript;

    public bool isSelected;
    public int teamId;

    bool isInitialized = false;
    public bool isTile, isUnit = false;
    void ManualStart()
    {
        isInitialized = true;
        if (redirectTile != null) isTile = true;
        else if (redirectUnitScript != null) isUnit = true;

        if (isTile) teamId = redirectTile.geo.teamId;
        else if (isUnit) teamId = redirectUnitScript.stats.teamId;

    }
    public void InitializeIfNot()
    {
        if (!isInitialized) ManualStart();
    }
    public void RedirectHover()
    {
        if (!isInitialized) ManualStart();
        if (isTile) redirectTile.Hover();
        else if (isUnit) redirectUnitScript.Hover();
    }
    public List<int> PlanMovement(int toTileId)
    {
        if (!isUnit) return null;
        return redirectUnitScript.PlanMovement(toTileId);
    }
    public void RedirectUnHover()
    {
        if (!isInitialized) ManualStart();
        if (isTile) redirectTile.UnHover();
        else if (isUnit) redirectUnitScript.UnHover();
    }
    public void RedirectSelect()
    {
        if (!isInitialized) ManualStart();
        isSelected = true;
        if (isTile) redirectTile.Select();
        else if (isUnit) redirectUnitScript.Select();
    }
    public void RedirectUnSelect()
    {
        if (!isInitialized) ManualStart();
        isSelected = false;
        if (isTile) redirectTile.UnSelect();
        else if (isUnit) redirectUnitScript.UnSelect();
    }
}
