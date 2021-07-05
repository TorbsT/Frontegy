using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardBPs : IVerifiable
{
    [SerializeField] private List<SummonBP> bps = new List<SummonBP>();


    public SummonBP getSummonBP(int id)
    {
        foreach (SummonBP bp in getSummonBPs())
        {
            if (bp.isRoleId(id)) return bp;
        }
        Debug.LogError("IllegalStateException");
        return null;
    }
    public Roole makeRooleFromSummonBPs()
    {
        List<Role> roles = new List<Role>();
        foreach (SummonBP sbp in getSummonBPs())
        {
            roles.Add(sbp.getRole());
        }
        return new Roole(roles);
    }


    public void verify()
    {
        if (bps == null) Debug.LogError("InspectorException: Set cardBPs");
        if (bps.Count == 0) Debug.LogError("InspectorException: empty cardBPs");
        foreach (ICardBP bp in getBPs())
        {
            bp.verify();
        }
    }


    private List<SummonBP> getSummonBPs()
    {
        return getBPs();
    }
    private List<SummonBP> getBPs()
    {
        if (bps == null) Debug.LogError("IllegalStateException");
        return bps;
    }
}
