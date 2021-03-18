using UnityEngine;

public class View
{
    protected int life;
    protected CameraScript cs;
    public View()
    {
        life = 0;
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
