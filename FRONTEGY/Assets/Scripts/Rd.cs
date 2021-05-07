using System.Collections;
using System.Collections.Generic;
using System;

public class Rd
{
    [UnityEngine.SerializeField] private int seed;
    private Random rd;

    public Rd(int seed)
    {
        this.seed = seed;
        rd = new Random(seed);
    }
    /*
    public List<int> manyIntsByWeights(int count, Weights weights)
    {
        int sumWeights = weights.getSum();


        List<int> counts = new List<int>();

    }*/

    public int rangeInt(int min, int max)
    {
        return (int)Math.Floor(rangeDouble(min, max));
    }
    public double rangeDouble(float min, float max)
    {
        double x = nextDouble();
        x *= (max - min);
        x += min;
        return x;
    }
    public double nextDouble() { return rd.NextDouble(); }
    public int next() { return rd.Next(); }
}
