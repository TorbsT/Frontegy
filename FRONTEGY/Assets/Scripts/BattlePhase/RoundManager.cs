using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager
{
    public static RoundManager Instance { get; private set; }
    public int roundId { get => _roundId; }

    private Grid grid;
    private Round _round;
    private int _roundId;

    public RoundManager(Grid grid)
    {
        Instance = this;
        if (grid == null) Debug.LogError("IllegalArgumentException");
        this.grid = grid;
        _roundId = 0;
        _round = new Round(this, _roundId);
    }
    public void update(Control c)
    {
        bool roundOver = _round.bupdate(c);
        if (roundOver) nextRound();
    }
    public void attemptSkip()
    {
        nextRound();
    }
    private void nextRound()
    {
        _roundId++;
        Grid.Instance.makeStatesForNewRound(_round);
        _round = new Round(this, _roundId);
    }
}
