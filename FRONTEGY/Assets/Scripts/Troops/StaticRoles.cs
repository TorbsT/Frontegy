using System.Collections.Generic;

public static class StaticRoles
{
    public static List<Role> roles = new List<Role>
    {
        new Role("private", "Prv.", 1,    new UnitStats(1f, 2f, 1f, 2)),
        new Role("lieutenant", "Ltn.", 2, new UnitStats(1f, 3f, 2f, 2)),
        new Role("major", "Major", 3,     new UnitStats(1f, 4f, 3f, 2)),
        new Role("captain", "Capt.", 4,   new UnitStats(1f, 5f, 4f, 2)),
        new Role("colonel", "Col.", 5,    new UnitStats(1f, 6f, 5f, 2)),
        new Role("general", "Gen.", 6,    new UnitStats(1f, 7f, 6f, 2)),
        new Role("king", "King", 7,       new UnitStats(2f, 8f, 7f, 3)),
        new Role("spy", "Spy", 8,         new UnitStats(1f, 1f, 0f, 100))
    };
    public static Role GetRole(int id)
    {
        return roles[id];
    }
}
