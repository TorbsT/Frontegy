using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class UICaard
{
    public List<Card> cards { get => _cards; }

    private static Quaternion rotation;
    private List<Card> _cards = new List<Card>();

    private void setUICaardPos()
    {
        // called once
        rotation = Quaternion.identity;
        //rotation *= Quaternion.Euler(90, 0, 0);
        for (int index = 0; index < _cards.Count; index++)
        {
            Card c = _cards[index];


            Bounds b = c.getColliderBounds();
            float cardWidth = b.size.x;
            float cardHeight = b.size.y;
            float x = cardWidth * index;
            float y = cardHeight/2f;


            Pos3 p3 = new Pos3(x, y, 0f);
            Quaternion rot = rotation;
            c.trans.setParent(UIManager.Instance.getTransAtPlace(UIPlace.caardBox), true);
            c.trans.pos3p.set(p3, true);
            Debug.Log(c.trans.pos3p.get() + " " + c.trans.pos3p.get(false));
            Debug.Log(c.trans.transform.position);
        }
    }
    public void unuizeAll()
    {
        foreach (Card c in _cards)
        {
            // suppose it's uized
            c.unuize();
        }
    }
    public void setCards(List<Card> cards)
    {
        _cards = cards;
        setUICaardPos();
    }
}
