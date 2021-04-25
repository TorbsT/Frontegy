using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiile
{  // Forgiving: getter methods will not issue errors from delegations, but may return unexpected values
    // No assumptions made here.
    // Downside: longer search time
    // Upside: safer and more flexible
    private List<Tile> tiles;

    public Tiile() { tiles = new List<Tile>(); }  // private here, let's see what happens
    public Tiile(List<Tile> tiles) { if (tiles == null) Debug.LogError("Should never happen"); this.tiles = tiles; }

    protected Tile getTile(Loc loc)
    {  // OPT Slow
        foreach (Tile t in getTiles())
        {
            bool sameLoc = loc.sameLoc(t.getLoc());  // ^ readability, v speed
            if (sameLoc) return t;
        }
        return null;
    }
    public int getCount() { return getTiles().Count; }
    public List<Tile> getTiles()
    {
        if (tiles == null) Debug.LogError("Should never happen");
        return tiles;
    }
    public void add(Tile t)
    {
        getTiles().Add(t);
    }
    public virtual Tile find(TileLoc tileLoc)
    {  // may be null
        return getTile(tileLoc);
    }
    /*
    public Tiile GetRandomizedTiles()
    {
        Tiile tiile = AllTiile.genRectTiile();
        int remainingSpaces = TileTracker.GetTileCount();
        int remainingTiles = Mathf.RoundToInt(tileRatio * TileTracker.GetTileCount());

        int gridXPos = 0;
        int gridYPos = 0;

        List<int> reservoirSpawnWeights = new List<int>();
        foreach (Reservoir res in reservoirs)
        {
            reservoirSpawnWeights.Add(res.spawnWeight);
        }


        for (int i = 0; i < TileTracker.GetTileCount(); i++)
        {
            float prob = ((float)remainingTiles) / ((float)remainingSpaces);

            Geo blueprintGeo = new Geo();
            float thisRandomValue = Random.value;
            if (prob >= thisRandomValue)
            {
                blueprintGeo.isActive = true;
                remainingTiles--;
            }
            else blueprintGeo.isActive = false;


            blueprintGeo.reservoir = DecideReservoir(reservoirSpawnWeights);

            if (0.5f >= Random.value) blueprintGeo.playerId = 0;
            else blueprintGeo.playerId = 1;


            remainingSpaces--;
            blueprintGeo.height = 1f + gameMaster.heightScalar * Mathf.Round(Random.value * 10f) / 10f;
            blueprintGeo.id = i;
            blueprintGeo.gridPos = new Vector2Int(gridXPos, gridYPos);
            blueprintGeos.Add(blueprintGeo);

            gridXPos++;
            if (gridXPos >= gridSize[0])
            {
                gridXPos = 0;
                gridYPos++;
            }
        }
        return blueprintGeos;
    }
    Reservoir DecideReservoir(List<int> weights, bool debug = false)
    {
        int resWeightsSoFar = 0;
        float randomFloat = Random.value;
        int reservoirValue = Mathf.FloorToInt(randomFloat * Maffs.GetSumOfWeights(weights));
        if (debug) Debug.Log(randomFloat + " " + reservoirValue);
        for (int i = 0; i < weights.Count; i++)
        {
            int sW = weights[i] + resWeightsSoFar;
            if (debug) Debug.Log(sW + " " + weights[i] + " " + resWeightsSoFar);
            if (sW > reservoirValue)
            {
                if (debug) Debug.Log("true");
                return reservoirs[i];
            }
            if (debug) Debug.Log("false");
            resWeightsSoFar += weights[i];
        }
        Debug.LogError("FATAL ERROR ON GETTING RESERVOIR");
        return reservoirs[0];
    }
    */
}
