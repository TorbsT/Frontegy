using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILinker : Linker
{
    // provides a reference to UIGOs for UIManager.
    // Also displays internal state of UIManager to inspector.
    public List<UIRect> uiRects { get => _uiRects; }

    [Header("View internal state")]
    [SerializeReference] private UIManager uiManager;

    [Header("Assign variables")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<UIRect> _uiRects;
    [SerializeField] private TextMeshProUGUI header;

    public void setUiManager(UIManager uiManager)
    {
        if (uiManager == null) Debug.LogError("IllegalArgumentException");
        if (this.uiManager != null) Debug.LogError("IllegalStateException");
        this.uiManager = uiManager;
    }
    public Trans getTransAtPlace(UIPlace place)
    {
        return getUIRectAtPlace(place).trans;
    }
    public RectTransform getRectAtPlace(UIPlace place)
    {
        return getUIRectAtPlace(place).rect;
    }
    private UIRect getUIRectAtPlace(UIPlace place)
    {
        if (place == UIPlace.none) Debug.LogError("IllegalArgumentException");
        foreach (UIRect rect in uiRects)
        {
            if (rect.place == place) return rect;
        }
        Debug.LogError("InspectorException: UIPlace '" + place + "' is unassigned but was tried accessed");
        return null;
    }
    public Canvas getCanvas()
    {
        if (canvas == null) Debug.LogError("InspectorException: Set UILinker.canvas");
        return canvas;
    }
    public TextMeshProUGUI getHeader()
    {
        if (header == null) Debug.LogError("InspectorException: Set UILinker.header");
        return header;
    }
}
