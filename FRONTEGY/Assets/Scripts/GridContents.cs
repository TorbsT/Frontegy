using System.Collections.Generic;

[System.Serializable]
public class GridContents
{
    public List<Geo> geos;
    public List<UnitStats> unitStats;

    public GridContents(List<Geo> geos, List<UnitStats> unitStats)
    {
        this.geos = geos;
        this.unitStats = unitStats;
    }
}
