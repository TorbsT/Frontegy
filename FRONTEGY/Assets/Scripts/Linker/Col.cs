using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Col
{  // Wrapper for Color that makes it nullable

    public string place { get => _place; }
    public Color color { get => _color; }

    [SerializeField] private string _place;
    [SerializeField] private Color _color;
}
