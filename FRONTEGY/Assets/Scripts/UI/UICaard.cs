using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class UICaard
{
    public List<Card> cards { get => _cards; }


    private static Rot rot = new Rot(Quaternion.Euler(90f, 180f, 0f));
    private static Rot[] rots = new Rot[] {
        new Rot(Quaternion.Euler(-90f, -90f, -90f)),
        new Rot(Quaternion.Euler(-90f, -90f, 0f)),
        new Rot(Quaternion.Euler(-90f, -90f, 90f)),
        new Rot(Quaternion.Euler(-90f, -90f, 180f)),
                new Rot(Quaternion.Euler(-90f, 0f, -90f)),
        new Rot(Quaternion.Euler(-90f, 0f, 0f)),
        new Rot(Quaternion.Euler(-90f, 0f, 90f)),
        new Rot(Quaternion.Euler(-90f, 0f, 180f)),
                new Rot(Quaternion.Euler(-90f, 90f, -90f)),
        new Rot(Quaternion.Euler(-90f, 90f, 0f)),
        new Rot(Quaternion.Euler(-90f, 90f, 90f)),
        new Rot(Quaternion.Euler(-90f, 90f, 180f)),
                new Rot(Quaternion.Euler(-90f, 180f, -90f)),
        new Rot(Quaternion.Euler(-90f, 180f, 0f)),
        new Rot(Quaternion.Euler(-90f, 180f, 90f)),
        new Rot(Quaternion.Euler(-90f, 180f, 180f)),

                new Rot(Quaternion.Euler(0f, -90f, -90f)),
        new Rot(Quaternion.Euler(0f, -90f, 0f)),
        new Rot(Quaternion.Euler(0f, -90f, 90f)),
        new Rot(Quaternion.Euler(0f, -90f, 180f)),
                new Rot(Quaternion.Euler(0f, 0f, -90f)),
        new Rot(Quaternion.Euler(0f, 0f, 0f)),
        new Rot(Quaternion.Euler(0f, 0f, 90f)),
        new Rot(Quaternion.Euler(0f, 0f, 180f)),
                new Rot(Quaternion.Euler(0f, 90f, -90f)),
        new Rot(Quaternion.Euler(0f, 90f, 0f)),
        new Rot(Quaternion.Euler(0f, 90f, 90f)),
        new Rot(Quaternion.Euler(0f, 90f, 180f)),
                new Rot(Quaternion.Euler(0f, 180f, -90f)),
        new Rot(Quaternion.Euler(0f, 180f, 0f)),
        new Rot(Quaternion.Euler(0f, 180f, 90f)),
        new Rot(Quaternion.Euler(0f, 180f, 180f)),

                new Rot(Quaternion.Euler(90f, -90f, -90f)),
        new Rot(Quaternion.Euler(90f, -90f, 0f)),
        new Rot(Quaternion.Euler(90f, -90f, 90f)),
        new Rot(Quaternion.Euler(90f, -90f, 180f)),
                new Rot(Quaternion.Euler(90f, 0f, -90f)),
        new Rot(Quaternion.Euler(90f, 0f, 0f)),
        new Rot(Quaternion.Euler(90f, 0f, 90f)),
        new Rot(Quaternion.Euler(90f, 0f, 180f)),
                new Rot(Quaternion.Euler(90f, 90f, -90f)),
        new Rot(Quaternion.Euler(90f, 90f, 0f)),
        new Rot(Quaternion.Euler(90f, 90f, 90f)),
        new Rot(Quaternion.Euler(90f, 90f, 180f)),
                new Rot(Quaternion.Euler(90f, 180f, -90f)),
        new Rot(Quaternion.Euler(90f, 180f, 0f)),
        new Rot(Quaternion.Euler(90f, 180f, 90f)),
        new Rot(Quaternion.Euler(90f, 180f, 180f)),

                new Rot(Quaternion.Euler(180f, -90f, -90f)),
        new Rot(Quaternion.Euler(180f, -90f, 0f)),
        new Rot(Quaternion.Euler(180f, -90f, 90f)),
        new Rot(Quaternion.Euler(180f, -90f, 180f)),
                new Rot(Quaternion.Euler(180f, 0f, -90f)),
        new Rot(Quaternion.Euler(180f, 0f, 0f)),
        new Rot(Quaternion.Euler(180f, 0f, 90f)),
        new Rot(Quaternion.Euler(180f, 0f, 180f)),
                new Rot(Quaternion.Euler(180f, 90f, -90f)),
        new Rot(Quaternion.Euler(180f, 90f, 0f)),
        new Rot(Quaternion.Euler(180f, 90f, 90f)),
        new Rot(Quaternion.Euler(180f, 90f, 180f)),
                new Rot(Quaternion.Euler(180f, 180f, -90f)),
        new Rot(Quaternion.Euler(180f, 180f, 0f)),
        new Rot(Quaternion.Euler(180f, 180f, 90f)),
        new Rot(Quaternion.Euler(180f, 180f, 180f)),
    };
    private List<Card> _cards = new List<Card>();

    private void setUICaardPos()
    {
        // called once
        //rotation *= Quaternion.Euler(90, 0, 0);
        for (int index = 0; index < _cards.Count; index++)
        {
            Card c = _cards[index];


            Bounds b = c.getColliderBounds();
            float cardWidth = b.size.x;
            float cardHeight = b.size.y;
            float x = cardWidth * index;
            float y = cardHeight/2f;


            Pos3 p3 = new Pos3((x-(float)_cards.Count/2f)*10, y*10, 0f);
            Trans trans = UIManager.Instance.getTransAtPlace(UIPlace.caardBox);
            Debug.Log(trans);
            c.trans.setParent(trans, true);
            c.trans.pos3p.set(p3, true);
            //if (index < rots.Length) c.trans.rotp.set(rots[index], true);
            c.trans.rotp.set(rot, true);
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
