using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rekt : MonoBehaviour, ITransParent
{
    [SerializeField] private RectTransform _transform;
    private Rect _worldRect;

}
