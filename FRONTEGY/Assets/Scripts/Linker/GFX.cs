using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GFX
{  // Provides a dictionary on renderers, materials and colors used in a GO.
    // as of now, public methods only use enums as arguments.
    public static Mat notFoundMat { get { return GameMaster.GetGM().notFoundMat; } }
    [SerializeField] private string name;
    [SerializeField] private Reend reend;
    [SerializeField] private Maat maat;
    [SerializeField] private Cool cool;

    public void setMatAtPlace(MatPlace matPlace, RendPlace rendPlace)
    {
        Mat mat = getMatAtPlace(matPlace);
        if (mat == null)
        {
            mat = notFoundMat;
            Debug.LogWarning(this + " tried setting matPlace '" + matPlace + "' on RendPlace '" + rendPlace + "', but that matPlace wasn't assigned in inspector.");
        }
        bool success = getReend().setMatAtPlace(mat, rendPlace);
        if (!success)
        {
            Debug.LogError(this + " tried setting matPlace '" + matPlace + "' on RendPlace '" + rendPlace + "', but that rendPlace wasn't assigned in inspector.");
        }
    }
    public void setColAtPlace(MatPlace matPlace, RendPlace rendPlace)
    {
        Col col = getColAtPlace(matPlace);
        if (col == null)
        {
            Debug.LogWarning(this + " tried setting colPlace '" + matPlace + "' on RendPlace '" + rendPlace + "', but that matPlace wasn't assigned in inspector.");
            setMatAtPlace(MatPlace.none, rendPlace);
        }
        bool success = getReend().setColAtPlace(col, rendPlace);
        if (!success)
        {
            Debug.LogError(this + " tried setting colPlace '" + matPlace + "' on RendPlace '" + rendPlace + "', but that rendPlace wasn't assigned in inspector.");
        }
    }
    public void setFloat(RendPlace rendPlace, string name, float f)
    {
        bool success = getReend().setFloatAtPlace(rendPlace, name, f);
        if (!success)
        {
            Debug.LogError(this + " tried setting float '" + name + "' = "+f+" on RendPlace '"+rendPlace+"', but that rendPlace wasn't assigned in inspector.");
        }
    }

    private Mat getMatAtPlace(MatPlace place)
    {
        Mat mat = getMaat().getMat(place);
        return mat;
    }
    private Col getColAtPlace(MatPlace place)
    {
        Col col = getCool().getCol(place);
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
