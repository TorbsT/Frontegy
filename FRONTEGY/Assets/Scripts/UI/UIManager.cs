﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class UIManager
{
    public static UIManager Instance { get; private set; }
    public Transive transive { get => _transive; }
    [System.NonSerialized] private GameMaster gm;

    [System.NonSerialized] private UILinker linker;
    [SerializeReference] private Transive _transive;
    [SerializeReference] private UICaard uiCaard;
    private static float canvasDistance = 0f;
    private Cam cam => Cam.Instance;



    public UIManager()
    {
        Instance = this;
        gm = GameMaster.GetGM();

        GameObject go = Object.Instantiate(getPrefab());
        if (go == null) Debug.LogError("WhatException: WHat the actual fuck");
        linker = go.GetComponent<UILinker>();
        if (linker == null) Debug.LogError("InspectorException: UIPrefab does not have UIController Component");
        linker.setUiManager(this);
        _transive = new Transive(linker.transform, cam.transive);
        _transive.pos3p.set(new Pos3(0, 0f, -canvasDistance), true);
        _transive.rotp.set(new Rot(Quaternion.identity), true);
        foreach (UIRect uiRect in linker.uiRects)
        {
            uiRect.setTransParent(_transive);
        }
    }

    public void restart()
    {
        uiCaard = new UICaard();
    }
    public Trans getTransAtPlace(UIPlace place)
    {
        return linker.getTransAtPlace(place);
    }
    public Transform getTransformAtPlace(UIPlace place)
    {
        return getRectAtPlace(place);
    }
    public RectTransform getRectAtPlace(UIPlace place)
    {
        return linker.getRectAtPlace(place);
    }
    private GFX getGFX()
    {
        return getLinker().getGFX();
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

        Debug.Log("Tacticalstart");

        List<Card> cards = tp.getCaardToShow();
        uiCaard.setCards(cards);
    }
    public void tacticalUpdate()
    {
        showTrans();
    }
    private void updateHeader(Player player)
    {
        if (player == null) Debug.LogError("IllegalArgumentException");
        TextMeshProUGUI header = getLinker().getHeader();
        header.text = player.getName();
        getGFX().setColAtPlace(player.getMatPlace(), RendPlace.header);
    }
    void showTrans()
    {
        /*
        Transform t = linker.transform;
        Quaternion camRotation = cam.getRotation();
        Vector3 camPos = cam.getV3();
        t.SetPositionAndRotation(camPos + t.forward * canvasDistance, camRotation);
        */
    }
}
