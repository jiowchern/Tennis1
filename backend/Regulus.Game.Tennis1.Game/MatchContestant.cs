using System;
using Regulus.Game.Tennis1.Protocol;

namespace Regulus.Game.Tennis1.Game
{
    internal class MatchContestant : Matcher.IContestant , Regulus.Game.Tennis1.Protocol.IMatch
    {
        private User _User;

        public MatchContestant(User user)
        {
            this._User = user;
        }

        Guid Identifiable.Id => _User.Id;

        event Action _CancelOnceEvent;
        event Action Matcher.IContestant.CancelOnceEvent
        {
            add
            {
                _CancelOnceEvent += value;
            }

            remove
            {
                _CancelOnceEvent -= value;
            }
        }

        void IMatch.Cancel()
        {
            _CancelOnceEvent();
            _CancelOnceEvent = () => { };
        }
    }
}