using UnityEngine;

public class View
{
    protected Groop groop;
    protected int life;
    protected CameraScript cs;
    public View()
    {
        life = 0;
        groop = GameMaster.getAllGroop();
        cs = GameMaster.getCameraScript();
    }
    public bool bupdate()
    {
        bool finished = bupdateVirtual();
        life++;
        return finished;
    }
    public virtual bool bupdateVirtual()
    {
        return true;
    }
}
