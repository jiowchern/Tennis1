using System.Collections;
using System.Collections.Generic;
using Regulus.Remote;
using UnityEngine;

public class Agent : Tennis1.Tennis1Agent
{
    public override IAgent _GetAgent()
    {
        return Client.GetUser().Agent;
    }

    
}
