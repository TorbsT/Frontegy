using System.Collections.Generic;
using UnityEngine;

public abstract class Conflict
{
    public Conflict(int step, Troop a, Troop b)
    {
        // Standardization:
        // When creating a new conflict, have two troops
        // More involved troops are found and can be merged
        this.involvedTroops = new List<Troop>();
        addTroop(a);
        addTroop(b);
        init(step);
    }
    public void init(int step)
    {
        modified = true;
        this.step = step;
        if (noInvolvedTroops()) Debug.LogError("Tried to make a conflict with 0 or null involvedTroops");
    }
    //public List<TroopStats> originalTroops;
    // Use an easier structure, not working at NASA here
    // yes we are working at NASA, fuck easy structures
    private List<Troop> involvedTroops;
    private List<Troop>[] winnersAndLosers;  // stored as state, since it should be calculated as rarely as possible
    private bool modified;
    private int step;


    public void merge(Conflict c)
    {
        foreach (Troop t in getInvolvedTroops())
        {
            addTroop(t);
        }
    }
    private void addTroop(Troop t)
    {
        modified = true;
        involvedTroops.Add(t);
    }

    public Consequi makeConsequi()
    {  // TODO maybe change name to getConsequi, more standardized?
        Consequi consequi = new Consequi();
        foreach (Troop t in getLosers())
        {
            Consequence c = new Consequence(step, t);
            c.setDies();
            consequi.add(c);
        }
        return consequi;
    }
    public List<Troop> getInvolvedTroops() { return involvedTroops; }
    private List<Troop> getWinners() { return getWinnersAndLosers()[0]; }
    private List<Troop> getLosers() { return getWinnersAndLosers()[1]; }
    private List<Troop>[] getWinnersAndLosers()
    {
        if (modified) decideWinnersAndLosers();
        return winnersAndLosers;
    }
    private List<Troop>[] decideWinnersAndLosers()
    {  // 0 is winners, 1 is losers
        modified = false;

        List<Troop>[] winnersAndLosers = new List<Troop>[2];
        List<Troop> winners = new List<Troop>();
        List<Troop> losers = new List<Troop>();

        Player winningPlayer = getWinningPlayer();
        winners = getTroopsOfPlayer(winningPlayer);
        losers = getTroopsNotOfPlayer(winningPlayer);

        winnersAndLosers[0] = winners;
        winnersAndLosers[1] = losers;
        return winnersAndLosers;
    }
    private Player getWinningPlayer()
    {
        
        return winningPlayerByTroopCount();
        
    }
    private Player winningPlayerByTroopCount()
    {  // mostly used for debug purposes
        List<Player> players = getInvolvedPlayers();
        int record = 0;
        Player recordHolder = null;
        foreach (Player player in players)
        {
            int counter = getTroopsOfPlayer(player).Count;

            if (counter > record)
            {
                recordHolder = player;
                record = counter;
            }
        }
        if (recordHolder == null) Debug.LogError("Something terribly wrong has happened");
        return recordHolder;
    }
    private List<Troop> getTroopsNotOfPlayer(Player p)
    {
        List<Troop> troops = new List<Troop>();
        foreach (Troop troop in involvedTroops)
        {
            if (!troop.getPlayer().isSamePlayer(p)) troops.Add(troop);
        }
        return troops;
    }
    private List<Troop> getTroopsOfPlayer(Player p)
    {
        List<Troop> troops = new List<Troop>();
        foreach (Troop troop in involvedTroops)
        {
            if (troop.getPlayer().isSamePlayer(p)) troops.Add(troop);
        }
        return troops;
    }
    private List<Player> getInvolvedPlayers()
    {
        List<Player> players = new List<Player>();
        for (int i = 0; i < involvedTroopsCount(); i++)
        {
            Troop troop = getTroop(i);
            Player p = troop.getPlayer();
            // is this player already registered?
            bool newPlayer = true;
            foreach (Player registeredPlayer in players)
            {
                if (p.isSamePlayer(registeredPlayer))
                {
                    newPlayer = false;
                    break;
                }
            }
            if (newPlayer) players.Add(p);
        }
        return players;
    }
    private Troop getTroop(int index)
    {
        if (troopOutOfRange(index))
        {
            Debug.LogError("Tried getting troop out of range");
            return null;
        }
        return involvedTroops[index];
    }
    private bool troopOutOfRange(int index)
    {
        bool o = index < 0 || index >= involvedTroopsCount();
        return o;
    }
    private bool noInvolvedTroops() { return involvedTroopsCount() == 0; }
    private int involvedTroopsCount()
    {
        if (involvedTroops == null) return 0;
        return involvedTroops.Count;
    }

    /*
    public void ManualUpdate()
    {

    }
    public void AutoResolve()
    {
        int winnerPlayerId;

        float aPower = a.stats.units[0].myRole.stats.ATK;
        float bPower = b.stats.units[0].myRole.stats.ATK;
        if (aPower > bPower)
        {
            winnerPlayerId = a.stats.playerId;
        }
        else winnerPlayerId = b.stats.playerId;
        Debug.Log("Winner: " + winnerPlayerId);
    }
    public void DebugTroops(List<TroopData> troops)
    {
        foreach (TroopData troop in troops)
        {
            Debug.Log("The following troop is from team "+troop.playerId);
            foreach (Unit unit in troop.units)
            {
                Debug.Log(unit.myRole.displayName);
            }
        }
    }
    public List<TroopData> GetRankedTroops(List<TroopData> troopsToRank)
    {
        List<TroopData> troopsInRank = new List<TroopData>();

        foreach (TroopData troopToRank in troopsToRank)
        {
            TroopData rankedTroop = GetRankedTroop(troopToRank);
            troopsInRank.Add(rankedTroop);
        }
        return troopsInRank;
    }
    TroopData GetRankedTroop(TroopData troopToRank)  // "In rank" means sorted so:
    {
        // Spy, King, Gen., Col., Capt., Major, Ltn., Pvt.
        TroopData rankedTroop = new TroopData(0, new List<Unit>()); // BAD BAD BAD BAD BAD BAD BAD BAD BAD

        foreach (Unit unitToRank in troopToRank.units)  // INSERTION SORT!
        {
            int insertPriority = unitToRank.myRole.priority;
            int idToInsertInto = rankedTroop.units.Count;
            for (int i = 0; i < rankedTroop.units.Count; i++)
            {
                Unit thisUnit = rankedTroop.units[i];
                int thisPriority = thisUnit.myRole.priority;
                if (thisUnit.isDead)
                if (insertPriority >= thisPriority)
                {
                    idToInsertInto = i;
                    break;  // we got him boys
                }
                // Worse
            }

            if (idToInsertInto == rankedTroop.units.Count)  // If it's the worst so far!
                rankedTroop.units.Add(unitToRank);
            else
                rankedTroop.units.Insert(idToInsertInto, unitToRank);
        }
        return rankedTroop;
    }
    */
    public bool canMerge(Conflict c)
    {
        if (!sameStep(c)) return false;
        if (!sameLoc(c)) return false;
        return true;
    }
    private bool sameStep(Conflict c)
    {
        return getStep() == c.getStep();
    }
    public abstract bool sameLoc(Conflict c);

    public bool isStep(int step) { return this.step == step; }
    public int getStep() { return step; }
}
