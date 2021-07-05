using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rend
{
    [SerializeField] private RendPlace place;
    [SerializeField] private MeshRenderer renderer;

    [Header("Reading only")]
    [SerializeReference] private Mat mat;
    [SerializeReference] private Col col;
    [SerializeReference] private Dictionary<string, float> floats = new Dictionary<string, float>();


    public void setMat(Mat mat)
    {
        this.mat = mat;
        refresh();
    }
    public void setCol(Col col)
    {
        this.col = col;
        refresh();
    }
    public void setFloat(string name, float f)
    {
        if (name == null || name == "") Debug.LogError("IllegalArgumentException");
        if (floats.ContainsKey(name)) floats[name] = f;
        else floats.Add(name, f);
        refresh();
    }

    public bool isPlace(RendPlace place) { return this.place == place; }
    

    private void refresh()
    {
        if (hasMat()) getRenderer().material = getMat().getMaterial();
        //else getRenderer().material = null;
        if (hasCol()) getRenderer().material.color = getCol().getColor();
        foreach (string name in floats.Keys)
        {
            getRenderer().material.SetFloat(name, floats[name]);
        }
    }
    private bool hasMat() { return mat != null; }
    private bool hasCol() { return col != null; }
    private Mat getMat() { return mat; }
    private Col getCol() { return col; }
    private MeshRenderer getRenderer() { if (renderer == null) Debug.LogError("InspectorException: " + this + " has no renderer"); return renderer; }
}
