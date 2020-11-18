using System.Collections.Generic;

[System.Serializable]
public class GridData
{
    public List<Tile> tiles;
    public List<Troop> troops;
    public List<Card> cards;

    public GridData(List<Tile> tiles, List<Troop> troops, List<Card> cards)
    {
        this.tiles = tiles;
        this.troops = troops;
        this.cards = cards;
    }
}
