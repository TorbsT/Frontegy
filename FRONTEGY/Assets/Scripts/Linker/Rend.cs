using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rend
{
    public string place { get => _place; }

    [SerializeField] private string _place;
    [SerializeField] private List<MeshRenderer> _renderers;

    private Mat _mat;
    private Col _col;
    private Dictionary<string, float> _floats = new Dictionary<string, float>();


    public void setMat(Mat mat)
    {
        _mat = mat;
        refresh();
    }
    public void setCol(Col col)
    {
        _col = col;
        refresh();
    }
    public void setFloat(string name, float f)
    {
        if (name == null || name == "") Debug.LogError("IllegalArgumentException");
        if (_floats.ContainsKey(name)) _floats[name] = f;
        else _floats.Add(name, f);
        refresh();
    }
    

    private void refresh()
    {
        if (hasMat()) _renderers.ForEach(r => r.material = getMat().material);
        //else getRenderer().material = null;
        if (hasCol()) _renderers.ForEach(r => r.material.color = getCol().color);
        foreach (string name in _floats.Keys)
        {
            _renderers.ForEach(r => r.material.SetFloat(name, _floats[name]));
        }
    }
    private bool hasMat() { return _mat != null; }
    private bool hasCol() { return _col != null; }
    private Mat getMat() { return _mat; }
    private Col getCol() { return _col; }
}
