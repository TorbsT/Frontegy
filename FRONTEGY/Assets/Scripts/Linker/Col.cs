using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Col
{  // Wrapper for Color that makes it nullable
    [SerializeField] private MatPlace place;
    [SerializeField] private Color color;

    public Color getColor()
    {
        return color;
    }
    public bool isPlace(MatPlace place)
    {
        return place == this.place;
    }
    public MatPlace getPlace()
    {
        return place;
    }
}
