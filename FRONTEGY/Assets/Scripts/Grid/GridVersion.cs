using System.Collections.Generic;

[System.Serializable]
public class GridData
{
    public List<Tile> tiles;
    public List<Troop> troops;

    public GridData(List<Tile> tiles, List<Troop> troops)
    {
        this.tiles = tiles;
        this.troops = troops;
    }
}
