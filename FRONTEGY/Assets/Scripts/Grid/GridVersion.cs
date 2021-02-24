using System.Collections.Generic;

[System.Serializable]
public class GridData
{
    public List<Tile> tiles;
    private List<Troop> troops;
    public List<Card> cards;

    public GridData(List<Tile> tiles, List<Troop> troops, List<Card> cards)
    {
        this.tiles = tiles;
        this.troops = troops;
        this.cards = cards;
    }

    public void UpdateTroopTiles()
    {  // changes troop tiles to their path's last breadcrumb's tile
        if (IsNoTroops()) return;  // crash unnecessary
        foreach (Troop troop in GetTroops())
        {
            troop.stats.UpdateParentTile();
        }
    }
    public bool IsNoTroops()
    {
        return GetTroopCount() == 0;
    }
    private int GetTroopCount()
    {
        if (troops == null) return 0;
        return GetTroops().Count;
    }
    public List<Troop> GetTroops()
    {
        return troops;
    }

}
