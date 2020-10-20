using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardEL : Selectable
{
    [Header("Variables")]
    [SerializeField] CardData cardData;

    [Header("System")]
    [SerializeField] Material hoverMat;
    [SerializeField] Material selectMat;
    [SerializeField] GameObject cardFront;
    [SerializeField] GameObject cardBack;
    [SerializeField] TextMeshProUGUI titleMesh;
    [SerializeField] TextMeshProUGUI descMesh;
    [SerializeField] TextMeshProUGUI typeMesh;
   
    Renderer frontRend;
    Renderer backRend;
    Material initialFrontMat;
    Material initialBackMat;
    bool isInitialized;
    bool isSelected;

    void ManualStart()
    {
        isInitialized = true;
        titleMesh.text = cardData.title;
        descMesh.text = cardData.desc;
        typeMesh.text = cardData.type;

        frontRend = cardFront.GetComponent<Renderer>();
        backRend = cardBack.GetComponent<Renderer>();
        initialFrontMat = frontRend.material;
        initialBackMat = backRend.material;
    }
    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
        transform.Rotate(0f, 0.05f, 0.2f, Space.Self);
    }
    void Update() { ManualUpdate(); }


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



    public override CardEL SelGetCard() { return this; }
    public override void SelHover()
    {
        if (isSelected) return;
        BucketMat(hoverMat);
    }
    public override void SelUnHover()
    {
        if (isSelected) return;
        ResetMats();
    }
    public override void SelSelect()
    {
        isSelected = true;
        BucketMat(selectMat);

    }
    public override void SelUnSelect()
    {
        isSelected = false;
        ResetMats();
    }
}
