using System;
using Tennis1.Common;

namespace Tennis1.Game
{
    
    internal class Preparer :  Tennis1.Common.IPreparer
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
            _User.Binder.Bind<Common.IPreparer>(this);
        }
        public void End()
        {
            _User.Binder.Unbind<Common.IPreparer>(this);
        }
        void Common.IPreparer.SignUp(Registration registration)
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