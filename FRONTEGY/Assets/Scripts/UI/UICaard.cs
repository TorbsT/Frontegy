using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class UICaard
{
    public List<Card> cards { get => _cards; }


    private static Rot rot = new Rot(Quaternion.Euler(90f, 180f, 0f));
    private List<Card> _cards = new List<Card>();
    private UIRect _caardBox;

    private Rect _rect;
    private Rect _lastRect;
    public bool showTransNecessary { private get; set; }

    public UICaard()
    {
        _caardBox = UIManager.Instance.getUIRectAtPlace(UIPlace.caardBox);
    }
    public void fixedUpdate()
    {
        _rect = _caardBox.getRect();
        bool rectChanged = _rect != _lastRect;
        showTransNecessary = showTransNecessary || rectChanged;

        if (showTransNecessary) updatePoses();

        _lastRect = _rect;
        showTransNecessary = false;
    }
    public void empty()
    {
        setCards(new List<Card>());
    }
    public void setCards(List<Card> cards)
    {
        foreach (Card c in _cards)
        {
            c.transive.unuize();
            c.transive.pos3p.set(Pos3.identity);
        }
        _cards = cards;
        for (int index = 0; index < _cards.Count; index++)
        {
            Card c = _cards[index];
            c.transive.uize();
            c.transive.setParent(UIManager.Instance.transive);


            

            /*
            ct.pos3p.setParent(trans);
            ct.pos3p.set(p3, true);
            ct.rotp.setParent(trans);
            ct.rotp.set(rot, true);
            */
        }
        showTransNecessary = true;
    }
    private void updatePoses()
    {
        // TODO
        Pos3 boxCenter = _caardBox.center+new Pos3(0f, 0f, 0f);
        for (int i = 0; i < _cards.Count; i++)
        {
            Card c = _cards[i];
            c.transive.scalep.set(Scale.identity, false);

            Bounds b = c.getColliderBounds();
            float cardWidth = 4.5f;// b.size.x;
            float cardHeight = b.size.z;
            //Debug.Log("HEY "+ cardWidth + " " + cardHeight);
            float x = cardWidth * (i-_cards.Count/2f) * 20;
            float y = 0;//cardHeight / 2f;
            float z = i*10;


            //Pos3 p3 = new Pos3(x-(float)_cards.Count/2f, y, 0f);
            //Pos3 p3 = new Pos3(x - (float)_cards.Count / 2f, 0, 0f);
            Pos3 p3 = boxCenter+new Pos3(x, y, z);
            c.transive.pos3p.set(p3, true);
            c.transive.rotp.set(rot, true);
            
        }
    }
}
