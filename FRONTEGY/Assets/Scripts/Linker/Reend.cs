using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Reend
{
    [SerializeField] private List<Rend> rends;

    public void setMat(Mat mat)
    {
        foreach (Rend r in getRends())
        {
            r.setMat(mat);
        }
    }
    public void setCol(Col col)
    {
        foreach (Rend r in getRends())
        {
            r.setCol(col);
        }
    }
    public bool setMatAtPlace(Mat mat, RendPlace place)
    {
        int counter = 0;
        foreach (Rend r in getRends())
        {
            if (r.isPlace(place))
            {
                r.setMat(mat);
                counter++;
            }
        }
        bool success = counter > 0;
        return success;
    }
    public bool setColAtPlace(Col col, RendPlace place)
    {
        int counter = 0;
        foreach (Rend r in getRends())
        {
            if (r.isPlace(place))
            {
                r.setCol(col);
                counter++;
            }
        }
        bool success = counter > 0;
        return success;
    }
    public bool setFloatAtPlace(RendPlace place, string name, float f)
    {
        int counter = 0;
        foreach (Rend r in getRends())
        {
            if (r.isPlace(place))
            {
                r.setFloat(name, f);
                counter++;
            }
        }
        bool success = counter > 0;
        return success;
    }

    private List<Rend> getRends()
    {
        if (rends == null) Debug.LogError("InspectorException: Reend.rends in '" + this + "' is null");
        //if (rends.Count == 0) Debug.LogError("InspectorException: Reend.rends in '" + this + "' is empty");
        return rends;
    }
}
