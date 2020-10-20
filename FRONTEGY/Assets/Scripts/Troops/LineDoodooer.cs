using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineDoodooer : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] int cornerVertices;
    [SerializeField] int endVertices;

    [Header("System")]
    GameMaster gameMaster;
    public Troop ownerUnit;
    [SerializeField] public LineRenderer line;
    int vertexCount;
    bool isInitialized = false;


    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
    void Update()
    {
        if (gameMaster.IsThisPhase(StaticPhaseType.strategic)) DrawLine(ownerUnit.stats.path);
    }

    void DrawLine(List<Breadcrumb> breadcrumbs)
    {
        //List<Tile> tilesToCross = TileTracker.GetTilesByIds(tileIdsToCross);

        line = GetComponent<LineRenderer>();

        line.numCapVertices = endVertices;
        line.numCornerVertices = cornerVertices;

        line.positionCount = 0;
        vertexCount = 0;

        if (breadcrumbs == null) return;


        for (int i = 0; i < breadcrumbs.Count; i++)
        {
            Tile a;
            Tile b = TileTracker.GetTileById(breadcrumbs[i].tileId);
            if (i > 0)  // DEFAULT: MAKE PREVIOUS AND CURRENT VERTEX
            {
                a = TileTracker.GetTileById(breadcrumbs[i - 1].tileId);
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
    }
    void NewVertex(Tile xzTile, Tile yTile)
    {
        line.positionCount++;
        line.SetPosition(vertexCount, xzTile.GetLinePosAtHeightOf(yTile));
        vertexCount++;
    }
}
