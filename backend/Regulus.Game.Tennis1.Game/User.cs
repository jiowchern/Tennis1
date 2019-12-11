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
            Name = Id.ToString();
        }

        public string Name { get; internal set; }
    }
}