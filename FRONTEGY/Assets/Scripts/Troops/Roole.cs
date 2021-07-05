using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Roole : IVerifiable
{  // 21/05/25 - Maybe better to name them Ace, 2, King etc instead of Spy, Private, King etc (more user friendly)
    [SerializeField] private List<Role> roles = new List<Role>();
    private static List<Role> staticRoles;


    public Roole(List<Role> roles)
    {
        this.roles = roles;
        verify();
    }


    public void applyRoles()
    {
        verify();
        staticRoles = roles;
    }


    public void verify()
    {
        if (roles == null) Debug.LogError("IllegalStateException");
        if (roles.Count == 0) Debug.LogError("InspectorException: Assign some roles in inspector first!");
    }


    public static Role getRole(int id)
    {
        foreach (Role role in getRoles())
        {
            if (role.isId(id)) return role;
        }
        Debug.LogError("IllegalStateException: Not all RoleTypes are represented as Roles, or invalid int casted to roletype?");
        return null;
    }
    public static List<Role> getRoles()
    {
        if (staticRoles == null) Debug.LogError("IllegalStateException: static roles have not been set but tried accessing");
        return staticRoles;
    }

    private static bool outOfRange(int index)
    {
        return index < 0 || index >= getRoleCount();
    }
    private static int getRoleCount()
    {
        return getRoles().Count;
    }
}
