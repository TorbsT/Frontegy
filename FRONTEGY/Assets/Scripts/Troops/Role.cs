using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Role
{  // This *could* be a class, which would make it possible to modify global role stats. But it would take some work
    // 21/05/05 - wdym it would take some work? Making it a class should be better for performance I think. Changing
    [SerializeField] private string name;
    [SerializeField] private int id;
    [SerializeField] private MatPlace frontFGPlace;
    [SerializeField] private RoleStats stats;


    public bool isId(int id) { return id == this.id; }
    public string getName() { if (name == null) Debug.LogError("IllegalStateException: role name == null"); return name; }
    public RoleStats getStats() { return stats; }
    public MatPlace getFrontFGPlace() { return frontFGPlace; }
    public override string ToString() { return "{"+name+"}"; }
}
