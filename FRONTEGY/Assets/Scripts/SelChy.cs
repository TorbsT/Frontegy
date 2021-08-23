using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SelChy : Chy, IPlayerOwned
{
    public abstract Player owner { get; }
    public int ownerId { get => owner.id; }



    public virtual void unhover()
    {
        uninteract();
    }
    public virtual void unselect()
    {
        uninteract();
    }
    protected virtual void uninteract()
    {
        setMat("selectable", getInitialSelMatPlace());
    }
    public virtual void hover()
    {
        setMat("selectable", "hover");
    }
    public virtual void primarySelect()
    {
        setMat("selectable", "select");
    }
    public virtual bool canSecondarySelectOn(SelChy selChy) => false;
    public virtual void secondarySelectOn(SelChy selChy) { }

    protected string getPlayerMatPlace()
    {
        if (owner == null) Debug.LogError(this + " tried accessing playerMatPlace, but owner was null");
        return owner.getMatPlace();
    }
    protected abstract string getInitialSelMatPlace();  // Not necessarily single mat, may be playerMat
}