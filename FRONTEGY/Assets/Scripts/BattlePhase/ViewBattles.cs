using UnityEngine;

public class ViewBattles : View
{
    public override bool bupdateVirtual()
    {
        cs.eagleView();
        return (life > 10000);
    }
}
