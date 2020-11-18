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
        Troop newTroop = new Troop(new TroopStats(Maffs.GetGM().phase.playerId, new List<Unit>() { new Unit(unitId) }));
        gameMaster.grid.data.troops.Add(newTroop);
        newTroop.stats.parentTileId = selHover.SelGetTile().geo.id;
        newTroop.Instantiate();
    }
}
