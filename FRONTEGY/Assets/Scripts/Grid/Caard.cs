using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caard
{
    private List<Card> cards;

    public Caard() { }
    public Caard(List<Card> cards)
    {
        this.cards = cards;
    }

    public void add(Card c)
    {
        getCards().Add(c);
    }

    public Caard getCaardInHandOf(Player player)
    {
        Caard caard = new Caard();
        foreach (Card c in getCards())
        {
            if (player.isSamePlayer(c.getHolder())) caard.add(c);
        }
        return caard;
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
