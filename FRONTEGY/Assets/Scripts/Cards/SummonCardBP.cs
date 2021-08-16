using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "SummonBP", menuName = "ScriptableObjects/SummonBP", order = 3)]
public class SummonCardBP : ScriptableObject, ICardBP
{
    // maybe add field for level to unlock
    //[SerializeField] private MatPlace ;  // think this through

    public int roleId { get => _role.id; }
    public Role role { get => _role; }
    public MatPlace frontFGMatPlace { get => _frontFGMatPlace; }

    [SerializeField] private Role _role;
    [SerializeField] private MatPlace _frontFGMatPlace;
    public void cast(Card card, Tile tile, CastType type)
    {
        if (card == null) Debug.LogError("IllegalArgumentException");

        TroopState state = new TroopState();
        state.parentTile = tile;
        state.owner = card.owner;
        state.role = card.role;
        Troop troop = new Troop(state);
    }

    public override string ToString() { return role.ToString(); }

}
