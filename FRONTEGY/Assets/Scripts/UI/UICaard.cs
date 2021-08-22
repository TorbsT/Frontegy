using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class UICaard
{
    public List<Card> cards { get => _cards; }


    private static Rot rot = new Rot(Quaternion.Euler(90f, 180f, 0f));
    private List<Card> _cards = new List<Card>();

    public void empty()
    {
        setCards(new List<Card>());
    }
    public void setCards(List<Card> cards)
    {
        foreach (Card c in _cards)
        {
            c.transive.setParent(null, false);
            c.transive.pos3p.set(Pos3.identity);
        }
        _cards = cards;
        for (int index = 0; index < _cards.Count; index++)
        {
            Card c = _cards[index];


            Bounds b = c.getColliderBounds();
            float cardWidth = b.size.x;
            float cardHeight = b.size.y;
            float x = cardWidth * index;
            float y = cardHeight / 2f;


            //Pos3 p3 = new Pos3(x-(float)_cards.Count/2f, y, 0f);
            Pos3 p3 = new Pos3(x - (float)_cards.Count / 2f, 0, 0f);
            Trans trans = UIManager.Instance.getTransAtPlace(UIPlace.caardBox);
            Debug.Log(trans);
            c.transive.setParent(trans);
            c.transive.pos3p.set(p3, true);
            //if (index < rots.Length) c.trans.rotp.set(rots[index], true);
            c.transive.rotp.set(rot, true);
            c.transive.scalep.set(Scale.identity, false);
        }
    }
}
