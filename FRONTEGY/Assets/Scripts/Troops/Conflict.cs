using System.Collections.Generic;
using UnityEngine;

public abstract class Conflict
{
    public int step { get => _step; }
    public int roundId { get => _roundId; }

    private List<Duel> _duels;
    private List<TroopState> _winners = new List<TroopState>();
    private List<TroopState> _losers = new List<TroopState>();
    private bool _computed = false;
    private List<int> _involvedPlayers;
    private List<TroopState> _involvedStateWrappers;

    // Use an easier structure, not working at NASA here
    // yes we are working at NASA, fuck easy structures
    private List<int> _involvedTroops;
    private int _step;
    private int _roundId;

    public Conflict(int roundId, int step, List<int> involvedTroops)
    {
        // Standardization:
        // When creating a new conflict, have two troops
        // More involved troops are found and can be merged
        _involvedTroops = involvedTroops;

        _roundId = roundId;
        _step = step;
        if (_involvedTroops.Count == 0) Debug.LogError("Tried to make a conflict with 0 or null involvedTroops");
    }
    public void mergeConflicts(Conflict c)
    {
        foreach (int id in c._involvedTroops)
        {
            addTroop(id);
        }
    }
    private void addTroop(int id)
    {
        _computed = false;
        _involvedTroops.Add(id);
    }

    public void compute()
    {
        _computed = true;

        // Sets up helper lists.
        _involvedPlayers = new List<int>();
        _involvedStateWrappers = new List<TroopState>();
        List<TroopState> haventfought = new List<TroopState>();
        for (int i = 0; i < _involvedTroops.Count; i++)
        {
            TroopState state = getTroopState(_involvedTroops[i]);
            haventfought.Add(state);
            _involvedStateWrappers.Add(state);
            int playerId = state.ownerId;
            // is this player already registered?
            if (!_involvedPlayers.Contains(playerId)) _involvedPlayers.Add(playerId);
        }

        // Sorts the involved state wrappers. This makes sure the strongest troops fight against eachother first
        _involvedStateWrappers.Sort(TroopState.defaultTroopComparison);
        while (true)
        {
            // THIS METHOD IS SHITTY. ONCE THE GAME WORKS, PLEASE FIX THIS TODO
            // Gets one troop-statewrapper for each involved player
            List<TroopState> duelTogether = new List<TroopState>();
            foreach (int playerId in _involvedPlayers)
            {
                TroopState duellant = haventfought.Find(wrapper => wrapper.ownerId == playerId);
                if (duellant == null) continue;
                duelTogether.Add(duellant);
            }
            if (duelTogether.Count > 1)
            {
                Duel duel = new Duel(duelTogether);
                _duels.Add(duel);
                _winners.Add(duel.winner);
                _losers.AddRange(duel.losers);

                // Remove these so they don't fight again
                haventfought.Remove(duel.winner);
                foreach (TroopState loser in duel.losers)
                {
                    foreach (TroopState match in haventfought)
                    {
                        if (match.Equals(loser))
                        {
                            haventfought.Remove(loser);
                            break;
                        }
                    }
                }
            }
            else if (duelTogether.Count == 1)
            {
                _winners.Add(duelTogether[0]);
                break;
            }
            else
            {
                break;
            }

        }

        

        foreach (TroopState loser in _losers)
        {
            loser.stepStates.addConsequence(new Consequence(_roundId, _step, loser.id, true));
        }
    }
    private TroopState getTroopState(int id)
    {
        foreach (int i in _involvedTroops)
        {
            if (i == id) return Grid.Instance.troopStates.Find(troop => troop.id == id && troop.roundId == _roundId);
        }
        return null;
    }

    /*
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
        if (_step != c._step) return false;
        if (!sameLoc(c)) return false;
        return true;
    }
    public abstract bool sameLoc(Conflict c);
    public static Conflict makeConflict(int roundId, int stepId, TroopState a, TroopState b)
    {
        if (stepId <= 0) Debug.LogError("step should start at 1");
        Tile af = a.stepStates.getStepState(stepId - 1).currentBreadcrumb.tile;
        Tile at = a.stepStates.getStepState(stepId).currentBreadcrumb.tile;
        Tile bf = b.stepStates.getStepState(stepId - 1).currentBreadcrumb.tile;
        Tile bt = b.stepStates.getStepState(stepId).currentBreadcrumb.tile;
        FromTo aft = new FromTo(af, at);
        FromTo bft = new FromTo(bf, bt);
        int aId = a.id;
        int bId = b.id;

        if (FromTo.meet(aft, bft)) return new TileConflict(roundId, stepId, new List<int> { aId, bId });
        if (FromTo.pass(aft, bft)) return new BorderConflict(roundId, stepId, new List<int> { aId, bId });
        return null;  // No appropriate conflict found for the two troops
    }
}
