public struct Role
{  // This *could* be a class, which would make it possible to modify global role stats. But it would take some work
    public string name;
    public string displayName;
    public int priority;
    public UnitStats stats;
    public Role(string _name, string _displayName, int _priority, UnitStats _stats)
    {
        name = _name;
        displayName = _displayName;
        priority = _priority;
        stats = _stats;
    }
}
