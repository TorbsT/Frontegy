using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData
{
    public string title;
    public string desc;
    public string type;
    public List<SpellCollection> spellCollections;
    public void Activate(string triggerString, int ownerId)  // ownerId difficult when casting from tactical?
    { 
        foreach (SpellCollection spellCollection in spellCollections)
        {
            if (triggerString == spellCollection.triggerString)
            {
                spellCollection.CastSpells(ownerId);
            }
        }
    }
}