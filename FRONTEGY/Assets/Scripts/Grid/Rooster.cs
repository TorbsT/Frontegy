using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooster
{  // All rosters
    /*  UNUSED
    private GameMaster gm;  // Stored as state since this is accessed by many objects
    private List<object> rosters;
    Roster<CardPhy> cardRoster;

    public Rooster(int troopPhyCount, int cardPhyCount, int tilePhyCount)
    {
        gm = GameMaster.GetGM();
        rosters = new List<object>();
        cardRoster = new Roster<CardPhy>();
        rosters.Add(cardRoster);
    }
    public Roster<Phy> getCorrectRoster() { return cardRoster; }
    public void unstageAll()
    {
        cardRoster.unstageAll();
        tileRoster.unstageAll();
        troopRoster.unstageAll();
        pafRoster.unstageAll();
    }
    public GameMaster getGM() { if (gm == null) Debug.LogError("Should never happen"); return gm; }
    public CardPhy getUnstagedCardPhy()
    {
        Phy p = cardRoster.getUnstagedPhy();
        CardPhy cp = (CardPhy)p;
        return cp;
    }
    public TilePhy getUnstagedTilePhy()
    {
        Phy p = tileRoster.getUnstagedPhy();
        TilePhy tp = (TilePhy)p;
        return tp;
    }
    public TroopPhy getUnstagedTroopPhy() { return (TroopPhy)troopRoster.getUnstagedPhy(); }
    public PafPhy getUnstagedPafPhy() { return (PafPhy)pafRoster.getUnstagedPhy(); }
    */
}
