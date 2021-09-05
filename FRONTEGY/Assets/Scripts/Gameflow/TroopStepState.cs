using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopStepState
{
    public Breadcrumb currentBreadcrumb { get => _currentBreadcrumb; set { _currentBreadcrumb = value; } }
    public bool dead { get => _dead; set { _dead = value; } }

    private Breadcrumb _currentBreadcrumb;
    private bool _dead;

    public TroopStepState(Breadcrumb breadcrumb)
    {
        _currentBreadcrumb = breadcrumb;
    }
    public override string ToString() => "StepState{dead"+_dead+" bc"+_currentBreadcrumb+"}";
}
