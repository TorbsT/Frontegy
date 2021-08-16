using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransObserver : System.IObserver<Trans>
{
    public void OnCompleted()
    {
        throw new NotImplementedException();
    }

    public void OnError(Exception error)
    {
        throw new NotImplementedException();
    }

    public void OnNext(Trans value)
    {
        throw new NotImplementedException();
    }
}
