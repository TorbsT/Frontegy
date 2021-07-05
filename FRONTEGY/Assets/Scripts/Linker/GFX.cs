using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GFX
{  // Provides a dictionary on renderers, materials and colors used in a GO.
    // as of now, public methods only use enums as arguments.
    [SerializeField] private string name;
    [SerializeField] private Reend reend;
    [SerializeField] private Maat maat;
    [SerializeField] private Cool cool;

    public void setMatAtPlace(MatPlace matPlace, RendPlace rendPlace)
    {
        bool success = getReend().setMatAtPlace(getMatAtPlace(matPlace), rendPlace);
        if (!success) rendNotFound(rendPlace);
    }
    public void setColAtPlace(MatPlace matPlace, RendPlace rendPlace)
    {
        bool success = getReend().setColAtPlace(getColAtPlace(matPlace), rendPlace);
        if (!success) rendNotFound(rendPlace);
    }
    public void setFloat(RendPlace rendPlace, string name, float f)
    {
        bool success = getReend().setFloatAtPlace(rendPlace, name, f);
        if (!success) rendNotFound(rendPlace);
    }


    private void rendNotFound(RendPlace place)
    {
        Debug.LogError("InspectorException: '" + this + "' has no assigned RendPlace.'" + place + "'");
    }
    private void matNotFound(MatPlace place)
    {
        Debug.LogError("InspectorException: '" + this + "' has no assigned MatPlace.'" + place + "'");
    }
    private void colNotFound(MatPlace place)
    {
        Debug.LogError("InspectorException: '" + this + "' has no assigned ColPlace.'" + place + "'");
    }
    private Mat getMatAtPlace(MatPlace place)
    {
        Mat mat = getMaat().getMat(place);
        if (mat == null) matNotFound(place);
        return mat;
    }
    private Col getColAtPlace(MatPlace place)
    {
        Col col = getCool().getCol(place);
        if (col == null) colNotFound(place);
        return col;
    }
    private Reend getReend()
    {
        if (reend == null) Debug.LogError("IllegalStateException");
        return reend;
    }
    private Maat getMaat()
    {
        if (maat == null) Debug.LogError("IllegalStateException");
        return maat;
    }
    private Cool getCool()
    {
        if (cool == null) Debug.LogError("IllegalStateException");
        return cool;
    }
    public override string ToString()
    {
        if (name == null || name == "")
        {
            Debug.LogError("Found an unnamed GFX, name it to help debugging");
            return "Unnamed GFX";
        }
        return name + ".GFX";
    }
}
