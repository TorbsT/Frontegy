[System.Serializable]  // for objectcopiers sake
public struct Breadcrumb
{
    private int tileId;
    private int stepsRemaining;
    private int stepId;

    public Breadcrumb(int _tileId, int _stepsRemaining, int _stepId)
    {
        tileId = _tileId;
        stepsRemaining = _stepsRemaining;
        stepId = _stepId;
    }

    public bool IsInvalidTile() { return GetTileId() < 0 || GetTileId() >= TileTracker.GetTileCount(); }
    public Tile GetTile() { return TileTracker.GetTileById(GetTileId()); }
    public int GetTileId() { return tileId; }
    public int GetStepsRemaining() { return stepsRemaining; }
    public int GetStepId() { return stepId; }
}
