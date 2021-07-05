using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SummonBP : ICardBP
{
    // maybe add field for level to unlock
    //[SerializeField] private MatPlace ;  // think this through
    [SerializeField] private SummonSpell spell;
    public SummonSpell Spell { get { return spell; } }



    public bool isRoleId(int id) { return spell.isRoleId(id); }


    public void verify()
    {
        if (spell == null) Debug.LogError("InspectorException: SummonBP '" + this + "' misses spell");
    }

    public Role getRole()
    {
        Role role = spell.Role;
        return role;
    }
    public void cast(Card card, Tile tile, CastType type)
    {
        if (card == null) Debug.LogError("IllegalArgumentException");
        spell.cast(card, tile);
    }
    public override string ToString() { return getRole().ToString(); }

}
