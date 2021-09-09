using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public class UICaard
{
    public List<Card> cards { get => _cards; }
    public static bool usingMixedCam = true;
    public static float distanceBetween = 0.1f;  // Relative

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
            if (usingMixedCam) c.transive.unuize();
            c.transive.pos3p.set(Pos3.identity);
        }
        _cards = cards;
        Transive parent;
        if (usingMixedCam) parent = Cam.Instance.mixedUITransive;
        else parent = UIManager.Instance.transive;
        for (int index = 0; index < _cards.Count; index++)
        {
            Card c = _cards[index];
            if (!usingMixedCam) c.transive.uize();
            
            c.transive.setParent(parent);


            

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
        Debug.Log("KA"+boxCenter);
        for (int i = 0; i < _cards.Count; i++)
        {
            Card c = _cards[i];
            if (c.used) continue;
            c.transive.scalep.set(Scale.identity, false);

            Bounds b = c.getColliderBounds();
            float cardWidth = 5f;// b.size.x;
            float cardHeight = b.size.z;
            //Debug.Log("HEY "+ cardWidth + " " + cardHeight);


            float x = cardWidth * (i-_cards.Count/2f)*20*(1+distanceBetween);
            float y = 0;//cardHeight / 2f;
            float z = i*10;
            Pos3 local = new Pos3(x, y, z);

            Pos3 p3 = boxCenter+local;
            if (usingMixedCam) p3 /= 100;
            c.transive.pos3p.set(p3, true);
            c.transive.rotp.set(rot, true);
            
        }
    }
}
