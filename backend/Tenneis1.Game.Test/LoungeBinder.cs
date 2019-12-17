using Tennis1.Common;
using Regulus.Remote;
using System;

namespace Tests
{
    internal class LoungeBinder : ISoulBinder 
    {
        public Tennis1.Common.IPreparer Lounge;
        public bool IsUnbind { get; private set; }
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
            Lounge = soul as Tennis1.Common.IPreparer;


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