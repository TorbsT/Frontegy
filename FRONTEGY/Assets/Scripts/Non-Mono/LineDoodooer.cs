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
    public UnitScript ownerUnit;
    [SerializeField] public LineRenderer line;
    int vertexCount;
    bool isInitialized = false;


    void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
    void Update()
    {
        if (gameMaster.phase.type.name == "strategic") DrawLine(ownerUnit.SetStats().path);
    }

    void DrawLine(List<Vector2Int> tilePosesToCross)
    {
        List<Tile> tilesToCross = TileTracker.GetTilesByPoses(tilePosesToCross);

        line = GetComponent<LineRenderer>();

        line.numCapVertices = endVertices;
        line.numCornerVertices = cornerVertices;

        line.positionCount = 0;
        vertexCount = 0;

        if (tilesToCross == null) return;


        for (int i = 0; i < tilesToCross.Count; i++)
        {
            Tile a = null;
            Tile b = tilesToCross[i];
            if (i > 0)  // DEFAULT: MAKE PREVIOUS AND CURRENT VERTEX
            {
                a = tilesToCross[i - 1];
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
