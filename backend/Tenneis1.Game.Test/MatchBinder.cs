using Regulus.Remote;
using System;

namespace Tests
{
    internal class MatchBinder : Regulus.Remote.ISoulBinder
    {
        public Tennis1.Common.IMatchable Matchable;
        public bool IsUnbind { get; private set; }

        public MatchBinder()
        {
        }

        event Action ISoulBinder.BreakEvent
        {
            add
            {
            }

            remove
            {

            }
        }

        void ISoulBinder.Bind<TSoul>(TSoul soul)
        {
            IsUnbind = false;
            Matchable = soul as Tennis1.Common.IMatchable;
        }

        void ISoulBinder.Return<TSoul>(TSoul soul)
        {
        }

        void ISoulBinder.Unbind<TSoul>(TSoul soul)
        {
            IsUnbind = true;
        }
    }
}