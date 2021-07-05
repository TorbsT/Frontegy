using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Coontrol
{  // Few and probably only one. Also contains a lot of data, and changes a lot
    // Not very secure, speed is important here.
    private Queue<Control> controls;
    
    public Coontrol()
    {
        controls = new Queue<Control>();
    }
    public Control record()
    {
        Control c = new Control(true);
        controls.Enqueue(c);
        return c;
    }
    public Control replay()
    {
        return controls.Dequeue();
    }
}
