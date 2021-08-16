using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Phase
{
    public static Phase Instance;

    public int roundId { get => round.roundId; }
    public int phaseOwnerId { get => phaseOwner.id; }
    public bool canSkipNow { get; set; }

    private int life;
    private Round round;
    private Player phaseOwner;
    private PhaseType type;

    public Phase(Round round, Player phaseOwner)
    {
        Instance = this;
        if (round == null) Debug.LogError("IllegalArgumentException");
        this.round = round;
        if (phaseOwner == null) Debug.LogError("IllegalArgumentException");
        this.phaseOwner = phaseOwner;
        // runs before the construction of any subclass!
    }
    public bool isType(PhaseType type) { return this.type == type; }
    public PhaseType getType() { return type; }
    protected void setType(PhaseType t) { type = t; }
    public bool bupdate(Control c)
    {
        // common phase update method goes here
        if (life == 0) startAbstra();
        bool x = bupdateAbstra(c);
        life++;
        return x;
    }
    public Player getPhasePlayer()
    {
        if (phaseOwner == null) Debug.LogError("IllegalStateException");
        return phaseOwner;
    }
    public bool trySkip()
    {
        if (canSkipNow) return round.skipPhase();
        return false;
    }
    protected int getLife() { return life; }  // never

    protected abstract void startAbstra();
    protected abstract bool bupdateAbstra(Control c);
    public Round getRound() { if (round == null) Debug.LogError("IllegalStateException"); return round; }
}
