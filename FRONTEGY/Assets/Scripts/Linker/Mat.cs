using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mat
{
    [SerializeField] private MatPlace place;
    [SerializeField] private Material material;

    public bool isPlace(MatPlace place) { return this.place == place; }
    public MatPlace getPlace()
    {
        return place;
    }
    public Material getMaterial()
    {
        //if (material == null) Debug.LogError("IllegalStateException: Mat at MatPlace.'"+place+"' has no material");
        return material;
    }
}
