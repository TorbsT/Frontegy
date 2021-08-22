using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GFX : MonoBehaviour
{  // Provides a dictionary on renderers, materials and colors used in a GO.
    // as of now, public methods only use enums as arguments.
    public static Mat notFoundMat { get { return GameMaster.GetGM().notFoundMat; } }
    [SerializeField] private List<Rend> _rends;
    [SerializeField] private List<Mat> _mats;
    [SerializeField] private List<Col> _cols;

    public void setMatAtPlace(string rendPlace, string matPlace)
    {
        Mat mat = _mats.Find(match => match.place == matPlace);
        if (mat == null)
        {
            mat = notFoundMat;
            Debug.LogWarning(this + " tried setting matPlace '" + matPlace + "' on RendPlace '" + rendPlace + "', but that matPlace wasn't assigned in inspector.");
        }
        List<Rend> rs = getRendsAtPlace(rendPlace);
        rs.ForEach(r => r.setMat(mat));
        bool success = rs.Count > 0;
        if (!success)
        {
            Debug.LogError(this + " tried setting matPlace '" + matPlace + "' on RendPlace '" + rendPlace + "', but that rendPlace wasn't assigned in inspector.");
        }
    }
    public void setColAtPlace(string rendPlace, string colPlace)
    {
        Col col = _cols.Find(match => match.place == colPlace);
        if (col == null)
        {
            Debug.LogWarning(this + " tried setting colPlace '" + colPlace + "' on RendPlace '" + rendPlace + "', but that matPlace wasn't assigned in inspector.");
            setMatAtPlace(rendPlace, null);
        }
        List<Rend> rs = getRendsAtPlace(rendPlace);
        rs.ForEach(r => r.setCol(col));
        bool success = rs.Count > 0;
        if (!success)
        {
            Debug.LogError(this + " tried setting colPlace '" + colPlace + "' on RendPlace '" + rendPlace + "', but that rendPlace wasn't assigned in inspector.");
        }
    }
    public void setFloat(string rendPlace, string name, float f)
    {
        List<Rend> rs = getRendsAtPlace(rendPlace);
        rs.ForEach(r => r.setFloat(name, f));
        bool success = rs.Count > 0;
        if (!success)
        {
            Debug.LogError(this + " tried setting float '" + name + "' = "+f+" on RendPlace '"+rendPlace+"', but that rendPlace wasn't assigned in inspector.");
        }
    }
    private List<Rend> getRendsAtPlace(string place) => _rends.FindAll(match => match.place == place);
    public override string ToString() => name + ".GFX";
}
