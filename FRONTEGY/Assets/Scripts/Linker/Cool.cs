using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cool
{
    [SerializeField] private List<Col> cols;

    public Col getCol(MatPlace place)
    {
        if (place == MatPlace.none) return null;
        foreach (Col col in getCols())
        {
            if (col.isPlace(place)) return col;
        }
        Debug.Log("InspectorException: ColPlace " + place + " was not found");
        return null;
    }
    private List<Col> getCols()
    {
        if (cols == null) Debug.LogError("IllegalStateException");
        return cols;
    }
}
