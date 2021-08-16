using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Roole : IVerifiable
{  // 21/05/25 - Maybe better to name them Ace, 2, King etc instead of Spy, Private, King etc (more user friendly)
    public List<Role> roles { get { return _roles; } }

    [SerializeField] private List<Role> _roles = new List<Role>();
    private List<SummonCardBP> _summonCardBPs;
    public static Roole Instance;

    public void init()
    {
        Instance = this;
        verify();
    }
    public void verify()
    {
        if (roles == null) Debug.LogError("IllegalStateException");
        if (roles.Count == 0) Debug.LogError("InspectorException: Assign some roles in inspector first!");
    }
    public Role getRole(int id)
    {
        foreach (Role role in _roles)
        {
            if (role.id == id) return role;
        }
        Debug.LogError("IllegalStateException: Not all RoleTypes are represented as Roles, or invalid int casted to roletype?");
        return null;
    }
}
