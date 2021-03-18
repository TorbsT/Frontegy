using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineDoodooer : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] int cornerVertices;
    [SerializeField] int endVertices;

    [Header("System")]
    GameMaster gm;
    public Troop ownerTroop;
    [SerializeField] public LineRenderer line;
    int vertexCount;
    bool isInitialized = false;


    void Start()
    {
        gm = GameMaster.GetGM();
    }
    void Update()
    {
        if (gm.isThisPhase(StaticPhaseType.strategic))
        {
            DrawLine();
        }
    }
    void DrawLine()
    {
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
            Tile tile = paf.GetBreadcrumb(i).GetTile();
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
    private Paf GetPaf()
    {
        if (ownerTroop == null) Debug.LogError("LineDooDooer has no ownerTroop");  // should never happen me thinks?
        return ownerTroop.stats.GetPaf();
    }
    void NewVertex(Tile xzTile, Tile yTile)
    {
        line.positionCount++;
        line.SetPosition(vertexCount, xzTile.GetLinePosAtHeightOf(yTile));
        vertexCount++;
    }
}
