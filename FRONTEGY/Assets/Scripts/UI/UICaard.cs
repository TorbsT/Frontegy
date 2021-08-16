using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class UICaard : Caard
{
    private UIManager manager;
    private Quaternion rotation;

    public UICaard() : base()
    {
        manager = UIManager.Instance;
    }

    private void setUICaardPos()
    {
        // called once
        rotation = Quaternion.identity;
        //rotation *= Quaternion.Euler(90, 0, 0);
        for (int index = 0; index < getCount(); index++)
        {
            Card c = getCard(index);


            Bounds b = c.getColliderBounds();
            float cardWidth = b.size.x;
            float cardHeight = b.size.y;
            float x = cardWidth * index;
            float y = cardHeight/2f;


            Pos3 p3 = new Pos3(x, y, 0f);
            Quaternion rot = rotation;
            c.trans.setParent(manager.getTransAtPlace(UIPlace.caardBox), true);
            c.trans.pos3p.set(p3, true);
        }
    }
    public void unuizeAll()
    {
        foreach (Card c in getCards())
        {
            // suppose it's uized
            c.unuize();
        }
    }
    public void setCaard(Caard caard)
    {
        if (caard == null) Debug.LogError("IllegalArgumentException");
        List<Card> cards = caard.getCards();
        int count = caard.getCount();
        makeEmpty();
        
        for (int i = count-1; i >= 0; i--)
        {
            Card c = cards[i];
            add(c);

        }
        setUICaardPos();

        /*
        foreach (Card c in caard.getCards())
        {
            add(c);
            c.display(UIPlace.caardBox);
        }
        */
    }
}
