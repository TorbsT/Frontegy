using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public Transive transive { get => _transive; }
    private Transform canvasTransform { get => _canvas.transform; }
    private Transive camTransive { get => Cam.Instance.transive; }
    public static Pos3 position { get => Instance._position; }
    [System.NonSerialized] private GameMaster gm;
    private Vector2Int _resolution;
    private Rect _canvasRect;

    [SerializeField] private Pos3 _position;
    [SerializeField] private List<UIRect> _uiRects;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshPro header;
    [SerializeReference] private Transive _transive;
    
    [SerializeReference] private UICaard uiCaard;
    private GFX _gfx;
    
    //private static Scale scale = new Scale(0.1f, 0.1f, 0.1f);
    private static Scale scale = Scale.identity;
    private Cam cam => Cam.Instance;


    private void Awake()
    {
        Instance = this;
        Debug.Log("CIJJJ");
        gm = GameMaster.GetGM();
        _gfx = GetComponent<GFX>();
        if (_gfx == null) Debug.LogError("InspectorException: UIManager does not have GFX component");
        _canvasRect = _canvas.pixelRect;
        Debug.Log(_canvas.referencePixelsPerUnit);
        _transive = new Transive(transform, null);
        _transive.scalep.set(scale);
        _transive.pos3p.set(_position);
        /*
        foreach (UIRect uiRect in _uiRects)
        {
            uiRect.setTransParent(_transive);
        }
        */
    }
    void FixedUpdate()
    {
        if (uiCaard != null) uiCaard.fixedUpdate();
    }
    public void restart()
    {
        uiCaard = new UICaard();
        Debug.Log("EYEP");
    }
    public UIRect getUIRectAtPlace(UIPlace place) => _uiRects.Find(match => match.place == place);
    public void battleStart()
    {
        uiCaard.empty();
    }
    public void tacticalStart(TacticalPhase tp)
    {
        if (tp == null) Debug.LogError("IllegalArgumentException");
        updateHeader(tp.getPhasePlayer());

        Debug.Log("Tacticalstart");

        List<Card> cards = tp.getCaardToShow();
        Debug.Log(uiCaard);  // HOW CAN THIS BE NULL
        uiCaard.setCards(cards);
    }
    public void tacticalUpdate()
    {
        
    }
    public void uize(Transive t)
    {
        t.transform.SetParent(canvasTransform);
    }
    public void unuize(Transive t)
    {
        t.transform.SetParent(null);
    }

    private void updateHeader(Player player)
    {
        if (player == null) Debug.LogError("IllegalArgumentException");
        header.text = player.getName();
        _gfx.setColAtPlace("header", player.getMatPlace());
    }
}
