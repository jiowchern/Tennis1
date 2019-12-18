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
    public Client()
    {
        _Updater = new Regulus.Utility.Updater();
    }
    void Start()
    {

        
    }

    public void RunByRemote()
    {
        _Client = new Regulus.Framework.Client<Tennis1.User.User>(this.Console, this.Console.Command);


        var asms = System.AppDomain.CurrentDomain.GetAssemblies();

        var asm = asms.Where(a => a.ManifestModule.Name == "Tennis1.ProtocolTmpl.dll").First();

        _Client.Selector.AddFactoty("remote", new Tennis1.User.RemoteUserFactory(asm));
        var provider = _Client.Selector.CreateUserProvider("remote");
        this.User  = provider.Spawn("User");
        provider.Select("User");
        _Updater.Add(_Client);
    }

    public void RunByStandalone()
    {
        _Client = new Regulus.Framework.Client<Tennis1.User.User>(this.Console, this.Console.Command);

        var asms = System.AppDomain.CurrentDomain.GetAssemblies();

        
        var asm = Regulus.Remote.Protocol.ProtocolProvider.GetProtocols().First().Assembly;
        
        var entry = new Tennis1.Game.Entry();
        entry.Launch();
        _Client.Selector.AddFactoty("standalone", new Tennis1.User.StandaloneUserFactory(entry, asm));
        var provider = _Client.Selector.CreateUserProvider("standalone");
        this.User = provider.Spawn("User");
        provider.Select("User");
        _Updater.Add(_Client);
    }

    // Update is called once per frame
    void Update()
    {
        _Updater.Working();
    }

    public User User { get; private set; }
}
