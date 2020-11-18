using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SpellCollection.asset", menuName = "SpellCollection")]
public class SpellCollection : ScriptableObject
{
    public string triggerTag;
    public List<Spell> spells;

    public void CastSpells(Card parent)
    {
        foreach (Spell spell in spells)
        {
            spell.Cast(parent);
        }
    }
}
