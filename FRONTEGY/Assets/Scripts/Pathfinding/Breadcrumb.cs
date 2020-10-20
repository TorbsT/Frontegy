[System.Serializable]  // for objectcopiers sake
public struct Breadcrumb
{
    public int tileId;
    public int stepsRemaining;
    public int stepId;

    public Breadcrumb(int _tileId, int _stepsRemaining, int _stepId)
    {
        tileId = _tileId;
        stepsRemaining = _stepsRemaining;
        stepId = _stepId;
    }
}
