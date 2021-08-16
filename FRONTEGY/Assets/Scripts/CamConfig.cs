using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CamConfig
{
    [SerializeField] private float height;
    [SerializeField] private Vector2 verAngleLimits;
    [SerializeField] private float horSpeed;
    [SerializeField] private float verSpeed;

    public float getHeight() { return height; }
    public Vector2 getVerAngleLimits() { return verAngleLimits; }
    public float getHorSpeed() { return horSpeed; }
    public float getVerSpeed() { return verSpeed; }

}
