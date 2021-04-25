using UnityEngine;

[System.Serializable]  // for objectcopiers sake
public struct Breadcrumb
{  // For scalability reasons class is better, but maybe struct is performance-wise better so idk
    private Tile tile;
    private int stepsRemaining;
    private int step;

    public Breadcrumb(Tile tile, int stepsRemaining, int step)
    {
        this.tile = tile;
        this.stepsRemaining = stepsRemaining;
        this.step = step;
    }

    // MAYBUG public bool IsInvalidTile() { return GetTileId() < 0 || GetTileId() >= TileTracker.GetTileCount(); }
    public bool defBetterThan(Breadcrumb b)
    {
        return sameTileAs(b) && betterThan(b);
    }
    public bool betterThan(Breadcrumb b)
    {  // NOT NECESSARILY SAME TILE! check defBetterThan()
        return stepsRemaining > b.GetStepsRemaining();
    }
    public bool isNeigOfBC(Breadcrumb b) { return isNeigOfTile(b.getTile()); }
    public bool isNeigOfTile(Tile t) { return getTile().isNeigOfTile(t); }
    public bool sameTileAs(Breadcrumb b) { return isTile(b.getTile()); }
    public bool isTile(Tile t) { return getTile().sameTile(t); }
    public Tiile getNeigTiile() { return getTile().getNeigTiile(); }
    public Tile getTile() { if (isInvalid()) Debug.LogError("TRIED USING INVALID BREADCRUMB"); return tile; }
    public int GetStepsRemaining() { if (isInvalid()) Debug.LogError("TRIED USING INVALID BREADCRUMB"); return stepsRemaining; }
    public int getStep() { if (isInvalid()) Debug.LogError("TRIED USING INVALID BREADCRUMB"); return step; }
    public bool isInvalid() { return !isValid(); }  
    public bool isValid() { return tile != null; } // don't use getTile() here
    public void showMark() { getTile().showMark(this); }
    public void hideMark() { getTile().hideMark(); }

    public static Breadcrumb makeInvalid()
    {
        return new Breadcrumb(null, -1, -1);
    }
    public static Breadcrumb makeStarter(Tile t, int stepsRemaining)
    {
        return new Breadcrumb(t, stepsRemaining, 0);
    }
    public static Breadcrumb makeNeig(Tile t, int stepsRemaining, int step)
    {
        return new Breadcrumb(t, stepsRemaining - 1, step+1);
    }
}
