using System.Collections.Generic;
using UnityEngine;

public class Groop
{  // group of troops
    // TODO make AllGroop
    private List<Troop> troops;

    public Groop() { }
    public Groop(List<Troop> troops)
    {
        this.troops = troops;
    }
    public int getMaxSteps()
    {  // This here is some good shit
        int longest = 0;
        foreach (PafChy pafChy in getPafs())
        {
            int thisLength = pafChy.getPafCount();
            if (thisLength > longest) longest = thisLength;
        }
        return longest;
    }
    private List<PafChy> getPafs()
    {
        List<PafChy> pafs = new List<PafChy>();
        foreach (Troop troop in troops)
        {
            PafChy paf = troop.getPafChy();
            pafs.Add(paf);
        }
        return pafs;
    }
    private bool troopOutOfBounds(int index) { return index < 0 || index >= getCount(); }
    private bool noTroops() { return getCount() == 0; }
    public int getCount()
    {
        if (troops == null) return 0;
        return troops.Count;
    }
    public void resetParentTiles()
    {
        foreach (Troop troop in getTroops())
        {
            troop.resetParentTile();
        }
    }
    private Troop getTroop(int index)
    {
        if (troopOutOfBounds(index))
        {
            Debug.LogError("Should probably not happen");
            return null;
        }
        return troops[index];
    }
    public void add(Troop t) { getTroops().Add(t); }
    private List<Troop> getTroops()
    {
        if (troops == null) Debug.LogError("Should never happen");
        return troops;
    }
    
    public Coonflict getCoonflict(int step, Consequi consequi)
    {  // coonflict not stored in state, rather computed
        Coonflict coonflict = new Coonflict();

        int troopCount = getCount();  // doesn't change throughout this algorithm
        if (troopCount <= 0) Debug.LogError("This should never happen");
        for (int a = 0; a < troopCount; a++)
        {
            Troop at = getTroop(a);


            if (consequi.deadBy(step, at)) continue;


            for (int b = a+1; b < troopCount; b++)
            {
                Troop bt = getTroop(b);


                if (consequi.deadBy(step, bt)) continue;


                Conflict newConflict = at.findConflictByStepAndTroop(step, bt);
                if (newConflict == null) continue;  // no suitable conflict found


                coonflict.mergeConflict(newConflict);  // should merge appropriately
            }
        }
        return coonflict;
    }
    public void weiterUpdate(WeiterView wv)
    {
        foreach (Troop t in troops)
        {
            t.weiterUpdate(wv);
        }
    }
}
