using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIRect : MonoBehaviour
{
    public UIPlace place { get { return _place; } }
    public RectTransform rectTransform { get { return _rectTransform; } }

    [SerializeField] private UIPlace _place;
    [SerializeField] private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

    }

    public Rect getRect() => _rectTransform.rect;
    public Pos3 center => new Pos3(_rectTransform.localPosition);
}
