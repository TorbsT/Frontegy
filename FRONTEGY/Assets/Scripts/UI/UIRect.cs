using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIRect
{
    public UIPlace place { get { return _place; } }
    public RectTransform rect { get { return _rect; } }
    public Transtatic trans { get { if (_transtatic == null) _transtatic = new Transtatic(rect, UIManager.Instance.transive); return _transtatic; } }

    [SerializeField] private UIPlace _place;
    [SerializeField] private RectTransform _rect;
    [SerializeReference] private Transtatic _transtatic;
    [System.NonSerialized] private Trans parentTrans;

    public void setTransParent(Trans trans)
    {
        // TODOthis.trans.  // ACTUALLY, finding _world for transtatic may be necessary as well?
    }
}
