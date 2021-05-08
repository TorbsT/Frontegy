using UnityEngine;

public abstract class View
{
    private Phase phase;
    protected int life;
    protected CameraScript cs;
    public View(Phase phase)
    {
        if (phase == null) Debug.LogError("IllegalArgumentException");
        this.phase = phase;
        life = 0;
        cs = GameMaster.getCameraScript();
    }
    public bool bupdate()
    {
        bool finished = bupdateVirtual();
        life++;
        return finished;
    }
    protected abstract bool bupdateVirtual();
    protected Groop getAllGroop() { return getGrid().getAllGroop(); }
    protected Grid getGrid() { return getPhaseManager().getGrid(); }
    protected PhaseManager getPhaseManager() { return getPhase().getPhaseManager(); }
    protected Phase getPhase() { if (phase == null) Debug.LogError("IllegalStateException: i feel like this will be relevant when resetting game"); return phase; }
}
