using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComparable<T>
{
    bool equals(T t);
}
