using UnityEngine;

[System.Serializable]
public class GridPivotConfig
{
    public float offset;
    public float globalOffset;
    public GridPivotConfig(float offset, float globalOffset)
    {
        this.offset = offset;
        this.globalOffset = globalOffset;
    }
}
