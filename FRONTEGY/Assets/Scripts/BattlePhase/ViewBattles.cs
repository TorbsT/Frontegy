using UnityEngine;

public class ViewBattles : View
{
    public ViewBattles(Phase phase) : base(phase) { }
    protected override bool bupdateVirtual()
    {
        cs.eagleView();
        return (life > 10000);
    }
}
