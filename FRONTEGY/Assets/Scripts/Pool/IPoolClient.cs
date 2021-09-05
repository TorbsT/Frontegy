using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolClient
{
    bool connected { get; set; }
    void stage();
    void unstage();
}
