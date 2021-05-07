using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public Unit(Role role)
    {
        if (role == null) 
        this.role = role;
        dead = false;
    }

    private Player player;
    private Role role;
    private bool dead;
    //public List<ActiveCard> activeCards;
    //private Moodifier moodifier  // stores all stat modifiers
    public int getPow()
    {
        return getModdedStats().getPOW();
    }
    public int getRange()
    {
        return getModdedStats().getRANGE();  // verygood (unironically) 
    }
    public Player getPlayer()
    {
        if (player == null) Debug.LogError("IllegalStateException: Unit.player == null");
        return player;
    }
    public RoleStats getModdedStats()
    {
        return getBaseStats().getModded();
    }
    private RoleStats getBaseStats()
    {
        return getRole().getStats();
    }
    public Role getRole()
    {
        if (role == null) Debug.LogError("IllegalStateException: Unit.role == null");
        return role;
    }
    public bool isDead() { return dead; }
    public void die() { if (dead) Debug.LogError("IllegalStateException: Can't die twice"); dead = true; }
}
