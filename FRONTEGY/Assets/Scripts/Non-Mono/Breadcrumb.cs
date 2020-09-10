
public struct Breadcrumb
{
    public int tileId;
    public int stepsRemaining;

    public Breadcrumb(int _tileId, int _stepsToReach)
    {
        tileId = _tileId;
        stepsRemaining = _stepsToReach;
    }
}
