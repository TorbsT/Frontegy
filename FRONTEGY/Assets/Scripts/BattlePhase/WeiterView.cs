using System.Collections.Generic;
using UnityEngine;

public class WeiterView : View
{
    private Slid slid;  // [0, 1]
    private int step;
    private BattlePhase bp;
    private Coonflict coonflict;
    public WeiterView(int step, BattlePhase bp) : base(bp)
    {
        this.step = step;
        this.bp = bp;
        slid = new Slid(0f);
        coonflict = bp.getStepCoonflict(step);
        // Should find some fancy way to auto-determine the pafs every troop will have in the end, also their fate? huummmmmmm
    }
    protected override bool bupdateVirtual()
    {
        addTime(0.01f);
        getAllGroop().weiterUpdate(this);
        return isDone();
    }
    private bool isDone() { return getSlid().isDone(); }
    private void addTime(float t)
    {
        slid.add(t);
    }
    public int getStep() { return step; }
    public Slid getSlid() { return slid; }
}
