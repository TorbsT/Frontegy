using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpellCollection
{
    public string triggerString;
    public List<Spell> spells;

    public void CastSpells(int ownerId)
    { 
        foreach (Spell spell in spells)
        {
            spell.Cast(ownerId);
        }
    }
}
public class Spell : ScriptableObject
{
    // PARAMS WILL BE DEFINED IN STATIC OBJECTS
    public string targetTag;

    public virtual void Cast(int ownerId)
    { 
        
    }
    public void LogTargetTag()
    {
        Debug.Log(targetTag);
    }
    public int GetTargetId()
    {
        if (targetTag == "") return 1;
        return 0;
    }
}