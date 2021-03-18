using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float canvasDistance;
    [SerializeField] Transform uiTransform;
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] GameMaster gameMaster;
    bool isInitialized = false;

    void ManualStart()
    {
        isInitialized = true;
    }

    public void ManualUpdate()
    {
        if (!isInitialized) ManualStart();
        SetPosRot();
        UpdateHeader();
        DisplayPhaseHand();
    }
    void UpdateHeader()
    {
        Player phasePlayer = gameMaster.getCurrentPlayer();
        string txt = phasePlayer.name;
        Color color = phasePlayer.mat.color;

        header.color = color;
        header.text = txt;
    }
    void SetPosRot()
    {
        Quaternion camRotation = cam.rotation;
        Vector3 camPos = cam.position;
        uiTransform.rotation = camRotation;
        uiTransform.position = camPos+ uiTransform.forward*canvasDistance;
    }
    void DisplayPhaseHand()
    {
        DisplayHandOf(gameMaster.getCurrentPlayer());
    }
    void DisplayHandOf(Player player)
    {
        List<Card> cards = GetCardsInHandOf(player);
        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            card.Display();
            card.SetGORot(GetHandCardRotById(i));
            card.SetGOPos(GetHandCardPosById(i));
            card.UpdateGOScale();
        }
    }
    List<Card> GetCardsInHandOf(Player player)
    {  // Searches through every card in grid, returns all that are in the HAND of player.
        List<Card> cards = new List<Card>();
        foreach (Card card in gameMaster.grid.data.cards)
        {
            if (card.playerHolder == player) cards.Add(card);
        }
        return cards;
    }
    Quaternion GetHandCardRotById(int id)
    {
        Quaternion baseRot = uiTransform.rotation;
        Quaternion finalRot = baseRot * Quaternion.Euler(-90f, 0f, 0f);
        return finalRot;
    }
    Vector3 GetHandCardPosById(int id)
    {
        Vector3 basePos = uiTransform.position;
        float x = id;
        float y = 0;
        float z = 0;
        Vector3 addPos = new Vector3(x, y, z);
        Vector3 finalPos = basePos + addPos;
        return finalPos;
    }
}
