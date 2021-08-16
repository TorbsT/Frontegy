using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileState
{
    public int roundId { get => _roundId; set { _roundId = value; } }
    public TileLoc loc { get => _loc; set { _loc = value; } }
    public bool active { get => _active; set { _active = value; } }
    public int ownerId { get => _owner.id; set { _owner = Playyer.Instance.getPlayerByIndex(value); } }
    public Player owner { get => _owner; set { _owner = value; } }

    private int _roundId;
    private TileLoc _loc;
    private bool _active;
    private Player _owner;

    public TileState copy()
    {
        return new TileState
        {
            _roundId = _roundId,
            _loc = _loc,
            _active = _active,
            _owner = _owner
        };
    }
}
