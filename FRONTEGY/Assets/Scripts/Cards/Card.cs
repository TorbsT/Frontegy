using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card : Chy  // changed to class since i didn't know why it should be struct
{  // TODO Currently only supports handCard, not upgrades

    public string title;
    public string desc;
    public string type;
    public List<SpellCollection> spellCollections;



    private CardPhy phy;
    private Player holder;

    public void setHolder(Player player)
    {
        holder = player;
    }

    public void Activate(string triggerTag)  // ownerId difficult when casting from tactical?
    {
        foreach (SpellCollection spellCollection in spellCollections)
        {
            if (triggerTag == spellCollection.triggerTag)
            {
                spellCollection.CastSpells(this);
            }
        }
    }
    public Player getHolder() { if (holder == null) Debug.LogError("Should probably not happen"); return holder; }  // may be null 
    public CardPhy getCardPhy()
    {
        return phy;
    }

    protected override Phy getPhy()
    {
        return getCardPhy();
    }

    protected override void connect()
    {
        phy = CardRoster.sgetUnstagedPhy();
    }

    protected override void disconnect()
    {
        phy = null;
    }
}
