using System.Collections.Generic;
using UnityEngine;

public class BattlePhase : Phase
{
    View v;
    private int _stepId;
    private int _totalSteps;

    public BattlePhase(Round round, Player phasePlayer) : base(round, phasePlayer)
    {
        // runs after Phase construction!
        _stepId = 0;
        makeWeiterView();
        _totalSteps = getResults().maxSteps;
        setType(PhaseType.battle);
    }
    protected override void startAbstra()
    {
        UIManager.Instance.battleStart();
    }
    protected override bool bupdateAbstra(Control c)
    {
        bool done = false;

        bool stepDone = v.bupdate(c);
        //Debug.Log(stepDone + " cock");

        if (stepDone)
        {
            UnityEngine.Debug.Log("Done with step " + _stepId+" out of "+_totalSteps);
            _stepId++;
            done = _stepId > _totalSteps;
            if (!done) makeWeiterView();
        }

        return done;
    }

    private void makeWeiterView() { v = new WeiterView(_stepId, this); }
    private int getConflictCount() { if (getAllCoonflict() == null) return 0; return getAllCoonflict().getCount(); }
    private Coonflict getAllCoonflict() { return getResults().getAllCoonflict(); }
    private Results getResults() { return getRound().getResults(); }  // not in Phase because not all should have access to Results

    public Coonflict getStepCoonflict(int step) { return getResults().getStepCoonflict(step); }
}
