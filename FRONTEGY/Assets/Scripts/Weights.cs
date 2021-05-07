using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weights
{  // The naming in here is absolutely horrendous. read at your own risk
    // edit: after almost completing this code, it is truly the ugliest naming in the whole project
    private int totalCount;
    private int weightCount;
    private int[] ids;
    private int[] weights;
    private int[] counts;
    private int[] goalCounts;
    private List<int> output;

    public Weights(int totalCount, List<Vector2Int> inputWeights, Rd rd)
    {
        if (totalCount <= 0) Debug.LogError("IllegalArgumentException");
        if (inputWeights == null) Debug.LogError("IllegalArgumentException");
        if (rd == null) Debug.LogError("IllegalArgumentException");
        weightCount = inputWeights.Count;
        ids = new int[weightCount];
        weights = new int[weightCount];
        counts = new int[weightCount];
        goalCounts = new int[weightCount];
        this.totalCount = totalCount;

        // sets ids and weights
        for (int i = 0; i < weightCount; i++)
        {
            ids[i] = inputWeights[i][0];
            weights[i] = inputWeights[i][1];
        }

        // sets goalCounts
        int totalCountLeft = totalCount;
        int totalWeightLeft = getSum();
        for (int i = 0; i < weightCount; i++)
        {
            int thisWeight = weights[i];
            int countToRemove = thisWeight * totalCountLeft / totalWeightLeft;

            goalCounts[i] = countToRemove;
            totalWeightLeft -= thisWeight;
            totalCountLeft -= countToRemove;
        }
        //DEBUG
        /*
        string d = "";
        for (int i = 0; i < weightCount; i++)
        {
            d += goalCounts[i] + ", ";
        }
        Debug.Log(d);
        */



        // assigns
        totalCountLeft = totalCount;
        //Debug.Log(totalCountLeft + " " + totalCount);
        output = new List<int>();
        for (int i = 0; i < totalCount; i++)
        {
            int selected = rd.rangeInt(0, totalCountLeft);
            //Debug.Log("Selected " + selected + ", max was "+totalCountLeft);
            int selectedIndex = -1;
            for (int j = 0; j < weightCount; j++)
            {
                int c = goalCounts[j] - counts[j];
                //Debug.Log("This w is" + c);
                if (c > selected)
                {
                    selectedIndex = j;
                    break;
                }
                else selected -= c;
            }
            if (selectedIndex == -1) Debug.LogError("UH FUCKING OH");

            counts[selectedIndex]++;
            totalCountLeft--;

            int id = ids[selectedIndex];
            //Debug.Log("Which means " + id);
            output.Add(id);
        }
    }
    public List<int> getOutput()
    {
        return output;
    }
    
    public int getSumCountsLeft()
    {
        int sum = 0;
        for (int i = 0; i < weightCount; i++)
        {
            sum += goalCounts[i] - counts[i];
        }
        return sum;
    }
    /*
    public List<float> getChances()
    {
        List<float> chances = new List<float>();
        float sum = getSum();
        if (noWeights()) Debug.LogError("IllegalStateException: Tried getting relative weights but all were 0");
        foreach (Weight w in weights)
        {
            float rel = (float)w.weight / sum;
            relWeights.Add(rel);
        }
        return relWeights;
    }
    public bool noWeights() { return getSum() == 0; }
    public int getCountLeft(int index) { getWeight(index).weight}
    */
    public int getSum()
    {
        int sum = 0;
        for (int i = 0; i < weightCount; i++)
        {
            sum += weights[i];
        }
        return sum;
    }
    /*
    private Weight getWeight(int index) { return weights[index]; }
    public List<Weight> getWeights() { return weights; }
    */
}
