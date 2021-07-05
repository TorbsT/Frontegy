using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardPhy : SelPhy
{
    [Header("Variables")]
    [SerializeReference] private Card cardChy;
    public float size = 0.05f;

    [Header("System")]
    Vector3 initialScale;

    Renderer frontRend;
    Renderer backRend;
    Material initialFrontMat;
    Material initialBackMat;

    public CardPhy()
    {
    }

    public Card getCard() { return CardPool.Instance.getClient(this); }
    public override SelChy getSelChy()
    {
        return getCard();
    }
    public override void unstage()
    {
        CardPool.Instance.unstage(this);
    }
}