using System;
using Regulus.Game.Tennis1.Protocol;

namespace Regulus.Game.Tennis1.Game
{
    
    internal class Preparer :  Regulus.Game.Tennis1.Protocol.IPreparer
    {
        public readonly Guid Id;

        readonly User _User;
        public Preparer(User user)
        {
            Id = user.Id;
            _User = user;
        }
        public void Start()
        {
            _User.Binder.Bind<Protocol.IPreparer>(this);
        }
        public void End()
        {
            _User.Binder.Unbind<Protocol.IPreparer>(this);
        }
        void Protocol.IPreparer.SignUp(Registration registration)
        {
            SignUpOnceEvent.Invoke(registration);
            SignUpOnceEvent = _Empty;
        }

        private void _Empty(Registration registration)
        {
        }

        public event System.Action<Registration> SignUpOnceEvent;

    }
}