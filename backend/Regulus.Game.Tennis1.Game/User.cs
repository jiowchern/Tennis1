using System;
using Regulus.Remote;


namespace Regulus.Game.Tennis1.Game
{
    public class User
    {
        public readonly ISoulBinder Binder;
        public readonly Guid Id;
        public User(ISoulBinder binder)
        {
            Id = Guid.NewGuid();
            Binder = binder;
            Registration = new Protocol.Registration() { Name = Id.ToString(), PlayerNumber = 1 };
            
        }

        public Regulus.Game.Tennis1.Protocol.Registration Registration { get; internal set; }
    }
}