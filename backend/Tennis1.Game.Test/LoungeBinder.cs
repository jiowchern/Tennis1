using Tennis1.Common;
using Regulus.Remote;
using System;

namespace Tests
{
    internal class LoungeBinder : IBinder 
    {
        public Tennis1.Common.IPreparer Lounge;
        public bool IsUnbind { get; private set; }
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
            Lounge = soul as Tennis1.Common.IPreparer;


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