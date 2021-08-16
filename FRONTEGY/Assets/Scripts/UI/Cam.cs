using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cam
{  // OPT
    public static Cam Instance { get; private set; }

    private Transform transform;
    private Camera camera;
    [SerializeField] private CamConfig config;

    public void eagleView(Control c)
    {
        Debug.Log("eagle");
    }


    [SerializeField] private Degree horAngleDeg;
    [SerializeField] private Degree verAngleDeg;
    [SerializeField] private Radian horAngleRad;
    [SerializeField] private Radian verAngleRad;
    [SerializeField] private float circleRadius;
    [SerializeField] private Pos3 focus;

    public Cam(Camera camera, CamConfig config)
    {
        Instance = this;
        if (config == null) Debug.LogError("IllegalArgumentException");
        this.config = config;
        if (camera == null) Debug.LogError("IllegalArgumentException");
        this.camera = camera;
        camera.orthographic = true;
        CamLinker linker = camera.GetComponent<CamLinker>();
        if (linker == null) Debug.LogError("InspectorException: Camera has no CamLinker");
        linker.setCam(this);
        transform = camera.transform;
        if (transform == null) Debug.LogError("IllegalStateException");

        horAngleDeg = new Degree(0f);
        verAngleDeg = new Degree(0f, getVerAngleLimits());
    }

    public void focusOn(Pos3 p3)
    {
        focus = p3;
    }
    public void freeView(Control c)
    {
        getHorAngleDeg().add(-c.getHorAxis() * getHorSpeed() * Time.deltaTime);
        getVerAngleDeg().add(c.getVerAxis() * getVerSpeed()*Time.deltaTime);
        freeViewTransform();
    }

    private void freeViewTransform()
    {
        updateCircleRadius();

        Quaternion newRotation = Quaternion.Euler(new Vector3(getVerAngleDeg().get(), getHorAngleDeg().get(), 0f));
        Vector2 periferalVector = getPeriferalVector();
        Vector3 newPosition = new Vector3(periferalVector[0]+focus.x, getHeight(), periferalVector[1]+focus.z);


        getTransform().position = newPosition;
        getTransform().rotation = newRotation;
        //Debug.Log("camMove: " + getTransform().position);
    }
    Vector2 getPeriferalVector()
    {
        float rad = getHorAngleRad().get();
        float radSin = Mathf.Sin(rad);
        float radCos = Mathf.Cos(rad);
        Vector2 a = Vector2.up; // a: base vector to rotate degs degrees
        float x = a[0];
        float y = a[1];
        Vector2 returnVector = new Vector2(x * radCos + y * radSin, -x * radSin + y * radCos);
        // can be simplified to:
        //Vector2 returnVector = new Vector2(radSin, radCos);
        returnVector *= circleRadius;
        return returnVector;
    }
    private void updateCircleRadius()
    {
        circleRadius = Mathf.Tan(     Degree.floatDegToFloatRadian(     getVerAngleDeg().get()+90f      ))*getHeight();
    }

    public GameObject getMousedGO(Control control)
    {
        //Debug.Log("selMan: "+getTransform().position);
        RaycastHit hit;
        Ray ray = getCamera().ScreenPointToRay(control.getMousePosition());
        Debug.DrawRay(getTransform().position, ray.direction, Color.red, 1f);

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
    }

    private float getHeight()
    {
        return getConfig().getHeight();
    }
    private Vector2 getVerAngleLimits()
    {
        return getConfig().getVerAngleLimits();
    }
    private float getHorSpeed()
    {
        return getConfig().getHorSpeed();
    }
    private float getVerSpeed()
    {
        return getConfig().getVerSpeed();
    }
    private CamConfig getConfig()
    {
        if (config == null) Debug.LogError("IllegalStateException");
        return config;
    }



    private Radian getHorAngleRad()
    {
        if (horAngleRad == null) horAngleRad = new Radian(getHorAngleDeg());
        else horAngleRad.assimilateFrom(getHorAngleDeg());
        return horAngleRad;
    }
    private Radian getVerAngleRad()
    {
        if (verAngleRad == null) verAngleRad = new Radian(getVerAngleDeg());
        else verAngleRad.assimilateFrom(getVerAngleDeg());
        return verAngleRad;
    }
    private Degree getHorAngleDeg()
    {
        if (horAngleDeg == null) Debug.LogError("IllegalStateException");
        return horAngleDeg;
    }
    private Degree getVerAngleDeg()
    {
        if (verAngleDeg == null) Debug.LogError("IllegalStateException");
        return verAngleDeg;
    }



    private Camera getCamera()
    {
        if (camera == null) Debug.LogError("IllegalStateException");
        return camera;
    }

    public Vector3 getV3()
    {
        return getTransform().position;
    }
    public Quaternion getRotation()
    {
        return getTransform().rotation;
    }
    private Transform getTransform()
    {
        if (transform == null) Debug.LogError("IllegalStateException");
        return transform;
    }
}
