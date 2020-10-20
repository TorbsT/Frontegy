using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SummonSpell.asset", menuName = "SummonSpell")]
public class SummonSpell : Spell
{
    [SerializeField] int unitId;
    public override void Cast(int ownerId)
    {
        base.Cast(ownerId);
        // Find selectionmanager and shit
        Debug.Log(unitId);
    }
}
