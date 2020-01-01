using System.Collections;
using System.Collections.Generic;
using Tennis1.Common;
using UnityEngine;

public class PlayerReincarnation : Tennis1.PlayerBroadcaster 
{
    public GameObject Shell;
    
    readonly System.Collections.Generic.Dictionary<System.Guid, GameObject> _Players;
    public PlayerReincarnation()
    {
        _Players = new Dictionary<System.Guid, GameObject>();
    }
    public void Supply(IPlayer player)
    {
        var playerShell = Instantiate(Shell);
        playerShell.SetActive(true);
        playerShell.transform.parent = gameObject.transform;
        _Players.Add(player.Id , playerShell);        
    }

    public void Unsupply(IPlayer player)
    {
        GameObject p = null;
        if(_Players.TryGetValue(player.Id , out p))
        {
            Destroy(p);
        }
    }
}
