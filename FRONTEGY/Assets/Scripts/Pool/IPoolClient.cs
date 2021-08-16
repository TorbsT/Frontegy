using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolClient
{
    bool staged { get; set; }
    void stage();
    void unstage();
}
