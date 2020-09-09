using UnityEngine;

public class Building : MonoBehaviour
{
    //GAMEMASTER NEEDS A LIST OF ALL BUILDINGS (manualupdate); MAYBE MOVE SELECTIONMANAGER TO GAMEMASTER?

    Tile parentTile;
    private bool isInitialized;
    private void ManualStart()
    {
        isInitialized = true;
    }
    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
    }
}
