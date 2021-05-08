using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SummonSpell.asset", menuName = "SummonSpell")]
public class SummonSpell : Spell
{
    //[SerializeField] int unitId;  // Unit to spawn
    private Role role;  // unit spawned uses this role
    protected override void use()
    {
        Unit unit = new Unit(role);
        Troop newTroop = new Troop(getGrid(), true, getPlayer(), unit);
        GameMaster.getAllGroop().add(newTroop);
        newTroop.placeDownOn(getHoveredTile());
        // MAYBUG newTroop.parentTIle.instantiate()
    }
}
