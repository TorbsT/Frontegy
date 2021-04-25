using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : ScriptableObject
{
    // PARAMS WILL BE DEFINED IN STATIC OBJECTS  // ...excuse me what does that mean
    // 21/21/04 - "static objects" ...do you have any idea what static means

    [System.NonSerialized] public GameMaster gameMaster;
    [System.NonSerialized] public SelectionManager selectionManager;
    [System.NonSerialized] public Selectable selHover;

    public virtual void Cast(Card parent)
    {
        gameMaster = GameMaster.GetGM();
        selectionManager = GameMaster.GetSelectionManager();
        selHover = selectionManager.GetSelectable(selectionManager.hoveredObj);
    }
}