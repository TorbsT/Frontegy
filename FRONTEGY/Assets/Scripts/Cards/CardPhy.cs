using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class CardPhy : Selectable
{
    [Header("Variables")]
    private HandCard cardChy;
    public float size = 0.05f;

    [Header("System")]
    Vector3 initialScale;
    readonly string canvasName = "Canvas";
    readonly string frontName = "Front";
    readonly string backName = "Back";
    readonly string titleName = "Title";
    readonly string descName = "Desc";
    readonly string typeName = "Type";
    Transform canvas;
    [SerializeField] Transform cardFront;
    [SerializeField] Transform cardBack;
    [SerializeField] TextMeshProUGUI titleMesh;
    [SerializeField] TextMeshProUGUI descMesh;
    [SerializeField] TextMeshProUGUI typeMesh;

  

    Renderer frontRend;
    Renderer backRend;
    Material initialFrontMat;
    Material initialBackMat;

    public CardPhy(Roster roster) : base(roster)
    {
        cardFront = getGO().transform.Find(frontName);
        frontRend = cardFront.GetComponent<Renderer>();
        initialFrontMat = frontRend.material;
        cardBack = getGO().transform.Find(backName);
        backRend = cardBack.GetComponent<Renderer>();
        initialBackMat = backRend.material;
        canvas = cardFront.Find(canvasName);
        titleMesh = canvas.Find(titleName).GetComponent<TextMeshProUGUI>();
        descMesh = canvas.Find(descName).GetComponent<TextMeshProUGUI>();
        typeMesh = canvas.Find(typeName).GetComponent<TextMeshProUGUI>();
        initialScale = getGO().transform.localScale;
    }
    public void ManualUpdate()
    {
        
    }
    public void Display()
    {
        titleMesh.text = cardChy.title;
        descMesh.text = cardChy.desc;
        typeMesh.text = cardChy.type;
    }

    public void UpdateGOScale()
    {
        SetGOScale(size);
    }
    public void SetGOScale(float s)
    {
        getGO().transform.localScale = initialScale * s;
    }
    public void SetGORot(Quaternion rot)
    {
        getGO().transform.rotation = rot;
    }
    void BucketMat(Material m)
    {
        SetMat(frontRend, m);
        SetMat(backRend, m);
    }
    void ResetMats()
    {
        SetMat(frontRend, initialFrontMat);
        SetMat(backRend, initialBackMat);
    }
    void SetMat(Renderer r, Material m) { r.material = m; }

    public override Card SelGetCard() { return this.getCard(); }
    public Card getCard() { return cardChy; }
    public override void SelHover()
    {
        if (isSelected()) return;
        BucketMat(getGM().globalHoverMat);
    }
    public override void SelUnHover()
    {
        if (isSelected()) return;
        ResetMats();
    }
    public override void SelSelect()
    {
        // MAYBUG selected = true;
        BucketMat(getGM().globalSelectMat);

    }
    public override void SelUnSelect()
    {
        // MAYBUG isSelected = false;
        ResetMats();
    }

    protected override void setChy(Chy chy)
    {
        cardChy = (HandCard)chy;
    }

    protected override Chy getChy()
    {
        return cardChy;
    }
}