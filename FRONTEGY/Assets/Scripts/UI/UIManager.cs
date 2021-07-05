using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class UIManager
{

    [System.NonSerialized] private GameMaster gm;

    [System.NonSerialized] private UILinker linker;
    [SerializeReference] private UICaard uiCaard;
    [SerializeField] private float canvasDistance;

    private Grid Grid
    {
        get
        {
            Grid g = gm.grid;
            if (g == null) Debug.LogError("IllegalStateException");
            return g;
        }
    }
    private Cam Cam
    {
        get
        {
            Cam c = Grid.getCam();
            if (c == null) Debug.LogError("IllegalStateException");
            return c;
        }
    }



    public UIManager(float canvasDistance)
    {
        gm = GameMaster.GetGM();
        this.canvasDistance = canvasDistance;

        GameObject go = Object.Instantiate(getPrefab());
        if (go == null) Debug.LogError("WhatException: WHat the actual fuck");
        linker = go.GetComponent<UILinker>();
        if (linker == null) Debug.LogError("InspectorException: UIPrefab does not have UIController Component");
        linker.setUiManager(this);
    }

    public void restart()
    {
        uiCaard = new UICaard(this);
    }

    private GFX getGFX()
    {
        return getLinker().getGFX();
    }
    public Transform getUIPlace(UIPlace place)
    {
        if (place == UIPlace.caardBox) return getCaardBox();
        Debug.LogError("IllegalStateException: UIPlace not associated with any Transform");
        return null;
    }
    private RectTransform getCaardBox()
    {
        return getLinker().getCaardBox();
    }
    private Transform getTopTransform()
    {  // The child of top transform. Doesn't have normal transform
        return getLinker().getTop();
    }
    public RectTransform getCanvasTransform()
    {
        return getLinker().getCanvasTransform();
    }
    public Canvas getCanvas()
    {
        return getLinker().getCanvas();
    }
    private UILinker getLinker()
    {
        if (linker == null) Debug.LogError("IllegalStateException");
        return linker;
    }
    private GameObject getPrefab()
    {
        GameObject go = gm.getUIPrefab();
        if (go == null) Debug.LogError("InspectorException: Remember setting UIPrefab in GameMaster");
        return go;
    }
    private void unuizeAll()
    {
        if (uiCaard != null)
        {
            uiCaard.unuizeAll();
        }
    }
    public void battleStart()
    {
        unuizeAll();
    }
    public void tacticalStart(TacticalPhase tp)
    {
        if (tp == null) Debug.LogError("IllegalArgumentException");
        unuizeAll();
        updateHeader(tp.getPhasePlayer());

        Caard caard = tp.getCaardToShow();
        uiCaard.setCaard(caard);
    }
    public void tacticalUpdate()
    {
        SetPosRot();
        uiCaard.update();
    }
    private void updateHeader(Player player)
    {
        if (player == null) Debug.LogError("IllegalArgumentException");
        TextMeshProUGUI header = getLinker().getHeader();
        header.text = player.getName();
        getGFX().setColAtPlace(player.getMatPlace(), RendPlace.header);
    }
    void SetPosRot()
    {
        Transform t = getTopTransform();
        Quaternion camRotation = Cam.getRotation();
        Vector3 camPos = Cam.getV3();
        t.rotation = camRotation;
        t.position = camPos + t.forward * canvasDistance;
    }
}
