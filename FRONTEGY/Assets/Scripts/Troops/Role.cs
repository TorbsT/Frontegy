using UnityEngine;

[CreateAssetMenu(fileName = "Role", menuName = "ScriptableObjects/Role", order = 1)]
[System.Serializable]
public class Role : ScriptableObject
{  // This *could* be a class, which would make it possible to modify global role stats. But it would take some work
    // 21/05/05 - wdym it would take some work? Making it a class should be better for performance I think. Changing
    public int id { get { return _id; } }
    public RoleStats baseStats { get { return _baseStats; } }
    
    [SerializeField] private int _id;
    [SerializeField] private RoleStats _baseStats;
    [SerializeField] private Role trumpRole;


    public override string ToString() { return "{"+name+"}"; }
    public bool trumps(Role role) { return role == trumpRole; }
}
