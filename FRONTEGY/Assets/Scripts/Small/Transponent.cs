using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transponent : MonoBehaviour
{
    // Functions as a wrapper for transtatic.
    // The only purpose of this component is to debug in inspector.

    public Transtatic transtatic { set { _transtatic = value;
                                         } }

    [SerializeReference] private Transtatic _transtatic;
}
