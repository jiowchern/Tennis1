using System;
using System.Collections;
using System.Collections.Generic;
using Regulus.Remote;
using UnityEngine;

public class Agent : Tennis1.Tennis1Agent
{
    [Serializable]
    public class UnityAgentConnectEvent : UnityEngine.Events.UnityEvent<bool> { }
    public UnityAgentConnectEvent ConnectStatusEvent;
    bool _Status;
    
    new public void Update()
    {
        if (Client.GetUser() == null)
            return;
        var currentStatus = Client.GetUser().Agent.Connected;
        if (currentStatus != _Status)
        {
            _Status = currentStatus;
            ConnectStatusEvent.Invoke(currentStatus);
        }
        base.Update();

    }

    new public void OnDestroy()
    {
        
    }


   



    public override IAgent _GetAgent()
    {
        if (Client.GetUser() == null)
            return null;
        return Client.GetUser().Agent;
    }

    
}
