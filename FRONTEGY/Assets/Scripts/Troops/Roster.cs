using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Roster
{  // Provides a way to reuse GameObjects, and by extension MonoBehaviour scripts.
    // 21/06/28 - removed inheritance and relation to Rooster
    // 21/06/29 - made generic
    // 21/06/30 (or something) - using Pool instead.
    /*
    private static Roster roster { get { if (_roster == null) _roster = GameMaster.GetGM().roster; return _roster; } }
    private static Roster _roster;

    private List<PhyPlan> phyPlans { get { if (_phyPlans.Count == 0) Debug.LogError("InspectorException: no PhyPlans in Roster"); return phyPlans; } }

    [Header("Assign")]
    [SerializeField] private List<PhyPlan> _phyPlans;

    [Header("Observe")]
    [SerializeReference] private List<Phy> phys;

    public Roster(GameObject go, int count)
    {
        if (go == null) Debug.LogError("IllegalArgumentException");
        phys = new List<Phy>();
        for (int i = 0; i < count; i++)
        {
            phys.Add(Object.Instantiate(go).GetComponent<Phy>());
        }
    }

    public void unstageAll()
    {
        foreach (Phy p in phys)
        {
            if (p.staged) p.chy.unstage();
        }
    }

    public static void findAndStage(Chy chy, int id)
    {
        roster._findAndStage(chy, id);
    }



    private void _findAndStage(Chy chy, int id)
    {
        if (id >= phyPlans.Count) Debug.LogError("IllegalArgumentException");
        if (chy == null) Debug.LogError("IllegalArgumentException");
        // Here,
        // id: refers to an index in phyPlans
        // index: refers to an index in phys

        int startIndex = 0;
        for (int i = 0; i < id; i++)
        {
            startIndex += phyPlans[i].count;
        }

        PhyPlan plan = phyPlans[id];

        for (int i = 0; i < plan.count; i++)
        {
            int checkIndex = startIndex + plan.looper;
            Phy checkPhy = phys[checkIndex];
            if (checkPhy.staged) continue;
            checkPhy.stage(chy);
            return;
        }
        Debug.LogWarning("No phys available for " + chy);
    }
    */
}
