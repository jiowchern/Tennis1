using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchHandler : MonoBehaviour
{
    public UnityEngine.UI.Text Ip;
    public UnityEngine.UI.Text Port;
    public UnityEngine.UI.Text Name;
    public UnityEngine.UI.Slider Number;
    public GameObject MatchMessage;
    public Agent Agent;
    
    public void Connect()
    {
        Agent.Connect(Ip.text, int.Parse(Port.text));
    }

    public void SignUp(Tennis1.Common.IPreparer preparer)
    {
        Tennis1.Common.Registration reg = default;
        reg.Name = Name.text;
        reg.PlayerNumber = (int)Number.value;
        preparer.SignUp(reg);
    }

    public void VisibleMatchMessage(bool visible)
    {
        MatchMessage.SetActive(visible);
    }



}
