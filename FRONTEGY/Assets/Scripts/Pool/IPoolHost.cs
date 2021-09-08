using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolHost
{
    bool connected { get; set; }
    IPoolClient chy { set; }
    void unstage();  // E.g. when restarting and all Phys should be unstaged
    bool tryRagdollMode();
    bool tryUnragdollMode();
    void justConnected();
    void justDisconnected();
}
