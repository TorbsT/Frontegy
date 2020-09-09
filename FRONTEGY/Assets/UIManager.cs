using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] int waitFrames = 10;

    [Header("System")]
    GameMaster gameMaster;
    [SerializeField] Text header;
    
    int waitFrame;

    bool isInitialized = false;
    private void ManualStart()
    {
        isInitialized = true;
    }
    void Start()
    {
        waitFrame = 0;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }
    void Update()
    {
        if (!WaitFinish()) return;
        UpdateHeader();
    }
    bool WaitFinish()
    {
        waitFrame++;
        if (waitFrame > waitFrames)
        {
            return true;
        }
        return false;
    }
    private void UpdateHeader()
    {
        string txt;
        txt = gameMaster.phase.type.name;
        header.text = txt;
        header.color = gameMaster.GetPhaseTeam().material.color;
    }
}
