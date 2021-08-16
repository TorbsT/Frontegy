using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[System.Serializable]
//[CreateAssetMenu(fileName = "SpellCollection.asset", menuName = "SpellCollection")]
public class SpellCollection
{
    // 21/05/21 - removed scriptableobjects for the same reason as in Spell

    public string triggerTag;
    public List<Spell> spells;
    public SpellCollection(List<Spell> spells, string triggerTag)
    {
        if (spells == null) Debug.LogError("IllegalArgumentException");
        if (triggerTag == null) Debug.LogError("IllegalArgumentException");
        this.spells = spells;
        this.triggerTag = triggerTag;
    }
    public void CastSpells(Card parent, Tile tile)
    {
        foreach (Spell spell in spells)
        {
            spell.cast(parent, tile);
        }
    }
}
*/