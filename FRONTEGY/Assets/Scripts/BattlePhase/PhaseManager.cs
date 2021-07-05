using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseManager
{
    private Grid grid;
    private Round round;
    private int roundId;

    public PhaseManager(Grid grid)
    {
        if (grid == null) Debug.LogError("IllegalArgumentException");
        this.grid = grid;
        roundId = 0;
        round = new Round(this, roundId);
    }

    public Player getFirstPlayer()
    {
        return grid.getFirstPlayer();
    }
    public Player getPlayer() { return getRound().getPlayer(); }
    public void update(Control c)
    {
        bool roundOver = round.bupdate(c);
        if (roundOver) nextRound();
    }
    public void attemptSkip()
    {
        nextRound();
    }
    private void nextRound()
    {
        roundId++;
        getAllGroop().newRound(getRound().getResults());
        round = new Round(this, roundId);
    }
    public bool isLastPlayer(Player player)
    {
        return grid.isLastPlayer(player);
    }
    public Player playerAfter(Player player)
    {
        return getGrid().playerAfter(player);
    }
    public Player getNonePlayer()
    {
        return getGrid().getNonePlayer();
    }
    public TroopPlan getTroopPlan(Troop troop) { return getRound().getTroopPlan(troop); }
    public SelMan getSelectionManager() { return getGrid().getSelectionManager(); }
    public Groop getAllGroop() { return getGrid().getAllGroop(); }
    public UIManager getUiManager() { return getGrid().UiManager; }
    public Cam getCam() { return getGrid().getCam(); }
    public Grid getGrid() { if (grid == null) Debug.LogError("IllegalStateException"); return grid; }
    public int getRoundId() { return roundId; }
    public Round getRound() { if (round == null) Debug.LogError("IllegalStateException"); return round; }
}
