using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable<Observer>
{
    void subscribe(Observer observer);
    void unsubscribe(Observer observer);
    void fireStateChanged(IObservable<Observer> observable);
}
