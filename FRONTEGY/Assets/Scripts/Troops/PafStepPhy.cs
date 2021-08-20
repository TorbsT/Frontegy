using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PafStepPhy : Phy
{
    public GameObject endGO { get => _endGO; }
    public GameObject straightGO { get => _straightGO; }
    public GameObject turnGO { get => _turnGO; }

    [SerializeField] private GameObject _endGO;
    [SerializeField] private GameObject _straightGO;
    [SerializeField] private GameObject _turnGO;


    public PafStepPhy() { }


    protected override void Awake()
    {
        base.Awake();
        if (_endGO == null) Debug.LogError("InspectorException: assign PafStep.endGO");
        if (_straightGO == null) Debug.LogError("InspectorException: assign PafStep.straightGO");
        if (_turnGO == null) Debug.LogError("InspectorException: assign PafStep.turnGO");
    }

    public PafStepChy getPafChy() { return PafStepPool.Instance.getClient(this); }
    protected override Chy getChy() { return getPafChy(); }
    public override void unstage() { PafStepPool.Instance.unstage(this); }

}
