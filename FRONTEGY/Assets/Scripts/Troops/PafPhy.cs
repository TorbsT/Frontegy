using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PafPhy : Phy
{
    [Header("System")]
    private PafChy pafChy;
    [SerializeField] private LineRenderer line;

    public PafPhy(Roster roster) : base(roster) { }

    public void tacticalUpdate()
    {
        DrawLine();
    }



    void DrawLine()
    {
        /* TODO SEMIURGENT
        Paf paf = GetPaf();
        //List<Tile> tilesToCross = TileTracker.GetTilesByIds(tileIdsToCross);

        line = GetComponent<LineRenderer>();

        line.numCapVertices = endVertices;
        line.numCornerVertices = cornerVertices;

        line.positionCount = 0;
        vertexCount = 0;

        if (paf == null) return;

        for (int i = 0; i < paf.GetBreadcrumbCount(); i++)
        {
            TilePhy tile = paf.GetBreadcrumb(i).getTile();
            NewVertex(tile, tile);
        }
        /*  for height variation
        for (int i = 0; i < path.GetBreadcrumbCount(); i++)
        {
            Tile a;
            Tile b = TileTracker.GetTileById(path.GetTileId(i));
            if (i > 0)  // DEFAULT: MAKE PREVIOUS AND CURRENT VERTEX
            {
                a = TileTracker.GetTileById(path.GetTileId(i-1));
                float aHeight = a.geo.height;
                float bHeight = b.geo.height;
                if (bHeight > aHeight)  // CLIMB UP FIRST
                {
                    NewVertex(a, b);
                } else
                {
                    NewVertex(b, a);
                }
            }
            NewVertex(b, b);
        }
        */
    }
    private void NewVertex(TilePhy xzTile, TilePhy yTile)
    {
        /*
        line.positionCount++;
        line.SetPosition(vertexCount, xzTile.GetLinePosAtHeightOf(yTile));
        vertexCount++;
        */
    }


    public override Chy getChy()
    {
        return pafChy;
    }

    protected override void setChy(Chy chy)
    {
        pafChy = (PafChy)chy;  // god this is dirty
    }

    protected override GameObject getPrefab()
    {
        return getGM().lineGOPrefab;
    }
}
