using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllCaard : Caard
{
    public static AllCaard Instance;

    /*
    public static AllCaard genStartCaard(int count, Rds seed)
    {

    }
    */
    public AllCaard()
    {
        Instance = this;
    }
}
