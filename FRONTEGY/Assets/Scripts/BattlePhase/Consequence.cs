public class Consequence
{
    public int roundId { get => _roundId; }
    public int troopId { get => _troopId; }
    public int step { get => _step; }
    public bool dies { get => _dies; }

    private int _roundId;
    private int _troopId;
    private int _step;
    private bool _dies;
    // Troop and step combined must be unique
    public Consequence(int roundId, int step, int troopId, bool dies)
    {
        _roundId = roundId;
        _troopId = troopId;
        _step = step;
        _dies = dies;
    }
}
