using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager
{
    private Grid grid;
    private Round _round;

    public RoundManager(Grid grid)
    {
        if (grid == null) Debug.LogError("IllegalArgumentException");
        this.grid = grid;
        _round = new Round(this, 0);
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
        Grid.Instance.makeStatesForNewRound(_round);
        _round = new Round(this, _round.roundId+1);
    }
}
