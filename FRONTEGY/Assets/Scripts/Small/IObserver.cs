using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver<T>
{
    void stateChanged(T observable);
}
