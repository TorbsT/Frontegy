using System.Collections.Generic;

public class BattlePhase : Phase
{
    View v;
    int stepId;
    int stepCount = -1;

    public BattlePhase(Round round, Player phasePlayer) : base(round, phasePlayer)
    {
        // runs after Phase construction!
        stepId = 0;
        makeWeiterView();
        stepCount = getResults().getMaxSteps();
        setType(PhaseType.battle);
    }
    protected override void startAbstra()
    {
        getUiManager().battleStart();
    }
    protected override bool bupdateAbstra(Control c)
    {
        bool done = false;

        bool stepDone = v.bupdate(c);

        if (stepDone) tryMakeNextStep();

        return done;

        void tryMakeNextStep()
        {
            stepId++;
            done = noMoreSteps();
            if (!done) makeWeiterView();
        }
    }

    private void makeWeiterView() { v = new WeiterView(stepId, this); }
    private bool noMoreSteps() { return stepId >= stepCount; }
    private bool noConflicts() { return getConflictCount() == 0; }
    private int getConflictCount() { if (getAllCoonflict() == null) return 0; return getAllCoonflict().getCount(); }
    private Coonflict getAllCoonflict() { return getResults().getAllCoonflict(); }
    private Results getResults() { return getRound().getResults(); }  // not in Phase because not all should have access to Results

    public Coonflict getStepCoonflict(int step) { return getResults().getStepCoonflict(step); }
}
