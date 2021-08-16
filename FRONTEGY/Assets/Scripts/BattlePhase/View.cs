using UnityEngine;

public abstract class View
{
    protected Cam cam { get => Cam.Instance; }
    protected Grid grid { get => Grid.Instance; }

    private Phase phase;
    protected int life;
    private UIManager uiManager;
    protected Player player { get { return phase.getPhasePlayer(); } }
    public View(Phase phase)
    {
        if (phase == null) Debug.LogError("IllegalArgumentException");
        this.phase = phase;
        life = 0;
    }
    public bool bupdate(Control c)
    {
        bool finished = bupdateVirtual(c);
        life++;
        return finished;
    }

    protected abstract bool bupdateVirtual(Control c);
}
