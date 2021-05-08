using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : ScriptableObject
{
    // PARAMS WILL BE DEFINED IN STATIC OBJECTS  // ...excuse me what does that mean
    // 21/04/21 - "static objects" ...do you have any idea what static means

    [System.NonSerialized] private Card card;

    
    public void cast(Card caster) { if (caster == null) Debug.LogError("IllegalArgumentException"); this.card = caster; use(); }
    public TilePhy getUnstagedTroopPhy() { return getGrid().getUnstagedTilePhy(); }
    public TilePhy getUnstagedCardPhy() { return getGrid().getUnstagedTilePhy(); }
    public Tile getHoveredTile() { return getGrid().getHoveredTile(); }
    public Grid getGrid() { return getCard().getGrid(); }
    public Player getPlayer() { return getCard().getPlayer(); }
    public Card getCard() { if (card == null) Debug.LogError("IllegalStateException"); return card; }

    protected abstract void use();
}