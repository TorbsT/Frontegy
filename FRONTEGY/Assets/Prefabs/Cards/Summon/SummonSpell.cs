using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SummonSpell.asset", menuName = "SummonSpell")]
public class SummonSpell : Spell
{
    //[SerializeField] int unitId;  // Unit to spawn
    private Role role;  // unit spawned uses this role
    public override void Cast(Card parent)
    {
        base.Cast(parent);
        /*
        Unit newUnit = new Unit(new UnitStats());

        newUnit.Instantiate();
        */
        Player castingPlayer = GameMaster.GetGM().getCurrentPlayer();
        Unit unit = new Unit(role);
        Troop newTroop = new Troop(true, castingPlayer, unit);
        GameMaster.getAllGroop().add(newTroop);
        newTroop.placeDownOn(selHover.SelGetTile());
        // MAYBUG newTroop.parentTIle.instantiate()
    }
}
