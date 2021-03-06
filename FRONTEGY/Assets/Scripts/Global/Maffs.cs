﻿using System.Collections.Generic;
using UnityEngine;

public static class Maffs
{
    public static float FloatLerp(float a, float b, float c)
    {
        return (1-c)*a + c*b;
    }
    public static bool Divisible(int a, int b)
    {
        return (float)a % (float)b == 0;
    }
    public static int GetSumOfWeights(List<int> weights)
    {
        int sum = 0;
        foreach (int weight in weights)
        {
            sum += weight;
        }
        return sum;
    }
    public static GameObject GetGMGO() { return GameObject.FindGameObjectWithTag("GameMaster"); }
    public static GameMaster GetGM()
    { return GetGMGO().GetComponent<GameMaster>();}
    public static SelectionManager GetSelectionManager()
    { return GetGMGO().GetComponent<SelectionManager>();}
}
