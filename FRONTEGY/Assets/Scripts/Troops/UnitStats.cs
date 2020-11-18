[System.Serializable]
public struct UnitStats
{
    public float HP;
    public float ATK;
    public float DEF;

    public int RANGE;

    
    public UnitStats(float _HP, float _ATK, float _DEF, int _RANGE = 1)
    {
        HP = _HP;
        ATK = _ATK;
        DEF = _DEF;
        RANGE = _RANGE;
    }
}
