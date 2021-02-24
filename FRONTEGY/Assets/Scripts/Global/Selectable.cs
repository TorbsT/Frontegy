using System.Collections.Generic;
using UnityEngine;

public class Selectable
{
    public bool isInstantiated = false;
    public GameMaster gameMaster;
    public GameObject selGO;

    public bool isSelected;
    public bool isHovered;

    public virtual void Instantiate()
    {  // USE THIS when creating GO externally.

    }
    public void Instantiate2(System.Type type)
    {
        if (selGO != null)
        {
            Debug.LogError("ERROR: tried creating selGO, but selGO already exists.");
            return;
        }
        gameMaster = Maffs.GetGM();
        Transform gridTrans = gameMaster.grid.gridGO;

        GameObject prefab = null;
        if (type == typeof(Troop)) prefab = gameMaster.troopGOPrefab;
        else if (type == typeof(Card)) prefab = gameMaster.cardGOPrefab;
        else if (type == typeof(Tile)) prefab = gameMaster.tileGOPrefab;

        if (prefab == null) Debug.LogError("ERROR: Couldn't find a fitting prefab for the calling type " + type.ToString());
        selGO = Object.Instantiate(prefab, gridTrans);

        selGO.AddComponent<SelLinker>();
        SelLinker selLink = selGO.GetComponent<SelLinker>();
        selLink.link = this;
        isInstantiated = true;
    }
    // Note: Should PROBABLY be impossible to get gameobject externally
    public virtual void SelHover()
    {
        isHovered = true;
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

    public bool IsTroop() { return SelGetTroop() != null; }
    public bool IsTile() { return SelGetTile() != null; }
    public bool IsCard() { return SelGetCard() != null; }

    public virtual Troop SelGetTroop() { LogScriptNotFound("Troop"); return null; }
    public virtual Tile SelGetTile() { LogScriptNotFound("Tile"); return null; }
    public virtual Card SelGetCard() { LogScriptNotFound("Card"); return null; }

    public void LogScriptNotFound(string scriptTypeText)
    {
        Debug.LogWarning("ERROR: "+scriptTypeText+" script not found");
    }
    public virtual Paf SelPlanMovement(int fromTileId, int toTileId)
    {
        return null;
    }
}
