using System;
using Regulus.Game.Tennis1.Protocol;

namespace Regulus.Game.Tennis1.Game
{
    internal class LoungePlayer : Lounge.IPlayable   , Regulus.Game.Tennis1.Protocol.ILounge
    {
        private readonly User _User;

        public LoungePlayer(User user)
        {
            this._User = user;
            _User.Binder.Bind<Regulus.Game.Tennis1.Protocol.ILounge>(this);
        }

        Guid Identifiable.Id =>  _User.Id;

        event Action<string> _SignUpOnceEvent;
        event Action<string> Lounge.IPlayable.SignUpOnceEvent
        {
            add
            {
                _SignUpOnceEvent += value;
            }

            remove
            {
                _SignUpOnceEvent -= value;
            }
        }

        void ILounge.SignUp(string name)
        {
            _SignUpOnceEvent.Invoke(name);
            _SignUpOnceEvent = _Empty;
        }

        private void _Empty(string name)
        {
            
        }
    }
}