using UnityEngine;

[System.Serializable]
public struct RoleStats
{
    [SerializeField] private int POW;
    [SerializeField] private int RANGE;
    
    public RoleStats(int POW, int RANGE = 1)
    {  // ONLY MADE IN CONSTRUCTOR. SEE MOODIFIER FOR MORE INFO
        if (POW < 0) Debug.LogError("IllegalArgumentException");
        if (RANGE < 1) Debug.LogError("IllegalArgumentException");
        this.POW = POW;
        this.RANGE = RANGE;
    }

    public RoleStats getModded() { return this; }
    public int getPOW() { return POW; }
    public int getRANGE() { return RANGE; }
}
