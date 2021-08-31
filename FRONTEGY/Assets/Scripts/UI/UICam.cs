using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICam : MonoBehaviour
{
    public static UICam Instance { get; private set; }
    public Transive transive { get => _transive; }

    private Camera _camera;
    [SerializeReference] private Transive _transive;
    
    void Awake()
    {
        Debug.Log("TISS");
        Instance = this;
        _camera = GetComponent<Camera>();
        if (_camera == null) Debug.LogError("InspectorException: UICam has no Camera component");
        _transive = new Transive(transform);
        
    }
    void Start()
    {
        _transive.pos3p.set(UIManager.position);
    }

    public GameObject getMousedGO(Control control)
    {
        //Debug.Log("selMan: "+getTransform().position);
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(control.getMousePosition());
        //Debug.DrawRay(_transive.pos3p.get().v3, ray.direction, Color.red, 1f);

        Debug.Log("trying");
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Found " +hit.transform.gameObject);
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
    } 
}
