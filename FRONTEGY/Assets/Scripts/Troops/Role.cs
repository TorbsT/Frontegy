public struct Role
{
    public string name;
    public string displayName;
    public int priority;
    public UnitStats defaultRoleStats;
    public Role(string _name, string _displayName, int _priority, UnitStats _defaultRoleStats)
    {
        name = _name;
        displayName = _displayName;
        priority = _priority;
        defaultRoleStats = _defaultRoleStats;
    }
}
