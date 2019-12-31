using System;
using Regulus.Remote;


namespace Tennis1.Game
{
    public class User
    {
        public readonly IBinder Binder;
        public readonly Guid Id;
        public User(IBinder binder)
        {
            Id = Guid.NewGuid();
            Binder = binder;
            Registration = new Common.Registration() { Name = Id.ToString(), PlayerNumber = 1 };
            
        }

        public Tennis1.Common.Registration Registration { get; set; }
    }
}