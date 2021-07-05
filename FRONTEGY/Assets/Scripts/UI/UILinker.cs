using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILinker : Linker
{
    // provides a reference to UIGOs for UIManager.
    // Also displays internal state of UIManager to inspector.
    [Header("View internal state")]
    [SerializeReference] private UIManager uiManager;

    [Header("Assign variables")]
    [SerializeField] private Transform top;
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private RectTransform caardBox;
    [SerializeField] private TextMeshProUGUI header;

    public void setUiManager(UIManager uiManager)
    {
        if (uiManager == null) Debug.LogError("IllegalArgumentException");
        if (this.uiManager != null) Debug.LogError("IllegalStateException");
        this.uiManager = uiManager;
    }

    public RectTransform getCaardBox()
    {
        if (caardBox == null) Debug.LogError("InspectorException: set UILinker.caardBox");
        return caardBox;
    }
    public RectTransform getCanvasTransform()
    {
        if (canvasTransform == null) Debug.LogError("InspectorException: set UILinker.canvasTransform");
        return canvasTransform;
    }
    public Canvas getCanvas()
    {
        if (canvas == null) Debug.LogError("InspectorException: Set UILinker.canvas");
        return canvas;
    }
    public Transform getTop()
    {
        if (top == null) Debug.LogError("InspectorException: Set UILinker.top");
        return top;
    }
    public TextMeshProUGUI getHeader()
    {
        if (header == null) Debug.LogError("InspectorException: Set UILinker.header");
        return header;
    }
}
