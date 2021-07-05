using UnityEngine;

public class ViewBattles : View
{
    public ViewBattles(Phase phase) : base(phase) { }
    protected override bool bupdateVirtual(Control c)
    {
        Debug.Log("penis");
        getCam().eagleView(c);
        getSelectionManager().resetSelections();
        return (life > 10000);
    }
}
