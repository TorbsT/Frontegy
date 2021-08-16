using UnityEngine;

public class ViewBattles : View
{
    public ViewBattles(Phase phase) : base(phase) { }
    protected override bool bupdateVirtual(Control c)
    {
        Debug.Log("penis");
        cam.eagleView(c);
        SelMan.Instance.resetSelections();
        return (life > 10000);
    }
}
