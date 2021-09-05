using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duel
{
    // A duel consists of atleast 2 troop-statewrappers.
    // All troop-statewrappers are from different teams, so there can only be one winner.
    public TroopState winner { get => _winner; }
    public List<TroopState> losers { get => _losers; }

    private List<TroopState> _involvedWrappers;
    private TroopState _winner;
    private List<TroopState> _losers;

    public Duel(List<TroopState> involvedWrappers)
    {
        _involvedWrappers = involvedWrappers;
        _involvedWrappers.Sort(TroopState.defaultTroopComparison);
        _winner = _involvedWrappers[0];
        _losers = new List<TroopState>();
        for (int i = 1; i < _involvedWrappers.Count; i++)
        {
            _losers.Add(_involvedWrappers[i]);
        }
        Debug.Log("Winner: " + _winner + ", loser0: " + _losers[0]);
    }
}
