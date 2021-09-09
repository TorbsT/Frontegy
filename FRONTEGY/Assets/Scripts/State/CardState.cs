using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState
{
    public bool used { get => _used; set { _used = value; } }
    public int roundId { get => _roundId; set { _roundId = value; } }
    public int ownerId { get => _owner.id; set { _owner = Playyer.Instance.getPlayerByIndex(value); } }
    public Player owner { get => _owner; set { _owner = value; } }
    public SummonCardBP blueprint { get => _blueprint; set { _blueprint = value; } }

    private bool _used;
    private Player _owner;
    private int _roundId;
    private SummonCardBP _blueprint;
}
