using UnityEngine;

public struct PhaseType
{
    public string name;
    public float duration;
    public bool skippable;
    public bool skipIfFirstRound;

    public PhaseType(string name, float duration, bool skippable, bool skipIfFirstRound)
    {
        this.name = name;
        this.duration = duration;
        this.skippable = skippable;
        this.skipIfFirstRound = skipIfFirstRound;
    }
}
