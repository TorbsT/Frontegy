using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phase
{
    public Phase()
    {
        // runs before the construction of any subclass!
        viewIndex = 0;
        Debug.Log("this is a phase.");
    }
    protected List<View> views;
    private int viewIndex;


    public PhaseType type;

    public int step;
    public int steps;




    public bool bupdate()
    {
        if (noViews()) Debug.Log("No views!");
        View v = getCurrentView();
        if (v.bupdate()) viewIndex++;
        return allViewsDone();
    }

    public View getCurrentView()
    {
        if (indexOutOfRange()) Debug.LogError("Tried to access view with invalid viewIndex. index = "+viewIndex+", length = "+getViewCount());
        return views[viewIndex];
    }
    private bool indexOutOfRange() { return viewIndex < 0 || allViewsDone(); }
    private bool allViewsDone() { return viewIndex >= getViewCount(); }

    private bool noViews() { return getViewCount() == 0; }
    private int getViewCount()
    {
        if (views == null) return 0;
        return views.Count;
    }
}
