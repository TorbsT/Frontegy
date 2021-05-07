using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight
{
    public int id;
    public int weight;
    private int count;
    public Weight(int id, int weight)
    {
        if (weight < 0) Debug.LogError("IllegalArgumentException: Weight cannot be negative, only 0 and up");
        this.id = id;
        this.weight = weight;
        count = 0;
    }
    public bool isNatural() { return weight > 0; }
    public void increment() { count++; }
    public int getCount() { return count; }
}
