using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public Unit(int roleId)
    {
        staticRoleId = roleId;
        ResetRoleToStatic();
    }

    public int staticRoleId = -1;
    public Role myRole;
    public bool isDead;
    //public List<ActiveCard> activeCards;

    public void ResetRoleToStatic()  // FIND A REALLY GOOD STURCTURE TO KEEP STATS AND ROLES AND SHIT
    {
        myRole = StaticRoles.GetRole(staticRoleId);
    }
    public void ResetStatsToStaticRole()
    {

    }
    public void DebugRole()
    {
        Debug.Log(myRole.name);
    }
}
