using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SummonSpell.asset", menuName = "SummonSpell")]
public class SummonSpell : Spell
{
    [SerializeField] int unitId;  // Unit to spawn
    public override void Cast(Card parent)
    {
        base.Cast(parent);
        /*
        Unit newUnit = new Unit(new UnitStats());

        newUnit.Instantiate();
        */
        int playerId = GameMaster.GetGM().getCurrentPlayerId();
        List<Unit> units = new List<Unit>() { new Unit(unitId) };
        Troop newTroop = new Troop(true, playerId, units);
        GameMaster.getAllGroop().add(newTroop);
        newTroop.parentTile = selHover.SelGetTile();
        // MAYBUG newTroop.parentTIle.instantiate()
    }
}
