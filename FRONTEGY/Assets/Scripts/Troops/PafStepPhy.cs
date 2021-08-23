using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PafStepPhy : Phy
{
    public GameObject endGO { get => _endGO; }
    public GameObject turnGO { get => _turnGO; }
    public GameObject circleGO { get => _circleGO; }
    public GameObject straightGO { get => _straightGO; }

    [SerializeField] private GameObject _endGO;
    [SerializeField] private GameObject _turnGO;
    [SerializeField] private GameObject _circleGO;
    [SerializeField] private GameObject _straightGO;


    public PafStepPhy() { }


    protected override void Awake()
    {
        base.Awake();
        if (_endGO == null) Debug.LogError("InspectorException: assign PafStep.endGO");
        if (_turnGO == null) Debug.LogError("InspectorException: assign PafStep.turnGO");
        if (_circleGO == null) Debug.LogError("InspectorException: assign PafStep.circleGO");
        if (_straightGO == null) Debug.LogError("InspectorException: assign PafStep.straightGO");
    }

    public PafStepChy getPafChy() { return PafStepPool.Instance.getClient(this); }
    protected override Chy getChy() { return getPafChy(); }
    public override void unstage() { PafStepPool.Instance.unstage(this); }

}
