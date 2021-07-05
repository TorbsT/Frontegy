using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell
{
    // PARAMS WILL BE DEFINED IN STATIC OBJECTS  // ...excuse me what does that mean
    // 21/04/21 - "static objects" ...do you have any idea what static means
    // 21/05/21 - ey it's me again exactly one month later what a cockincidence. anyways i removed scriptableobject inheritence since i wanna try doing this in code

    [System.NonSerialized] private Card card;

    
    public void cast(Card caster, Tile tile) { if (caster == null) Debug.LogError("IllegalArgumentException"); this.card = caster; use(tile); }
    public Grid getGrid() { return getCard().getGrid(); }
    public Player getOwner() { return getCard().owner; }
    public Card getCard() { if (card == null) Debug.LogError("IllegalStateException"); return card; }

    protected abstract void use(Tile tile);
}