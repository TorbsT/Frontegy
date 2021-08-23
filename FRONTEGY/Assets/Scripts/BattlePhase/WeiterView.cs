using System.Collections.Generic;
using UnityEngine;

public class WeiterView : View
{
    public Slid slid { get => _slid; }
    public int step { get => _step; }

    private Slid _slid;  // [0, 1]
    private int _step;
    private BattlePhase _bp;
    private Coonflict _coonflict;
    public WeiterView(int step, BattlePhase bp) : base(bp)
    {
        this._step = step;
        this._bp = bp;
        _slid = new Slid(0f);
        _coonflict = bp.getStepCoonflict(step);

        Debug.Log("YEP");
        foreach (Troop troop in AllGroop.Instance.get())
        {
            troop.weiterViewStart(this);
        }
    }
    protected override bool bupdateVirtual(Control c)
    {
        Debug.Log("YEP2");
        addTime(0.01f);
        foreach (Troop troop in AllGroop.Instance.get())
        {
            Debug.Log("YEP3");
            troop.weiterViewUpdate(this);
        }
        return isDone();
    }
    private bool isDone() { return getSlid().isDone(); }
    private void addTime(float t)
    {
        _slid += t;
    }
    public int getStep() { return _step; }
    public Slid getSlid() { return _slid; }
}
