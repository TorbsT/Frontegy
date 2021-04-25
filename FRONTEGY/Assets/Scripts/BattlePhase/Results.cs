using System.Collections.Generic;
using UnityEngine;  // Debug.LogError()

public class Results
{
    // Results has many TroopFates and List<list<conflict
    // TroopFate has many FateEvents
    // FateEvent is split up into different events:

    // not go in conflict
    // go in conflict (border, both attack)
    // go in conflict (gather, both attack)
    // go in conflict (on attacking side)
    // go in conflict (on defending side)
    // Start is called before the first frame update

    private Coonflict allCoonflict;
    private Consequi consequi;
    private int maxSteps;
    public Results()  // correct input?
    {
        allCoonflict = new Coonflict();
        consequi = new Consequi();

        Groop allGroop = GameMaster.getAllGroop();
        /*
        List<Paf> veryTempPafs = new List<Paf>();
        foreach (Troop troop in allTroops)
        {
            veryTempPafs.Add(troop.GetPaf());  // you're going to brazilø
        }
        Groop pafs = new Groop(veryTempPafs);
        veryTempPafs = null;  // get rid of that
        */

        // check for colliding pafs.
        // do this by going step for step and look for collisions
        maxSteps = allGroop.getMaxSteps();  // YES
        for (int step = 0; step < maxSteps; step++)
        {
            // Every step, this checks for coonflict that appears during that step.
            // When calculating coonflict on a step, these parameters are necessary:
            // step: all coonflict on the same step happen simultaneously and can therefore be calculated simultaneously.
            // consequi: information on what has happened previous steps. on first step, this is an empty Consequi.
            Coonflict coonflict = allGroop.getCoonflict(step, consequi);

            // These coonflict have consequi, which should be taken into account on next step.
            consequi.merge(coonflict.getConsequi());

            // Logs which coonflict have passed. Can be used to replay in scene.
            allCoonflict.merge(coonflict);
        }
        /*
         pafs = new list
         for all troops:
            add paf to pafs
        // check for colliding pafs.
        // do this by going step for step and look for collisions
        for i = 0, i < maxsteps:
            fromToTiles = new list of vector2int
            
            for all paf in pafs:
                // register all fromToTiles of this step
                newFromTo = paf[i].getfromto
                fromToTiles.add(newFromTo)
            
            // find collisions
            for a = 0; a < fromToTiles.count:
                if a dead by now skip
                for b = a+1; b < fromToTiles.count:
                    if b dead by now skip
                    // check if a and b collide
                    bool collideOnTile = (a.to == b.to)
                    bool collideOnBorder = (a.to == b.from && a.from == b.to)
                    // find neat way to go from this to list<conflict>
                    
            list<conflict> stepconflicts
            // maybe add conflicts for this step to a list of all conflicts in phase
            
            // do the actual battles
            for every conflict in stepconflicts:
                if everyoneinvolved is on same team: continue
                for every troop in conflict.losingside:
                    setFateDeath(troop, i);
                    
            
         */


    }

    // delegation
    public int getMaxSteps() { return maxSteps; }
    public Coonflict getStepCoonflict(int step) { return getAllCoonflict().getStepCoonflict(step); }  // TODO getters
    public Coonflict getAllCoonflict() { return allCoonflict; }
}
