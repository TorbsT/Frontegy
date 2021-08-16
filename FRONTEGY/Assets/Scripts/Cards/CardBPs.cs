using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardBPs
{
    public static CardBPs Instance { get; private set; }
    public List<SummonCardBP> summonBPs { get => _summonBPs; }


    [SerializeField] private List<SummonCardBP> _summonBPs;

    public void init()
    {
        Instance = this;
    }
    public SummonCardBP getSummonBP(int roleId)
    {
        if (_summonBPs == null) Debug.LogError("InspectorException: summonBPs is null");
        if (_summonBPs.Count == 0) Debug.LogError("InspectorException: assign summonBPs");
        SummonCardBP bp = _summonBPs.Find(match => match.roleId == roleId);
        if (bp == null) Debug.LogError("IllegalStateException: No role with id " + roleId);
        return bp;
    }

}
