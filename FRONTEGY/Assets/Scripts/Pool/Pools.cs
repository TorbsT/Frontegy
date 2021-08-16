using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pools
{
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
            new PafPool(pafPrefab)
        };
    }
    public void unstageAll()
    {
        foreach (IPool pool in pools)
        {
            pool.unstageAll();
        }
    }
}
