using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pools
{
    public static bool ragdollWhenUnstagingAll = true;

    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private GameObject troopPrefab;
    [SerializeField] private GameObject pafPrefab;

    private List<IPool> pools;

    public void init()
    {
        pools = new List<IPool>
        {
            new TilePool(tilePrefab),
            new CardPool(cardPrefab),
            new TroopPool(troopPrefab),
            new PafStepPool(pafPrefab)
        };
    }
    public void unstageAll()
    {
        foreach (IPool pool in pools)
        {
            if (ragdollWhenUnstagingAll)  // fix this you pineapple. Is there a way around?
            {
                pool.ragdollifyAll();
            }
            pool.unstageAll();
        }
    }
}
