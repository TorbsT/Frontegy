using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Playyer
{
    public static Playyer Instance;

    [SerializeField] private NonePlayer nonePlayer;
    [SerializeField] private List<Player> players;

    public void init()
    {
        Instance = this;
        nonePlayer.init();
    }

    public Player getPlayerByIndex(int index)
    {
        if (outOfBounds(index)) Debug.LogError("IndexOutOfBoundsException");
        return getPlayers()[index];
    }

    public Player playerAfter(Player player)
    {
        if (player == null) Debug.LogError("IllegalArgumentException");
        if (isLastPlayer(player)) Debug.LogError("IllegalArgumentException: This is the last player, no players are after.");

        for (int i = 0; i < getPlayerCount(); i++)
        {
            Player p = getPlayerByIndex(i);
            if (p.Equals(player))
            {
                return getPlayerByIndex(i + 1);
            }
        }
        Debug.LogError("IllegalArgumentException: player was not found");
        return null;
    }

    public Player getFirstPlayer()
    {
        if (noPlayers()) Debug.LogError("IllegalStateException");
        return getPlayerByIndex(0);
    }

    public Player getNonePlayer()
    {
        if (nonePlayer == null) Debug.LogError("IllegalStateException");
        return nonePlayer;
    }

    public bool isLastPlayer(Player player)
    {
        if (noPlayers()) Debug.LogError("IllegalStateException: There are no players.");
        int lastIndex = getPlayerCount() - 1;
        return getPlayerByIndex(lastIndex).Equals(player);
    }


    public bool outOfBounds(int index) { return index < 0 || index >= getPlayerCount(); }
    public bool noPlayers() { return getPlayerCount() == 0; }
    public int getPlayerCount() { return getPlayers().Count; }
    public List<Player> getPlayers()
    {
        if (players == null) Debug.LogError("IllegalStateException");
        return players;
    }
}
