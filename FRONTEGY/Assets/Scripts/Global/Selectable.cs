using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    /*
    [System.NonSerialized] public GameObject ro;  // redirectObject
    [SerializeField] public Tile redirectTile;
    [SerializeField] public Troop redirectUnitScript;

    public bool isSelected;
    public int playerId;

    bool isInitialized = false;
    public bool isTile, isUnit = false;
    void ManualStart()
    {
        isInitialized = true;
        ro = gameObject;
        if (redirectTile != null) isTile = true;
        else if (redirectUnitScript != null) isUnit = true;

        if (isTile) playerId = redirectTile.geo.playerId;
        else if (isUnit) playerId = redirectUnitScript.stats.playerId;

    }
    public void A()
    {
        if (!isInitialized) ManualStart();
    }
    */
    public virtual void SelHover()
    {

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

    public bool SelIsTroop() { return SelGetTroop() != null; }
    public bool SelIsTile() { return SelGetTile() != null; }
    public bool SelIsCard() { return SelGetCard() != null; }

    public virtual Troop SelGetTroop() { return null; }
    public virtual Tile SelGetTile() { return null; }
    public virtual CardEL SelGetCard() { return null; }


    public virtual List<Breadcrumb> SelPlanMovement(int fromTileId, int toTileId)
    {
        return null;
    }
}
