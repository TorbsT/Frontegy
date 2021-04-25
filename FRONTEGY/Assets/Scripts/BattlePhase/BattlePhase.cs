using System.Collections.Generic;

public class BattlePhase : Phase
{
    private Results results;
    View v;
    int stepId;
    int stepCount = -1;

    public BattlePhase()
    {
        // runs after Phase construction!
        results = new Results();
        stepId = 0;
        makeWeiterView();
        stepCount = results.getMaxSteps();
        setType(PhaseType.battle);
    }

    protected override bool bupdateVirtual()
    {
        bool done = false;

        bool stepDone = v.bupdate();

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
    private Results getResults() { return results; }

    public Coonflict getStepCoonflict(int step) { return results.getStepCoonflict(step); }
}
