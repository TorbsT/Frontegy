using UnityEngine;

public abstract class View
{
    private Phase phase;
    protected int life;
    private Cam cam;
    private UIManager uiManager;
    protected Player Player { get { return phase.getPhasePlayer(); } }
    public View(Phase phase)
    {
        if (phase == null) Debug.LogError("IllegalArgumentException");
        this.phase = phase;
        life = 0;
        cam = phase.getCam();
    }
    public bool bupdate(Control c)
    {
        bool finished = bupdateVirtual(c);
        life++;
        return finished;
    }

    protected abstract bool bupdateVirtual(Control c);
    protected SelMan getSelectionManager() { return getPhase().getSelectionManager(); }
    protected UIManager getUiManager() { return getPhase().getUiManager(); }
    protected Cam getCam() { return getPhase().getCam(); }
    protected Groop getAllGroop() { return getPhase().getAllGroop(); }
    public Grid getGrid() { return getPhase().getGrid(); }
    protected PhaseManager getPhaseManager() { return getPhase().getPhaseManager(); }
    protected Phase getPhase() { if (phase == null) Debug.LogError("IllegalStateException: i feel like this will be relevant when resetting game"); return phase; }
}
