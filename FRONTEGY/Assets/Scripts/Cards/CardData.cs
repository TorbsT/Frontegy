using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CardData
{
    public string title;
    public string desc;
    public string type;
    public List<SpellCollection> spellCollections;
}
