using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooster
{  // All rosters
    private GameMaster gm;  // Stored as state since this is accessed by many objects
    private CardRoster cardRoster;
    private TileRoster tileRoster;
    private TroopRoster troopRoster;
    private PafRoster pafRoster;

    public Rooster(int troopPhyCount, int cardPhyCount, int tilePhyCount)
    {
        gm = GameMaster.GetGM();
        cardRoster = new CardRoster(this, cardPhyCount);
        tileRoster = new TileRoster(this, tilePhyCount);
        troopRoster = new TroopRoster(this, troopPhyCount);
        pafRoster = new PafRoster(this, troopPhyCount);
    }
    public GameMaster getGM() { if (gm == null) Debug.LogError("Should never happen"); return gm; }
    public CardPhy getUnstagedCardPhy() { return (CardPhy)cardRoster.getUnstagedPhy(); }
    public TilePhy getUnstagedTilePhy() { return (TilePhy)tileRoster.getUnstagedPhy(); }
    public TroopPhy getUnstagedTroopPhy() { return (TroopPhy)troopRoster.getUnstagedPhy(); }
    public PafPhy getUnstagedPafPhy() { return (PafPhy)pafRoster.getUnstagedPhy(); }
}
