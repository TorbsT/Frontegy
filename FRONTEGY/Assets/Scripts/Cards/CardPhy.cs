using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardPhy : SelPhy
{
    [Header("Variables")]
    [SerializeReference] private Card cardChy;
    [SerializeField] private Rigidbody _rb;
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
        UIManager.Instance.unuize(transive);
        CardPool.Instance.unstage(this);
    }
    public override bool tryRagdollMode()
    {
        _rb.isKinematic = false;
        return true;
    }
    public override bool tryUnragdollMode()
    {
        _rb.isKinematic = true;
        transive.transformExternallyChanged();
        return true;
    }
}