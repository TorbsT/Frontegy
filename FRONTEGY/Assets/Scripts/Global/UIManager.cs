using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float canvasDistance;
    [SerializeField] Transform uiTransform;
    bool isInitialized = false;

    void ManualStart()
    {
        isInitialized = true;
    }


    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
        SetPosRot();
    }

    void SetPosRot()
    {
        Quaternion camRotation = cam.rotation;
        Vector3 camPos = cam.position;
        uiTransform.rotation = camRotation;
        uiTransform.position = camPos+ uiTransform.forward*canvasDistance;
    }
}
