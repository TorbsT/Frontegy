using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Phase
{
    private int life;
    private Round round;
    private Player phasePlayer;
    private PhaseType type;

    public Phase(Round round, Player phasePlayer)
    {
        if (round == null) Debug.LogError("IllegalArgumentException");
        this.round = round;
        if (phasePlayer == null) Debug.LogError("IllegalArgumentException");
        this.phasePlayer = phasePlayer;
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
        if (phasePlayer == null) Debug.LogError("IllegalStateException");
        return phasePlayer;
    }
    protected int getLife() { return life; }  // never

    protected abstract void startAbstra();
    protected abstract bool bupdateAbstra(Control c);


    public SelMan getSelectionManager() { return getRound().getSelectionManager(); }
    public PhaseManager getPhaseManager() { return getRound().getPhaseManager(); }
    public Groop getAllGroop() { return getRound().getAllGroop(); }
    public UIManager getUiManager() { return getRound().getUiManager(); }
    public Grid getGrid() { return getRound().getGrid(); }
    public Cam getCam() { return getRound().getCam(); }
    public Round getRound() { if (round == null) Debug.LogError("IllegalStateException"); return round; }
}
