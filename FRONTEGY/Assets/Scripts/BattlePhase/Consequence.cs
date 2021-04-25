public class Consequence
{
    private Troop troop;
    private int step;
    // Troop and step combined must be unique
    private bool dies;
    public Consequence(int step, Troop troop)
    {
        this.troop = troop;
        this.step = step;

        this.dies = false;
    }

    public bool allMatches(int s, Troop t) { return stepMatches(s) && troopMatches(t); }
    public bool troopMatches(Troop t) { return troop.Equals(t); }
    public bool stepMatches(int s) { return s == step; }
    private Troop getTroop() { return troop; }
    private int getStep() { return step; }
    public bool getDies() { return dies; }
    public void setDies() { dies = true; }
}
