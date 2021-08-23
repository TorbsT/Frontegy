using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolHost
{
    bool staged { get; }
    IPoolClient chy { set; }
    void unstage();  // E.g. when restarting and all Phys should be unstaged
}
