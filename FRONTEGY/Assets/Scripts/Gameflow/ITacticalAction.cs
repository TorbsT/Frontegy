using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITacticalAction
{
    bool wasLegal();  // Was this action legal when first taken?
    bool legal();  // Can this action be taken at this time?  
    void apply();  // Apply this action to current round state
}
