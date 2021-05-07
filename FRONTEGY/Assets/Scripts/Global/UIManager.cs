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
        string txt = phasePlayer.getName();
        Color color = phasePlayer.getMat().color;

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
        List<CardPhy> cards = getCaardInHandOf(player).getPhys();
        for (int i = 0; i < cards.Count; i++)
        {
            CardPhy card = cards[i];
            card.Display();
            card.SetGORot(GetHandCardRotById(i));
            card.setPos3(GetHandCardPos3ById(i));
            card.UpdateGOScale();
        }
    }
    Caard getCaardInHandOf(Player player)
    {  // Searches through every card in grid, returns all that are in the HAND of player.
        return gameMaster.grid.getAllCaard().getCaardInHandOf(player);
    }
    Quaternion GetHandCardRotById(int id)
    {
        Quaternion baseRot = uiTransform.rotation;
        Quaternion finalRot = baseRot * Quaternion.Euler(-90f, 0f, 0f);
        return finalRot;
    }
    Pos3 GetHandCardPos3ById(int id)
    {
        Pos3 basePos = new Pos3(uiTransform.position);
        float x = id;
        float y = 0;
        float z = 0;
        Pos3 addPos = new Pos3(x, y, z);
        Pos3 finalPos = Pos3.sum(basePos, addPos);
        return finalPos;
    }
}
