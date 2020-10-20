using System.Collections.Generic;

public static class StaticRoles
{
    public static List<Role> roles = new List<Role>
    {
        new Role("private", "Prv.", 1,    new UnitStats(1f, 2f, 1f)),
        new Role("lieutenant", "Ltn.", 2, new UnitStats(1f, 3f, 2f)),
        new Role("major", "Major", 3,     new UnitStats(1f, 4f, 3f)),
        new Role("captain", "Capt.", 4,   new UnitStats(1f, 5f, 4f)),
        new Role("colonel", "Col.", 5,    new UnitStats(1f, 6f, 5f)),
        new Role("general", "Gen.", 6,    new UnitStats(1f, 7f, 6f)),
        new Role("king", "King", 7,       new UnitStats(2f, 8f, 7f)),
        new Role("spy", "Spy", 8,         new UnitStats(1f, 1f, 0f))
    };

}
