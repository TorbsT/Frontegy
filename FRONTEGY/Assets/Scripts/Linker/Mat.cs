using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mat
{
    public string place { get => _place; }
    public Material material { get => _material; }

    [SerializeField] private string _place;
    [SerializeField] private Material _material;
}
