using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Caard
{
    [SerializeReference] private List<Card> cards;

    public Caard()
    {
        makeEmpty();
    }
    public Caard(List<Card> cards)
    {
        setCards(cards);
    }
    public void makeEmpty()
    {
        this.cards = new List<Card>();
    }
    public void add(Card c)
    {
        getCards().Add(c);
    }
    public bool outOfRange(int index)
    {
        return index < 0 || index >= getCount();
    }
    public Card getCard(int index)
    {
        if (outOfRange(index)) Debug.LogError("IllegalArgument");
        return getCards()[index];
    }
    public int getCount()
    {
        return getCards().Count;
    }
    public void stage()
    {
        foreach (Card c in getCards())
        {
            if (c.staged) continue;
            c.stage();
        }
    }
    public Caard getCaardOwnedBy(Player player)
    {
        Caard caard = new Caard();
        foreach (Card c in getCards())
        {
            if (player.isSamePlayer(c.owner)) caard.add(c);
        }
        return caard;
    }
    public void setCards(List<Card> cards)
    {
        if (cards == null) Debug.LogError("IllegalArgumentException");
        this.cards = cards;
    }
    public List<Card> getCards()
    {
        if (cards == null) Debug.LogError("Should never happen");
        return cards;
    }
    public List<CardPhy> getPhys()
    {
        List<CardPhy> phys = new List<CardPhy>();
        foreach (Card c in getCards())
        {
            CardPhy phy = c.getCardPhy();
            if (phy == null) Debug.LogError("Uh oh, this is probably bad");
            phys.Add(phy);
        }
        return phys;
    }
}
