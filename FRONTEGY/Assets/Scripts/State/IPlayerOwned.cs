using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerOwned
{
    Player owner { get; }
    int ownerId { get; }
}
