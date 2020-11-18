using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Card : Selectable
{
    [Header("Variables")]
    public CardData data;
    public float size = 0.05f;
    public Player playerHolder = null;
    public Unit unitHolder = null;

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
    bool isSelected;

    public override void Instantiate()
    {
        Debug.Log("Trying");
        Instantiate2(this.GetType());
        cardFront = selGO.transform.Find(frontName);
        frontRend = cardFront.GetComponent<Renderer>();
        initialFrontMat = frontRend.material;
        cardBack = selGO.transform.Find(backName);
        backRend = cardBack.GetComponent<Renderer>();
        initialBackMat = backRend.material;
        canvas = cardFront.Find(canvasName);
        titleMesh = canvas.Find(titleName).GetComponent<TextMeshProUGUI>();
        descMesh = canvas.Find(descName).GetComponent<TextMeshProUGUI>();
        typeMesh = canvas.Find(typeName).GetComponent<TextMeshProUGUI>();
        initialScale = selGO.transform.localScale;
    }
    public void ManualUpdate()
    {
        
    }
    public void Display()
    {
        titleMesh.text = data.title;
        descMesh.text = data.desc;
        typeMesh.text = data.type;
    }

    public void SetPlayerHolder(Player player)
    {
        unitHolder = null;
        playerHolder = player;
    }
    public void SetUnitHolder(Unit unit)
    {
        playerHolder = null;
        unitHolder = unit;
    }

    public void SetGOPos(Vector3 pos)
    {
        selGO.transform.position = pos;
    }
    public void UpdateGOScale()
    {
        SetGOScale(size);
    }
    public void SetGOScale(float s)
    {
        selGO.transform.localScale = initialScale * s;
    }
    public void SetGORot(Quaternion rot)
    {
        selGO.transform.rotation = rot;
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

    public override Card SelGetCard() { return this; }
    public override void SelHover()
    {
        if (isSelected) return;
        BucketMat(gameMaster.globalHoverMat);
    }
    public override void SelUnHover()
    {
        if (isSelected) return;
        ResetMats();
    }
    public override void SelSelect()
    {
        isSelected = true;
        BucketMat(gameMaster.globalSelectMat);

    }
    public override void SelUnSelect()
    {
        isSelected = false;
        ResetMats();
    }

    public void Activate(string triggerTag)  // ownerId difficult when casting from tactical?
    {
        Debug.Log("aaaaaaaaaaaaaaaa");
        foreach (SpellCollection spellCollection in data.spellCollections)
        {
            Debug.Log("bbbbbbbbbbbb");
            if (triggerTag == spellCollection.triggerTag)
            {
                Debug.Log("cccccccccccccccc");
                spellCollection.CastSpells(this);
            }
        }
    }
}