using UnityEngine;
public class Role
{  // This *could* be a class, which would make it possible to modify global role stats. But it would take some work
    // 21/05/05 - wdym it would take some work? Making it a class should be better for performance I think. Changing
    private string name;
    private RoleStats stats;
    public Role(string name, RoleStats stats)
    {
        if (name == null) Debug.LogError("IllegalArgumentException: Role.name = null");
        this.name = name;
        this.stats = stats;
    }

    public string getName() { if (name == null) Debug.LogError("IllegalStateException: role name == null"); return name; }
    public RoleStats getStats() { return stats; }
}
