using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Regulus.Framework;
using Tennis1.User;
using UnityEngine;

public class Client : MonoBehaviour
{
    private  Client<User> _Client;

    public Console Console;
    readonly Regulus.Utility.Updater _Updater;


    [System.Serializable]
    public class InitialedUnityEvent : UnityEngine.Events.UnityEvent { };
    public InitialedUnityEvent InitialedEvent;

    public Client()
    {
        _Updater = new Regulus.Utility.Updater();
    }
    void Start()
    {

        _Client = new Regulus.Framework.Client<Tennis1.User.User>(this.Console, this.Console.Command);
        _Client.ModeSelectorEvent += _Client_ModeSelectorEvent;
        
        _Updater.Add(_Client);


    }

    public void RunByRemote()
    {
        var asm = Regulus.Remote.Protocol.ProtocolProvider.GetProtocols().First().Assembly;
        _Selector.AddFactoty("remote", new Tennis1.User.RemoteUserFactory(asm));
        _Selector.CreateUserProvider("remote");
    }

    public void RunByStandalone()
    {
        var entry = new Tennis1.Game.Entry();
        entry.Launch();
        var asm = Regulus.Remote.Protocol.ProtocolProvider.GetProtocols().First().Assembly;
        _Selector.AddFactoty("standalone", new Tennis1.User.StandaloneUserFactory(entry, asm));
        _Selector.CreateUserProvider("standalone");

    }

    private void _Client_ModeSelectorEvent(GameModeSelector<User> selector)
    {
        _Selector = selector;
        selector.GameConsoleEvent += Selector_GameConsoleEvent;
        
    }

    private void Selector_GameConsoleEvent(UserProvider<User> provider)
    {
        User = provider.Spawn("User");
        provider.Select("User");
        _Updater.Add(User);
        InitialedEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        _Updater.Working();
    }

 
    private GameModeSelector<User> _Selector;

    public User User { get; private set; }
}
