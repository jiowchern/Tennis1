using Regulus.Remote;
using System;

namespace Tests
{
    internal class MatchBinder : Regulus.Remote.IBinder
    {
        public Tennis1.Common.IMatchable Matchable;
        public bool IsUnbind { get; private set; }

        public MatchBinder()
        {
        }

        event Action IBinder.BreakEvent
        {
            add
            {
            }

            remove
            {

            }
        }

        void IBinder.Bind<TSoul>(TSoul soul)
        {
            IsUnbind = false;
            Matchable = soul as Tennis1.Common.IMatchable;
        }

        void IBinder.Return<TSoul>(TSoul soul)
        {
        }

        void IBinder.Unbind<TSoul>(TSoul soul)
        {
            IsUnbind = true;
        }
    }
}