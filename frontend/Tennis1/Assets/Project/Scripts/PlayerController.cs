using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Tennis1.Common.Adsorption.ControllAdsorber
{
    public GameObject Panel;
    Tennis1.Common.IControll _Controll;
    public void Visible(bool enable)
    {
        Panel.SetActive(enable);
    }
	public void Get(Tennis1.Common.IControll controll)
    {
        _Controll = controll;
    }

    public void Forward()
    {
        _Controll.Move(new Regulus.CustomType.Vector2(0, 1));
    }
    public void Back()
    {
        _Controll.Move(new Regulus.CustomType.Vector2(0, -1));

    }

    public void Left()
    {
        _Controll.Move(new Regulus.CustomType.Vector2(-1, 0));

    }

    public void Right()
    {
        _Controll.Move(new Regulus.CustomType.Vector2(1, 0));

    }

    public void Stop()
    {
        _Controll.Move(new Regulus.CustomType.Vector2(0, 0));

    }
}
