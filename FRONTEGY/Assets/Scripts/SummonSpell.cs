using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[CreateAssetMenu(fileName = "SummonSpell.asset", menuName = "SummonSpell")]
[System.Serializable]
public class SummonSpell : Spell
{
    //[SerializeField] int unitId;  // Unit to spawn
    [SerializeField] private Role role; // unit spawned uses this role
    public Role Role { get { if (role == null) Debug.LogError("IllegalStateException"); return role; } }
    public SummonSpell(Role role)
    {
        if (role == null) Debug.LogError("IllegalArgumentException");
        this.role = role;
    }
    protected override void use(Tile tile)
    {
        Unit unit = new Unit(role);
        Troop newTroop = new Troop(getGrid(), true, getOwner(), unit, tile);
        GameMaster.getAllGroop().add(newTroop);
        // MAYBUG newTroop.parentTIle.instantiate()
    }
    public bool isRoleId(int id) { return role.isId(id); }
}
