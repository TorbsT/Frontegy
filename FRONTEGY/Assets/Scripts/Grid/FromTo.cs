using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FromTo
{
    private Tile from;
    private Tile to;
    public FromTo(Tile from, Tile to)
    {
        this.from = from;
        this.to = to;
        validate();
    }

    public Tile getFrom() { validate(); return from; }
    public Tile getTo() { validate(); return to; }
    private void validate()
    {
        if (from == null) Debug.LogError("Should never happen");
        if (to == null) Debug.LogError("Should never happen");
    }

    public bool meets(FromTo ft)
    {
        return getTo().sameTile(ft.getTo());
    }
    public bool passes(FromTo ft)
    {
        return (getTo().sameTile(ft.getFrom()) && getFrom().sameTile(ft.getTo()));
    }
}
